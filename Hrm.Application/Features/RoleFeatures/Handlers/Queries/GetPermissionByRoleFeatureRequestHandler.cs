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
        public GetPermissionByRoleFeatureRequestHandler(IHrmRepository<RoleFeature> RoleFeaturesRepository)
        {
            _RoleFeaturesRepository = RoleFeaturesRepository;
        }

        public async Task<FeaturePermissionDto> Handle(GetPermissionByRoleFeatureRequest request, CancellationToken cancellationToken)
        {
            var roleFeature = await _RoleFeaturesRepository.FindOneAsync(x => x.RoleName == request.RoleName && x.FeaturePath == request.FeaturePath);

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
