using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;
using SpeakToMe.Core;
using SpeakToMe.Core.Enums;
using SpeakToMe.Core.Events;
using SpeakToMe.Core.Interfaces;
using SpeakToMe.Speech;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakToMe.Presence
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SignalRPresence : IPresence
    {
        [Import]
        public IEventAggregator EventAggregator { get; set; }

        public bool IsConnected
        {
            get { return true; }
        }

        public void Initialize()
        {
            // *** TODO ***
        }

        private void ReplyToChannelEventHandler(ReplyToChannelEventArgs args)
        {
            if (args.Mode == ConversationType.SignalR)
            {
                if (args.Callback != null)
                {
                    args.Reply.UserId = args.UserId;
                    args.Reply.ConversationId = args.ConversationId;
                    args.Callback.ReturnResult(args.Reply);
                }
            }
        }

        public void OnImportsSatisfied()
        {
            this.EventAggregator.GetEvent<ReplyToChannelEvent>().Subscribe(ReplyToChannelEventHandler);
        }

        public void ProcessCommand(string command, string userId, string conversationId, IErpServiceCallback callback)
        {
            var processor = ServiceLocator.GetInstance<CommandProcessor>();

            processor.ProcessCommand(command, userId, ConversationType.SignalR, conversationId, callback);
        }
    }
}
