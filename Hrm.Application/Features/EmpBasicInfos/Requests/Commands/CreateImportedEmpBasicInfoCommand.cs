using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBasicInfos.Requests.Commands
{
    public class CreateImportedEmpBasicInfoCommand : IRequest<BaseCommandResponse>
    {
        public List<CreateEmpBasicInfoDto> EmpBasicInfoDtos { get; set; }
    }
}
