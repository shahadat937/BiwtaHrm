﻿using Hrm.Application;
using Hrm.Application.DTOs.District;
using Hrm.Application.DTOs.District;
using Hrm.Application.Features.District.Requests.Commands;
using Hrm.Application.Features.District.Requests.Queries;
using Hrm.Application.Features.District.Requests.Commands;
using Hrm.Application.Features.Stores.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Shared.Models;
using Hrm.Domain;
using Hrm.Application.DTOs.Division;
using Hrm.Application.Features.Division.Requests.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.District)]
    [ApiController]
    [Authorize]
    public class DistrictController : Controller
    {
        private readonly IMediator _mediator;
        public DistrictController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-district")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDistrictDto District)
        {
            var command = new CreateDistrictCommand { DistrictDto = District };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-district")]
        public async Task<ActionResult> Get()
        {
            var District = await _mediator.Send(new GetDistrictRequest { });
            return Ok(District);
        }

        [HttpGet]
        [Route("get-districtbyid/{id}")]
        public async Task<ActionResult<DistrictDto>> Get(int id)
        {
            var District = await _mediator.Send(new GetDistrictByIdRequest { DistrictId = id });
            return Ok(District);

        }
        [HttpGet]
        [Route("get-districByDivisionId/{id}")]
        public async Task<ActionResult<List<DistrictDto>>> GetDistricByDivisionId(int id)
        {
  
            var districts = await _mediator.Send(new GetDistrictByDivisionIdRequest
            {
                DivisionId = id
            });
            return Ok(districts);

        }
        [HttpGet]
        [Route("get-selecteddistrict")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedDistrict()
        {
            var district = await _mediator.Send(new GetSelectedDistrictRequest { });
            return Ok(district);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-district/{id}")]
        public async Task<ActionResult> Put([FromBody] DistrictDto District)
        {
            var command = new UpdateDistrictCommand { DistrictDto = District };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-district/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteDistrictCommand { DistrictId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
