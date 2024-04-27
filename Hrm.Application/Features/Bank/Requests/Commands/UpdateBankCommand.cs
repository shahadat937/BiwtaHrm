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
    public class UpdateExamTypeCommand : IRequest<BaseCommandResponse>
    {
        public BankDto BankDto { get; set; }
    }
}
