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
    public class GetModuleFeaturesByRoleIdRequestHandler : IRequestHandler<GetModuleFeaturesByRoleIdRequest, List<ModuleFeatureDto>>
    {
        private readonly IHrmRepository<RoleFeature> _RoleFeaturesRepository;
        private readonly IHrmRepository<Feature> _FeaturesRepository;
        private readonly IHrmRepository<AspNetRoles> _AspNetRolesRepository;
        private readonly IHrmRepository<Module> _ModuleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetModuleFeaturesByRoleIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<RoleFeature> RoleFeaturesRepository, IHrmRepository<Feature> featuresRepository, IHrmRepository<AspNetRoles> aspNetRolesRepository, IHrmRepository<Module> moduleRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _RoleFeaturesRepository = RoleFeaturesRepository;
            _FeaturesRepository = featuresRepository;
            _AspNetRolesRepository = aspNetRolesRepository;
            _ModuleRepository = moduleRepository;
        }

        public async Task<List<ModuleFeatureDto>> Handle(GetModuleFeaturesByRoleIdRequest request, CancellationToken cancellationToken)
        {
            var allRoleFeatures = await _RoleFeaturesRepository.Where(x => x.RoleName == request.RoleName && x.ViewStatus == true).ToListAsync();

            var result = allRoleFeatures
                .GroupBy(rf =>
                {
                    var feature = _FeaturesRepository.FindOne(x => x.FeatureId == rf.FeatureKey);
                    var module = _ModuleRepository.FindOne(x => x.ModuleId == feature.ModuleId);

                    return new
                    {
                        ModuleName = module.Title,
                        ModuleLink = module.ModuleName
                    };
                })
                .Select(group => new ModuleFeatureDto
                {
                    ModuleName = group.Key.ModuleName,
                    ModuleLink = group.Key.ModuleLink,
                    Features = group.Select(rf =>
                    {
                        var feature = _FeaturesRepository.FindOne(x => x.FeatureId == rf.FeatureKey);

                        return new ModuleFeaturesGroupDto
                        {
                            RoleName = rf.RoleName,
                            FeatureName = rf.FeatureName,
                            ViewStatus = rf?.ViewStatus ?? false,
                            Add = rf?.Add ?? false,
                            Update = rf?.Update ?? false,
                            Delete = rf?.Delete ?? false,
                            FeatureLink = feature.Path,
                        };
                    }).ToList()
                })
                .ToList();

            return result;
        }
    }

}
