using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BankBranch.Requests.Commands
{
    public class DeleteBankBranchCommand : IRequest<BaseCommandResponse>
    {
        public int BankBranchId { get; set; }
    }
}
