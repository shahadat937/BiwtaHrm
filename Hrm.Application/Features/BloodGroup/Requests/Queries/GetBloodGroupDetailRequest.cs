
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.BloodGroup;

namespace Hrm.Application.Features.BloodGroups.Requests.Queries
{
    public class GetBloodGroupDetailRequest : IRequest<BloodGroupDto>
    {
        public int BloodGroupId { get; set; }
    }
}
