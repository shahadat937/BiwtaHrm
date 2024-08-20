using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.RoleFeatures;
using Hrm.Application.Features.RoleFeatures.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RoleFeatures.Handlers.Queries
{
    public class GetPermissionByRoleFeatureRequestHandler : IRequestHandler<GetPermissionByRoleFeatureRequest, FeaturePermissionDto>
    {
        private readonly IHrmRepository<RoleFeature> _RoleFeaturesRepository;
        private readonly IHrmRepository<Feature> _FeaturesRepository;
        private readonly IHrmRepository<AspNetRoles> _AspNetRolesRepository;
        public GetPermissionByRoleFeatureRequestHandler(IHrmRepository<RoleFeature> RoleFeaturesRepository, IHrmRepository<Feature> FeaturesRepository, IHrmRepository<AspNetRoles> AspNetRolesRepository)
        {
            _RoleFeaturesRepository = RoleFeaturesRepository;
            _AspNetRolesRepository = AspNetRolesRepository;
            _FeaturesRepository = FeaturesRepository;
        }

        public async Task<FeaturePermissionDto> Handle(GetPermissionByRoleFeatureRequest request, CancellationToken cancellationToken)
        {
            var roleId = await _AspNetRolesRepository.FindOneAsync(x => x.Name == request.RoleName);
            var featureId = await _FeaturesRepository.FindOneAsync(x => x.Path == request.FeaturePath);

            var roleFeature = await _RoleFeaturesRepository.FindOneAsync(x => x.RoleId == roleId.Id && x.FeatureKey == featureId.FeatureId);

            var result = new FeaturePermissionDto
            {
                ViewStatus = roleFeature.ViewStatus,
                Add = roleFeature.Add,
                Update = roleFeature.Update,
                Delete = roleFeature.Delete,
            };

            return result;
        }
    }
}
