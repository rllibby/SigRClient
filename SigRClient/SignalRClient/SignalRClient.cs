using Microsoft.AspNet.SignalR.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRClient
{
    public class SignalRClient
    {
        private IHubProxy erpTicker;
        //private IHubConnection _hubConn;

        public string ChannelURL { get; set; }
        public string HubName { get; set; }

        public void HubConnect()
        {
            var _hubConn = new HubConnection(ChannelURL);

            erpTicker = _hubConn.CreateHubProxy(HubName);

            //stockTicker.On("updateStockPrice", stock => Console.WriteLine("Symbol {0} Price {1}", stock.Symbol, stock.Price));

            erpTicker.On("updateSalesKPI", SalesKPIAlert);

            //
            _hubConn.Start().Wait();
            // _hubConn.Start();

        }

        public void HubInvoke(string methodName)
        {
            erpTicker.Invoke(methodName).Wait();
        }

        private void SalesKPIAlert(dynamic kpi)
        {
            Console.WriteLine("Symbol {0} Price {1}", kpi.Type, kpi.Total);

        }

        public void HubInvoke(string methodName, KPI kpi)
        {
            erpTicker.Invoke(methodName, kpi).Wait();
        }

        public class KPI
        {
            public string Type { get; set; }

            public decimal Amount { get; set; }

        }
    }
}
