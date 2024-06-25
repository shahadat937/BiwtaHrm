using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.UserRoles.Requests.Queries
{
    public class GetSelectedUserRoleRequest : IRequest<List<SelectedModel>>
    {

    }
} 
      