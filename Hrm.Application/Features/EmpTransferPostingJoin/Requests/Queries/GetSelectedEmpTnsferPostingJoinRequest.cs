using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.EmpTnsferPostingJoin.Requests.Queries
{
    public class GetSelectedEmpTnsferPostingJoinRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      