using Hrm.Application.DTOs.EmpTnsferPostingJoin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTnsferPostingJoin.Requests.Queries
{
    public class GetEmpTnsferPostingJoinByIdRequest : IRequest<EmpTnsferPostingJoinDto>
    {
        public int EmpTnsferPostingJoinId { get; set; }
    }
}
