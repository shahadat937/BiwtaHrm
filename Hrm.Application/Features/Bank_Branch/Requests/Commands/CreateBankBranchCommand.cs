using Hrm.Application.DTOs.BankBranch;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BankBranch.Requests.Commands
{
    public class CreateBankBranchCommand : IRequest<BaseCommandResponse>
    {
        public CreateBankBranchDto BankBranchDto { get; set; }
    }
}
