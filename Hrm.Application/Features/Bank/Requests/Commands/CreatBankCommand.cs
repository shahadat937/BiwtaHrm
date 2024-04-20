using Hrm.Application.DTOs.Bank;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Bank.Requests.Commands
{
    public class CreateBankCommand :IRequest<BaseCommandResponse>
    {
        public CreateBankDto BankDto { get; set; }
    }
}
