using AutoMapper;
using Hrm.Application.Constants;
using hrm.Application.DTOs.Modules;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Features;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Modules.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

using System.Security.Claims;

namespace Hrm.Application.Features.Modules.Handlers.Queries
{
    public class GetModuleFeaturesHandler : IRequestHandler<GetModuleFeaturesRequests, List<ModuleFeatureDto>>
    {
        private readonly IHrmRepository<Module> _ModuleRepository;
        private readonly IHrmRepository<Feature> _FeatureRepository;
        private readonly IHrmRepository<RoleFeature> _RoleFeatureRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetModuleFeaturesHandler(
            IHrmRepository<Feature> FeatureRepository,
            IHrmRepository<RoleFeature> RoleFeatureRepository,
            IHrmRepository<Module> ModuleRepository, 
            IHttpContextAccessor httpContextAccessor, 
            IMapper mapper)
        {
            _ModuleRepository = ModuleRepository;
           // _RoleFeatureRepository = RoleFeatureRepository;
            _FeatureRepository = FeatureRepository;
            _mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ModuleFeatureDto>> Handle(GetModuleFeaturesRequests request, CancellationToken cancellationToken)
        {

            var rId = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value;
            var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            ICollection<RoleFeature> roleFeature = await _RoleFeatureRepository.FilterAsync(x => x.RoleId == rId);
            int[] featureIds = roleFeature.Select(x => x.FeatureKey).Distinct().ToArray();
            List<Feature> feautres = _FeatureRepository.FilterWithInclude(x => x.IsActive && x.FeatureTypeId == request.FeatureType && featureIds.Contains(x.FeatureId), "Module").ToList();
            List<Module> modeules = feautres.GroupBy(x => x.Module.ModuleName).Select(x => x.First().Module).OrderBy(x => x.MenuPosition).ToList();
            if (!featureIds.Any())
            {
                throw new BadRequestException("Features Not Assigned");
            }

            List<ModuleFeatureDto> moduleFeatureDto = _mapper.Map<List<ModuleFeatureDto>>(modeules);
            List<FeatureDto> featureDtos = _mapper.Map<List<FeatureDto>>(feautres);

            moduleFeatureDto = moduleFeatureDto.OrderBy(x => x.MenuPosition).Select(x =>
            {
                x.Features = featureDtos.Where(y => y.ModuleId == x.ModuleId).OrderBy(o => o.OrderNo).ToList();
                x.Role = role;
                return x;
            }).ToList();

            return moduleFeatureDto;
        }
    }

}
