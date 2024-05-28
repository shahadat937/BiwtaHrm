using Hrm.Application.DTOs.Employee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Employee.Requests.Queries
{
    public class GetEmployeeByAspNetUserIdRequest : IRequest<EmployeesDto>
    {
        public string AspNetUserId { get; set; }
    }
}