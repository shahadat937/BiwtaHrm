using Hrm.Application;
using Hrm.Application.DTOs.EmpPersonalInfo;
using Hrm.Application.DTOs.EmpPresentAddress;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Commands;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Queries;
using Hrm.Application.Features.EmpPresentAddresses.Requests.Commands;
using Hrm.Application.Features.EmpPresentAddresses.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpPresentAddress)]
    [ApiController]
    [Authorize]
    public class EmpPresentAddressController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpPresentAddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-EmpPresentAddressByEmpId/{id}")]
        public async Task<ActionResult<EmpPresentAddressDto>> GetEmpPresentAddresssById(int id)
        {
            var EmpPresentAddresss = await _mediator.Send(new GetEmpPresentAddressByIdRequest { Id = id });
            return Ok(EmpPresentAddresss);
        }


        [HttpPost]
        [Route("save-EmpPresentAddresss")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmpPresentAddressDto EmpPresentAddresss)
        {
            var command = new CreateEmpPresentAddressCommand { EmpPresentAddressDto = EmpPresentAddresss };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-EmpPresentAddresss/{id}")]
        public async Task<ActionResult> Put([FromBody] EmpPresentAddressDto EmpPresentAddresss)
        {
            var command = new UpdateEmpPresentAddressCommand { EmpPresentAddressDto = EmpPresentAddresss };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
