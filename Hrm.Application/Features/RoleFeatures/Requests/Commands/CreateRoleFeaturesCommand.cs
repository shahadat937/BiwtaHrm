using Hrm.Application.DTOs.RoleFeatures;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RoleFeatures.Requests.Commands
{
    public class CreateRoleFeaturesCommand : IRequest<BaseCommandResponse>
    {
        public List<CreateRoleFeatureDto> RoleFeatureDtos { get; set; }
    }
}
