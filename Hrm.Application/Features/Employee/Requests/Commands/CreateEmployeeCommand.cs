using Hrm.Application.DTOs.Employee;
using Hrm.Application.DTOs.EmployeeType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Employee.Requests.Commands
{
    public class CreateEmployeeCommand : IRequest<BaseCommandResponse>
    {
        public CreateEmployeesDto EmployeeDto { get; set; }
    }
}
