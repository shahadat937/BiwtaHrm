using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Features;
using Hrm.Application.DTOs.RoleFeatures;
using Hrm.Application.Features.RoleFeatures.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RoleFeatures.Handlers.Queries
{
    public class GetModuleFeaturesByRoleNameRequestHandler : IRequestHandler<GetModuleFeaturesByRoleNameRequest, List<ModuleFeatureDto>>
    {
        private readonly IHrmRepository<RoleFeature> _RoleFeaturesRepository;
        private readonly IHrmRepository<Feature> _FeaturesRepository;
        private readonly IHrmRepository<AspNetRoles> _AspNetRolesRepository;
        private readonly IHrmRepository<Module> _ModuleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetModuleFeaturesByRoleNameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<RoleFeature> RoleFeaturesRepository, IHrmRepository<Feature> featuresRepository, IHrmRepository<AspNetRoles> aspNetRolesRepository, IHrmRepository<Module> moduleRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _RoleFeaturesRepository = RoleFeaturesRepository;
            _FeaturesRepository = featuresRepository;
            _AspNetRolesRepository = aspNetRolesRepository;
            _ModuleRepository = moduleRepository;
        }

        public async Task<List<ModuleFeatureDto>> Handle(GetModuleFeaturesByRoleNameRequest request, CancellationToken cancellationToken)
        {
            var dashboardModule = new ModuleFeatureDto
            {
                Name = "Dashboard",
                Url = "/dashboard",
                IconComponent = new IconComponentDto
                {
                    Name = "cil-speedometer"
                }
            };

            var roleId = await _AspNetRolesRepository.FindOneAsync(x => x.Name == request.RoleName);

            var allRoleFeatures = await _RoleFeaturesRepository.Where(x => x.RoleId == roleId.Id && x.ViewStatus == true).ToListAsync();

            var dynamicModules = allRoleFeatures
                .GroupBy(rf =>
                {
                    var feature = _FeaturesRepository.FindOne(x => x.FeatureId == rf.FeatureKey);
                    var module = _ModuleRepository.FindOne(x => x.ModuleId == feature.ModuleId);

                    return new
                    {
                        Name = module.Title,
                        Url = module.ModuleName,
                        Icon = module.IconName,
                        MenuPosition = module.MenuPosition
                    };
                })
                .OrderBy(group => group.Key.MenuPosition)
                .Select(group => new ModuleFeatureDto
                {
                    Name = group.Key.Name,
                    Url = "/" + group.Key.Url,
                    IconComponent = new IconComponentDto
                    {
                        Name = group.Key.Icon
                    },
                    Children = group.Select(rf =>
                    {
                        var feature = _FeaturesRepository.FindOne(x => x.FeatureId == rf.FeatureKey);

                        return new ModuleFeaturesGroupDto
                        {
                            Name = feature.FeatureName,
                            Url = "/" + group.Key.Url + "/" + feature.Path,
                            OrderNo = feature.OrderNo,
                            IsActive = feature.IsActive
                        };
                    })
                    .OrderBy(child => child.OrderNo).Where(x => x.IsActive == true).ToList()
                })
                .ToList();

            var result = new List<ModuleFeatureDto> { dashboardModule };
            result.AddRange(dynamicModules);

            return result;
        }

    }

}
