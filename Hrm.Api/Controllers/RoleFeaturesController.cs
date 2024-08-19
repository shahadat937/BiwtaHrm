using AutoMapper;
using Azure.Core;
using Hrm.Application;
using Hrm.Application.DTOs.EmpSpouseInfo;
using Hrm.Application.DTOs.Features;
using Hrm.Application.DTOs.RoleFeatures;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Application.Features.EmpSpouseInfos.Requests.Commands;
using Hrm.Application.Features.Result.Requests.Queries;
using Hrm.Application.Features.RoleFeatures.Requests.Commands;
using Hrm.Application.Features.RoleFeatures.Requests.Queries;
using Hrm.Application.Features.UserRoles.Requests.Queries;
using Hrm.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.RoleFeatures)]
    [ApiController]
    public class RoleFeaturesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly HrmDbContext _context;
        private readonly IMapper _mapper;

        public RoleFeaturesController(IMediator mediator, HrmDbContext context, IMapper mapper)
        {
            _mediator = mediator;
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("get-features-by-role/{roleId}")]
        public async Task<ActionResult> GetFeaturesByRole(string roleId)
        {
            var roleFeatures = await _mediator.Send(new GetRoleFeaturesByRoleIdRequest { RoleId = roleId });
            return Ok(roleFeatures);
        }

        [HttpPost]
        [Route("save-roleFeatures")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] List<CreateRoleFeatureDto> roleFeatureDto)
        {
            var command = new CreateRoleFeaturesCommand { RoleFeatureDtos = roleFeatureDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route(("get-Modulefeatures-by-role/{roleName}"))]
        public async Task<ActionResult> GetModuleFeaturesByRole(string roleName)
        {
            var roleFeatures = await _mediator.Send(new GetModuleFeaturesByRoleNameRequest { RoleName = roleName });
            return Ok(roleFeatures);
        }


        [HttpGet]
        [Route(("get-featurePermission"))]
        public async Task<ActionResult> GetPermissionFeature(string roleName, string featurePath)
        {
            var featurePermission = await _mediator.Send(new GetPermissionByRoleFeatureRequest { RoleName = roleName , FeaturePath = featurePath });
            return Ok(featurePermission);
        }
    }
}
