using Hrm.Application.DTOs.Religion;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Religion.Requests.Commands
{
    public class UpdateReligionCommand : IRequest<Unit>
    {
        public ReligionDto ReligionDto { get; set; }
    }
}
