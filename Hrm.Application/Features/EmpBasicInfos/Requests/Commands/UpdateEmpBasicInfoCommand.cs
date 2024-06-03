using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.DTOs.Employee;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBasicInfos.Requests.Commands
{
    public class UpdateEmpBasicInfoCommand : IRequest<BaseCommandResponse>
    {
        public EmpBasicInfoDto EmpBasicInfoDto { get; set; }
    }
}

