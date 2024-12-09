using Hrm.Application.DTOs.EmpOtherResponsibility;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpOtherResponsibilities.Requests.Commands
{
    public class CreateEmpOtherResponsibilityCommand : IRequest<BaseCommandResponse>
    {
        public CreateEmpOtherResponsibilityDto EmpOtherResponsibilityDto { get; set; }
    }
}
