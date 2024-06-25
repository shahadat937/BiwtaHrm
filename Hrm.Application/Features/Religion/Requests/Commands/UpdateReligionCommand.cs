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
    public class UpdateReligionCommand : IRequest<BaseCommandResponse>
    {
        public ReligionDto ReligionDto { get; set; }
    }
}
