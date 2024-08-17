using AutoMapper;
using Azure.Core;
using Hrm.Application;
using Hrm.Application.DTOs.EmpSpouseInfo;
using Hrm.Application.DTOs.Features;
using Hrm.Application.DTOs.RoleFeatures;
using Hrm.Application.Features.EmpSpouseInfos.Requests.Commands;
using Hrm.Application.Features.RoleFeatures.Requests.Commands;
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
        public async Task<ActionResult<List<RoleFeatureDto>>> GetFeaturesByRole(string roleId)
        {
            var allFeatures = await _context.Feature.ToListAsync();

            // Retrieve role features for the given roleId
            var roleFeatures = await _context.RoleFeature
                .Where(rf => rf.RoleId == roleId)
                .ToListAsync();

            // Combine data
            var result = allFeatures.Select(f =>
            {
                var roleFeature = roleFeatures.FirstOrDefault(rf => rf.FeatureKey == f.FeatureId);
                return new RoleFeatureDto
                {
                    RoleFeatureId = roleFeature?.RoleFeatureId ?? 0,
                    RoleId = roleFeature?.RoleId ?? roleId,
                    FeatureKey = f.FeatureId,
                    FeatureName = f.FeatureName,
                    ViewStatus = roleFeature?.ViewStatus ?? false, // Set to false if not found
                    Add = roleFeature?.Add ?? false,
                    Update = roleFeature?.Update ?? false,
                    Delete = roleFeature?.Delete ?? false
                };
            }).ToList();


            return Ok(result);
        }

        [HttpPost]
        [Route("save-roleFeatures")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] List<CreateRoleFeatureDto> roleFeatureDto)
        {
            var command = new CreateRoleFeaturesCommand { RoleFeatureDtos = roleFeatureDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
