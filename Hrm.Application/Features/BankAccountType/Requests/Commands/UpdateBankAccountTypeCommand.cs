using Hrm.Application.DTOs.BankAccountType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BankAccountType.Requests.Commands
{
    public class UpdateBankAccountTypeCommand : IRequest<BaseCommandResponse>
    {
        public BankAccountTypeDto BankAccountTypeDto { get; set; }
    }
}
