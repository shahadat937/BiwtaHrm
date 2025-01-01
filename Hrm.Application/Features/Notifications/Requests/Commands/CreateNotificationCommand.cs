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
    public class CreateNotificationCommand : IRequest<BaseCommandResponse>
    {
        public CreateNotificationDto NotificationDto { get; set; }
    }
}
