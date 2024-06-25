using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Department.Requests.Commands
{
    public class DeleteDepartmentCommand : IRequest<BaseCommandResponse>
    {
        public int DepartmentId { get; set; }
    }
}
