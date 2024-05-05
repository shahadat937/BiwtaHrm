using Hrm.Application.DTOs.Bank;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Bank.Requests.Queries
{
    public class GetBankByIdRequest : IRequest<BankDto>
    {
        public int BankId { get; set; }
    }
}
