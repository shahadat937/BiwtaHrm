using Hrm.Application;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmployeeType)]
    [ApiController]
    public class EmployeeTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
    
    }
}
