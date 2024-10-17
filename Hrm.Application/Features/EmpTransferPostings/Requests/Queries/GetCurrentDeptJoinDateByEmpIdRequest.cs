using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTransferPostings.Requests.Queries
{
    public class GetCurrentDeptJoinDateByEmpIdRequest : IRequest<object>
    {
        public int EmpId { get; set; }
    }
}
