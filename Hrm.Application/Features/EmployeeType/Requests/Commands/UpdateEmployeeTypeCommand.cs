using Hrm.Application.DTOs.EmployeeType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmployeeType.Requests.Commands
{
    public class UpdateEmployeeTypeCommand : IRequest<BaseCommandResponse>
    {
        public EmployeeTypeDto EmployeeTypeDto { get; set; }
    }
}
