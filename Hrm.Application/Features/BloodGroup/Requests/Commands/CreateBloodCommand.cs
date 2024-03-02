using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BloodGroup.Requests.Commands
{
    public class CreateBloodCommand : IRequest<BaseCommandResponse>
    {
        public CreateBloodGroupDto BloodGroupDto { get; set; }
    }
}
