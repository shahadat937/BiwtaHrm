﻿using Hrm.Application;
using Hrm.Application.DTOs.Form;
using Hrm.Application.Features.Form.Requests.Commands;
using Hrm.Application.Features.Form.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using System.Dynamic;
using System.Runtime.InteropServices;
namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Form)]
    [ApiController]
    [Authorize]
    public class FormController:Controller
    {
        private readonly IMediator _mediator;
        
        public FormController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-Form")]
        public async Task<ActionResult> GetForm()
        {
            var command = new GetFormRequest();
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-FormById/{id}")]
        public async Task<ActionResult> GetFormBy(int id)
        {
            var command = new GetFormByIdRequest { FormId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-SelectedForm")]
        public async Task<ActionResult> GetSelectedForm()
        {
            var command = new GetSelectedFormRequest { };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-formAllInfoById/{id}")]
        public async Task<ActionResult> GetFormAllInfoById(int id)
        {
            var command = new GetFormAllInfoByIdRequest { FormId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-formDataById/{formRecordId}")]
        public async Task<ActionResult> GetFormData(int formRecordId)
        {
            var command = new GetAllFormDataRequest { FormRecordId = formRecordId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-EmployeeInfoForForm")]
        public async Task<ActionResult> GetEmployeeInfoForForm([FromQuery] string? IdCardNo)
        {
            var command = new GetEmployeeInfoRequest { IdCardNo = IdCardNo };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-EmployeeJobHistory")]
        public async Task<ActionResult> GetEmployeeJobHistory([FromQuery] int EmpId, [FromQuery] DateOnly StartDate, DateOnly EndDate)
        {
            var command = new GetEmployeeJobHistoryForFormRequest { EmpId = EmpId, startDate = StartDate, endDate = EndDate };

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-Form")]
        public async Task<ActionResult<BaseCommandResponse>> SaveForm([FromBody] CreateFormDto formDto)
        {
            var command = new CreateFormCommand { formDto = formDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-FormData")]
        public async Task<ActionResult<BaseCommandResponse>> SaveFormData([FromBody] FormDataDto formData)
        {
            var command = new CreateFormDataCommand { formData = formData };
            var response = await _mediator.Send(command);

            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("update-Form")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateForm([FromBody] FormDto formDto)
        {
            var command = new UpdateFormCommand { formDto = formDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("update-FormData")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateFormData([FromBody] FormDataDto formData, [FromQuery] int UpdateRole)
        {
            var command = new UpdateFormDataCommand { formData = formData, UpdateRole = UpdateRole };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("delete-Form")]
        public async Task<ActionResult<BaseCommandResponse>> DeleteForm(int formId)
        {
            var command = new DeleteFormCommand { FormId = formId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
