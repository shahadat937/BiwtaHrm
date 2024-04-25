using Hrm.Application.DTOs.BankBranch;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BankBranch.Requests.Queries
{
    public class GetBankBranchByIdRequest : IRequest<BankBranchDto>
    {
        public int BankBranchId { get; set; }
    }
}
