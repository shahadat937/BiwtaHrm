using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace Hrm.Infrastructure.SignalRHub
{
    public class NotificationHub : Hub
    {



        public async Task TriggerUpdateDevice(string message)
        {
            await Clients.All.SendAsync(message);
        }
    }
}
