
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.DTOs.Religion;

namespace Hrm.Application.Features.Religion.Requests.Queries
{
    public class GetReligionByIdRequest : IRequest<ReligionDto>
    {
        public int ReligionId { get; set; }
    }
}
