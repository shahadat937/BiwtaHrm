using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.BankAccountType.Requests.Queries
{
    public class GetSelectedBankAccountTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      