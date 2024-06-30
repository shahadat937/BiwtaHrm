using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBasicInfos.Requests.Commands
{
    public class UpdateUserStatusCommand : IRequest<object>
    {
        public int Id { get; set; }
    }
}
