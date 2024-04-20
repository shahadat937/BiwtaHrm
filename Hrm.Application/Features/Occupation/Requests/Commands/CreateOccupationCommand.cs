using Hrm.Application.DTOs.Occupation;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Occupation.Requests.Commands
{
    public class CreateBloodCommand : IRequest<BaseCommandResponse>
    {
        public CreateOccupationDto OccupationDto { get; set; }
    }
}
