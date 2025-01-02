using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.Notifications.Requests.Queries;
using Hrm.Application.DTOs.Board;
using Hrm.Application.Features.Board.Requests.Commands;
using Hrm.Application.DTOs.Notification;
using Hrm.Application.Features.Notifications.Requests.Commands;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Notification)]
    [ApiController]
    //[Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("get-notificationForUser")]
        public async Task<ActionResult<List<EmpBasicInfoDto>>> GetNotificationForUser([FromQuery] QueryParams queryParams, int empId)
        {
            var notifications = await _mediator.Send(new GetNotificationForUserRequest { QueryParams = queryParams, EmpId = empId });
            return Ok(notifications);
        }

        [HttpPost]
        [Route("save-notification")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateNotificationDto createNotificationDto)
        {
            var command = new CreateNotificationCommand { NotificationDto = createNotificationDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
