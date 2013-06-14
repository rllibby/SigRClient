using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalRClient;

namespace SigRConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var sRClient = new SignalRClient.SignalRClient();

            sRClient.ChannelURL = "http://swmsignalrsite.azurewebsites.net/";
            sRClient.HubName = "stockTicker";
            sRClient.HubConnect();
            sRClient.HubInvoke("CloseMarket");
            System.Threading.Thread.Sleep(5000);
            sRClient.HubInvoke("Reset");
            System.Threading.Thread.Sleep(5000);
            sRClient.HubInvoke("OpenMarket");

            while (true)
            {


            }
/*
            var sRClient = new SignalRResponder.SignalRResponder();

            // sRClient.ChannelURL = "http://localhost:63102/";
            sRClient.ChannelURL = "http://swmsignalrsite.azurewebsites.net/";
            sRClient.HubName = "stockTicker";
            var connID = sRClient.HubConnect();
           
            //System.Threading.Thread.Sleep(5000);
            //sRClient.HubInvoke("Reset");
            //System.Threading.Thread.Sleep(5000);
            //sRClient.HubInvoke("OpenMarket");
            SignalRResponder.ERPRequest request = new SignalRResponder.ERPRequest();
            request.CallerId = connID;
            request.RequestString = "left hand drawer";
            sRClient.HubInvoke("SendRequest", request.CallerId, request.RequestString);
            System.Threading.Thread.Sleep(5000);
*/


        }
    }
}