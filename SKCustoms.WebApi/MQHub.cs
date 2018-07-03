using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SKCustoms.WebApi
{
    public class MQHub : Hub
    {
        public void Hello()
        {
            //Clients.All.hello();
        }

        public override Task OnConnected()
        {
            MQHubsConfig.Clients = this.Clients;
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            MQHubsConfig.Clients = this.Clients;
            return base.OnDisconnected(stopCalled);
        }
    }
}