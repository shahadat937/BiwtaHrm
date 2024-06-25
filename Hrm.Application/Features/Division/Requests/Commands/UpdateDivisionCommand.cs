using Hrm.Application.DTOs.Division;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Division.Requests.Commands
{
    public class UpdateDivisionCommand : IRequest<BaseCommandResponse>
    {
        public DivisionDto DivisionDto { get; set; }
    }
}
