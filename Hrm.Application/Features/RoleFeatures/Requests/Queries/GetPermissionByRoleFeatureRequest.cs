﻿using Hrm.Application.DTOs.RoleFeatures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RoleFeatures.Requests.Queries
{
    public class GetPermissionByRoleFeatureRequest : IRequest<FeaturePermissionDto>
    {
        public string RoleName { get; set; }
        public string FeaturePath { get; set; }
    }
}
