using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.Religion;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Religion.Requests.Commands
{
    public class CreateReligionCommand : IRequest<BaseCommandResponse>
    {
        public CreateReligionDto ReligionDto { get; set; }
    }
}
