using Hrm.Application.DTOs.EmpPersonalInfo;
using Hrm.Application.DTOs.Employee;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPersonalInfos.Requests.Commands
{
    public class CreateEmpPersonalInfoCommand : IRequest<BaseCommandResponse>
    {
        public CreateEmpPersonalInfoDto EmpPersonalInfoDto { get; set; }
    }
}

