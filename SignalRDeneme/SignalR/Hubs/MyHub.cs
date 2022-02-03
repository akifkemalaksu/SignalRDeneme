using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRDeneme.SignalHub
{
    [HubName("MyHub")]
    public class MyHub : Hub
    {
        [HubMethodName("tetikle")]
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            context.Clients.All.tetikle();
        }
    }
}