using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Features.AttendanceDevice.Requests.Commands;
using Hrm.Infrastructure.SignalRHub;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Commands
{
    public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand,object>
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SendNotificationCommandHandler(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task<object> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("newDevice", "New Device Available");
            await _hubContext.Clients.All.SendAsync("notification", "New Message Available");

            return "Ok";
        }
    }
}
