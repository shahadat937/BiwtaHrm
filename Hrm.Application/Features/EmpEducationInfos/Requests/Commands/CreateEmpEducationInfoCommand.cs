using Hrm.Application.DTOs.EmpEducationInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpEducationInfos.Requests.Commands
{
    public class CreateEmpEducationInfoCommand : IRequest<BaseCommandResponse>
    {
        public List<CreateEmpEducationInfoDto> EmpEducationInfoDto { get; set; }
    }
}
