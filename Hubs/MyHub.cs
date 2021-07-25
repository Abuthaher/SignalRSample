using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleNotifiaction.Hubs
{
    public class MyHub : Hub
    {
        public async Task SendMessages()
        {            
            await Clients.All.SendAsync("ReceiveMessage");
        }
    }
}
