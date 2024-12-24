using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AttendanceDevice.Requests.Commands;
using Hrm.Infrastructure.SignalRHub;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Commands
{
    public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand,object>
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IHrmRepository<Hrm.Domain.Attendance> _attendanceRepo;

        public SendNotificationCommandHandler(IHubContext<NotificationHub> hubContext, IHrmRepository<Domain.Attendance> attendanceRepo)
        {
            _hubContext = hubContext;
            _attendanceRepo = attendanceRepo;
        }

        public async Task<object> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {

            var attendance = await _attendanceRepo.Get(16);
            await _hubContext.Clients.All.SendAsync("newDevice", attendance);
            await _hubContext.Clients.All.SendAsync("notification", "New Message Available");

            return "Ok";
        }
    }
}
