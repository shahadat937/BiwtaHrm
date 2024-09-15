using Hrm.Application;
using Hrm.Application.DTOs.EmpPermanentAddress;
using Hrm.Application.Features.EmpPermanentAddresses.Requests.Commands;
using Hrm.Application.Features.EmpPermanentAddresses.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpPermanentAddress)]
    [ApiController]
    [Authorize]
    public class EmpPermanentAddressController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpPermanentAddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-EmpPermanentAddressByEmpId/{id}")]
        public async Task<ActionResult<EmpPermanentAddressDto>> GetEmpPermanentAddresssById(int id)
        {
            var EmpPermanentAddresss = await _mediator.Send(new GetEmpPermanentAddressByIdRequest { Id = id });
            return Ok(EmpPermanentAddresss);
        }


        [HttpPost]
        [Route("save-EmpPermanentAddresss")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmpPermanentAddressDto EmpPermanentAddresss)
        {
            var command = new CreateEmpPermanentAddressCommand { EmpPermanentAddressDto = EmpPermanentAddresss };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-EmpPermanentAddresss/{id}")]
        public async Task<ActionResult> Put([FromBody] EmpPermanentAddressDto EmpPermanentAddresss)
        {
            var command = new UpdateEmpPermanentAddressCommand { EmpPermanentAddressDto = EmpPermanentAddresss };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
