using Hrm.Application.DTOs.Union;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Union.Requests.Commands
{
    public class CreateUnionCommand : IRequest<BaseCommandResponse>
    {
        public CreateUnionDto UnionDto { get; set; }
    }
}
