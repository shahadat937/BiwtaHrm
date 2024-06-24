using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.BankBranch.Requests.Queries
{
    public class GetSelectedBankBranchRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      