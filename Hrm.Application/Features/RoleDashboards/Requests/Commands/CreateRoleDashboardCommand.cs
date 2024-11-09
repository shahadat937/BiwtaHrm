using Hrm.Application.DTOs.RoleDashboard;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RoleDashboards.Requests.Commands
{
    public class CreateRoleDashboardCommand : IRequest<BaseCommandResponse>
    {
        public List<CreateRoleDashboardDto> RoleDashboardDtos { get; set; }
    }
}
