using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.CourseDuration;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.CourseDurations.Requests.Commands
{
    public class UpdateCourseDurationCommand : IRequest<BaseCommandResponse>
    {
        public CourseDurationDto CourseDurationDto { get; set; }
    }
}
