using Hrm.Application.DTOs.Notification;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Notifications.Requests.Commands
{
    public class UpdateNotificationStatusCommand : IRequest<BaseCommandResponse>
    {
        public CreateNotificationReadByDto NotificationReadByDto { get; set; }
    }
}
