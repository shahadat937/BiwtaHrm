using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTnsferPostingJoin.Requests.Commands
{
    public class DeleteEmpTnsferPostingJoinCommand : IRequest<BaseCommandResponse>
    {
        public int EmpTnsferPostingJoinId { get; set; }
    }
}
