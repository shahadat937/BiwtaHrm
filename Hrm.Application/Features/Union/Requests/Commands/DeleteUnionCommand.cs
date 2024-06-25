using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Union.Requests.Commands
{
    public class DeleteUnionCommand : IRequest<BaseCommandResponse>
    {
        public int UnionId { get; set; }
    }
}
