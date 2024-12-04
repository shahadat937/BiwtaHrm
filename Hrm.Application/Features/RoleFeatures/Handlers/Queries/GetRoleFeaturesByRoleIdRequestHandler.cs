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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RoleFeatures.Handlers.Queries
{
    public class GetRoleFeaturesByRoleIdRequestHandler : IRequestHandler<GetRoleFeaturesByRoleIdRequest, List<RoleFeatureDto>>
    {
        private readonly IHrmRepository<RoleFeature> _RoleFeaturesRepository;
        private readonly IHrmRepository<Feature> _FeaturesRepository;
        private readonly IHrmRepository<AspNetRoles> _AspNetRolesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetRoleFeaturesByRoleIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<RoleFeature> RoleFeaturesRepository, IHrmRepository<Feature> featuresRepository, IHrmRepository<AspNetRoles> aspNetRolesRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _RoleFeaturesRepository = RoleFeaturesRepository;
            _FeaturesRepository = featuresRepository;
            _AspNetRolesRepository = aspNetRolesRepository;
        }

        public async Task<List<RoleFeatureDto>> Handle(GetRoleFeaturesByRoleIdRequest request, CancellationToken cancellationToken)
        {
            var allFeatures = await _FeaturesRepository.Where(x => true).Include(x => x.Module).ToListAsync();

            var roleName = _AspNetRolesRepository.Where(x => x.Id == request.RoleId).FirstOrDefault();

            var roleFeatures = await _RoleFeaturesRepository.Where(rf => rf.RoleId == request.RoleId).ToListAsync();

            var result = allFeatures.Select(f =>
            {
                var roleFeature = roleFeatures.FirstOrDefault(rf => rf.FeatureKey == f.FeatureId);
                return new RoleFeatureDto
                {
                    RoleFeatureId = roleFeature?.RoleFeatureId ?? 0,
                    RoleId = roleFeature?.RoleId ?? request.RoleId,
                    RoleName = roleName.Name,
                    FeatureKey = f.FeatureId,
                    FeatureName = f.FeatureName,
                    FeaturePath = f.Path,
                    ViewStatus = roleFeature?.ViewStatus ?? false,
                    Add = roleFeature?.Add ?? false,
                    Update = roleFeature?.Update ?? false,
                    Delete = roleFeature?.Delete ?? false,
                    Report = roleFeature?.Report ?? false,
                    ModuleId = f.ModuleId,
                    ModuleName = f.Module.Title,
                    MenuPosition = f.Module.MenuPosition
                };
            }).OrderBy(x => x.MenuPosition).ToList();


            return result;
        }
    }

}
