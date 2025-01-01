using Hrm.Application.DTOs.Common;
using Hrm.Application.DTOs.Notification;
using Hrm.Application.Models;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Notifications.Requests.Queries
{
    public class GetNotificationForUserRequest : IRequest<PagedResult<NotificationDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int EmpId { get; set; }
    }
}
