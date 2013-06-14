using Microsoft.AspNet.SignalR.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeakToMe.Core;
using SpeakToMe.Core.Interfaces;
using SpeakToMe.Presence;
using SpeakToMe.Speech;
using SpeakToMe.Speech.Utility;
using System.ComponentModel.Composition.Hosting;

namespace SignalRResponder
{
    public class ERPRequest
    {
        public string CallerId { get; set; }
        public string RequestString { get; set; }
    }

    public class ERPResponseEvent : EventArgs
    {
        public string UserId { get; set; }
        public string ConversationId { get; set; }
        public string Entity { get; set; }
        public string Target { get; set; }
        public string Context { get; set; }
        public bool IsQuestion { get; set; }
        public string QuestionText { get; set; }
    }

    class BootStrapper : BootStrapperBase
    {
        protected override void AddCustomAssembliesToCatalog(System.ComponentModel.Composition.Hosting.AggregateCatalog catalog)
        {
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(TestPresence).Assembly));
        }
    }

    public class SignalRResponder
    {
        private IHubProxy _proxy;
        private HubConnection _Hub;
        private BootStrapper _Bs;
        private IPresence _Presence;

        public string ChannelURL { get; set; }
        public string HubName { get; set; }

        public event EventHandler<ERPResponseEvent> OnERPRequest;

        public string HubConnect()
        {
            _Bs = new BootStrapper();
            _Bs.Initialize();

            _Presence = ServiceLocator.GetInstance<SignalRPresence>();

            _Hub = new HubConnection(ChannelURL);

            _proxy = _Hub.CreateHubProxy(HubName);
            _proxy.On<ERPRequest>("incomingRequest", request => DoERPRequest(request));

            _Hub.Start().Wait();

            _proxy.Invoke("registerRequestHandler", _Hub.ConnectionId, _Hub.ConnectionId);

            return _Hub.ConnectionId;
        }

        private void OnERPResponse(IErpResult data)
        {
            EventHandler<ERPResponseEvent> handler = OnERPRequest;

            // Do we have an event handler wired up?
            if (handler != null)
            {
                ERPResponseEvent args = new ERPResponseEvent();

                args.UserId = data.UserId;
                args.ConversationId = data.ConversationId;
                args.Entity = data.Entity;
                args.Target = data.Target;
                args.Context = data.Context;
                args.IsQuestion = data.IsQuestion;
                args.QuestionText = data.QuestionText;

                // Fire the event
                handler.Invoke(this, args);
            }
        }

        private void DoERPRequest(ERPRequest request)
        {
            _Presence.ProcessCommand(request.RequestString, request.CallerId, request.CallerId, new CallbackWrapper(OnERPResponse));
        }

        public void HubInvoke(string methodName)
        {
            _proxy.Invoke(methodName).Wait();
        }

        public void HubInvoke(string methodName, string arg)
        {
            _proxy.Invoke(methodName, arg);
        }

        public void HubInvoke(string methodName, string arg1, string arg2)
        {
            _proxy.Invoke(methodName, arg1, arg2);
        }
    }
}
