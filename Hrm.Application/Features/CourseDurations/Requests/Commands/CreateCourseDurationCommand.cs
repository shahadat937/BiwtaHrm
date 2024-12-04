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
    public class CreateCourseDurationCommand : IRequest<BaseCommandResponse>
    {
        public CreateCourseDurationDto CourseDurationDto { get; set; }
    }
}
