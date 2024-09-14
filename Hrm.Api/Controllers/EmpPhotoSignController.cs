using Hrm.Application;
using Hrm.Application.DTOs.EmpPhotoSign;
using Hrm.Application.Features.EmpPhotoSigns.Requests.Commands;
using Hrm.Application.Features.EmpPhotoSigns.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpPhotoSign)]
    [ApiController]
    [Authorize]
    public class EmpPhotoSignController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpPhotoSignController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-EmpPhotoSignByEmpId/{id}")]
        public async Task<ActionResult<EmpPhotoSignDto>> GetEmpPhotoSignsById(int id)
        {
            var EmpPhotoSigns = await _mediator.Send(new GetEmpPhotoSignByIdRequest { Id = id });
            return Ok(EmpPhotoSigns);
        }


        [HttpPost]
        [Route("save-EmpPhotoSigns")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateEmpPhotoSignDto EmpPhotoSigns)
        {
            var command = new CreateEmpPhotoSignCommand { EmpPhotoSignDto = EmpPhotoSigns };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-EmpPhotoSigns/{id}")]
        public async Task<ActionResult> Put([FromForm] CreateEmpPhotoSignDto EmpPhotoSigns)
        {
            var command = new UpdateEmpPhotoSignCommand { EmpPhotoSignDto = EmpPhotoSigns };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
