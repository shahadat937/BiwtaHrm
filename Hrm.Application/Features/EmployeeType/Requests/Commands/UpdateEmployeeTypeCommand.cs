using Hrm.Application.DTOs.EmployeeType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmployeeType.Requests.Commands
{
    public class UpdateEmployeeTypeCommand : IRequest<Unit>
    {
        public EmployeeTypeDto EmployeeTypeDto { get; set; }
    }
}
