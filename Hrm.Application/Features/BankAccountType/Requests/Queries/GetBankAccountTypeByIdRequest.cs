using Hrm.Application.DTOs.BankAccountType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BankAccountType.Requests.Queries
{
    public class GetBankAccountTypeByIdRequest : IRequest<BankAccountTypeDto>
    {
        public int BankAccountTypeId { get; set; }
    }
}
