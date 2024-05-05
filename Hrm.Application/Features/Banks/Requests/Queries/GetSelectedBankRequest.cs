using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Bank.Requests.Queries
{
    public class GetSelectedBankRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      