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

            sRClient.ChannelURL = "http://localhost:63102/";
            //sRClient.ChannelURL = "http://swmsignalrsite.azurewebsites.net/";
            sRClient.HubName = "stockTicker";
            sRClient.HubConnect();
            sRClient.HubInvoke("CloseMarket");
            System.Threading.Thread.Sleep(5000);
            sRClient.HubInvoke("Reset");
            System.Threading.Thread.Sleep(5000);
            sRClient.HubInvoke("OpenMarket");
            SignalRClient.SignalRClient.KPI kpi = new SignalRClient.SignalRClient.KPI();
            kpi.Type = "Quote";
            kpi.Amount = 444.44m;
            sRClient.HubInvoke("UpdateStockPrice", kpi);
            System.Threading.Thread.Sleep(5000);




        }
    }
}