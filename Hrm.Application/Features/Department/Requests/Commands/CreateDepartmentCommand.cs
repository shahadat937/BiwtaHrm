using Hrm.Application.DTOs.Department;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Department.Requests.Commands
{
    public class CreateDepartmentCommand : IRequest<BaseCommandResponse>
    {
        public CreateDepartmentDto DepartmentDto { get; set; }
    }
}
