using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Result.Requests.Commands
{
    public class DeleteResultCommand : IRequest<BaseCommandResponse>
    {
        public int ResultId { get; set; }
    }
}
