using Hrm.Application.DTOs.EmpShiftAssign;
using Hrm.Application.DTOs.Employee;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpShiftAssigns.Requests.Commands
{
    public class UpdateEmpShiftAssignCommand : IRequest<BaseCommandResponse>
    {
        public CreateEmpShiftAssignDto EmpShiftAssignDto { get; set; }
    }
}

