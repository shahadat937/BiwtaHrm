using Hrm.Application.DTOs.EmpSpouseInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpSpouseInfos.Requests.Commands
{
    public class CreateEmpSpouseInfoCommand : IRequest<BaseCommandResponse>
    {
        public List<CreateEmpSpouseInfoDto> EmpSpouseInfoDto { get; set; }
    }
}
