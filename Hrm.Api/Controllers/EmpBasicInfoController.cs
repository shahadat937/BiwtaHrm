using Hrm.Application;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.DTOs.Employee;
using Hrm.Application.Features.EmpBasicInfos.Requests.Commands;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Application.Features.Employee.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpBasicInfo)]
    [ApiController]
    [Authorize]
    public class EmpBasicInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpBasicInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("get-allEmpBasicInfo")]
        public async Task<ActionResult<List<EmpBasicInfoDto>>> GetEmpBasicInfos([FromQuery] QueryParams queryParams, int departmentId, int sectionId)
        {
            var EmpBasicInfos = await _mediator.Send(new GetAllEmpBasicInfoRequest { QueryParams = queryParams, DepartmentId = departmentId, SectionId = sectionId });
            return Ok(EmpBasicInfos);
        }

        [HttpGet]
        [Route("get-EmpBasicInfosById/{id}")]
        public async Task<ActionResult<EmpBasicInfoDto>> GetEmpBasicInfosById(int id)
        {
            var EmpBasicInfos = await _mediator.Send(new GetEmpBasicInfoByIdRequest { Id = id });
            return Ok(EmpBasicInfos);
        }

        [HttpGet]
        [Route("get-EmpBasicInfoByAspNetUserId/{id}")]
        public async Task<ActionResult<EmpBasicInfoDto>> GetEmpBasicInfoByAspNetUserId(string id)
        {
            var EmpBasicInfo = await _mediator.Send(new GetEmpBasicInfoByAspNetUserIdRequest { AspNetUserId = id });
            return Ok(EmpBasicInfo);
        }

        [HttpGet]
        [Route("get-SelectedEmpBasicInfo")]
        public async Task<ActionResult> SelectedEmpBasicInfo()
        {
            var command = new GetSelectedEmpBasicInfoRequest { };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-SelectedFilteredEmpBasicInfo")]
        public async Task<ActionResult> SelectedFilteredEmpBasicInfo([FromQuery] EmpBasicInfoFilterDto filter)
        {
            var command = new GetSelectedFilteredEmpBasicInfoRequest { EmpFilterDto = filter };
            var response = await _mediator.Send(command);

            return Ok(response);

        }


        [HttpPost]
        [Route("save-EmpBasicInfos")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmpBasicInfoDto EmpBasicInfos)
        {
            var command = new CreateEmpBasicInfoCommand { EmpBasicInfoDto = EmpBasicInfos };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("save-ImportedEmpBasicInfos")]
        public async Task<ActionResult<BaseCommandResponse>> PostImportedEmpBasicInfo([FromBody] List<CreateEmpBasicInfoDto> EmpBasicInfos)
        {
            var command = new CreateImportedEmpBasicInfoCommand { EmpBasicInfoDtos = EmpBasicInfos };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-EmpBasicInfos/{id}")]
        public async Task<ActionResult> Put([FromBody] EmpBasicInfoDto EmpBasicInfos)
        {
            var command = new UpdateEmpBasicInfoCommand { EmpBasicInfoDto = EmpBasicInfos };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("update-userStatus/{id}")]
        public async Task<ActionResult> UpdateUserStatus(int id)
        {
            var command = new UpdateUserStatusCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-empBasicInfoByIdCardNo/{id}")]
        public async Task<ActionResult> GetEmpBasicInfoByIdCardNo(string id)
        {
            var command = new GetEmpBasicInfoByIdCardNoRequest { IdCardNo = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}