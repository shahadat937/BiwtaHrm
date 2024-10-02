
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.CourseDuration;

namespace Hrm.Application.Features.CourseDurations.Requests.Queries
{
    public class GetCourseDurationDetailRequest : IRequest<CourseDurationDto>
    {
        public int CourseDurationId { get; set; }
    }
}
