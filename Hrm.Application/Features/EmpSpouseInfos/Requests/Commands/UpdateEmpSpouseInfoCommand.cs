using Hrm.Application.DTOs.EmpJobDetail;
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
    public class UpdateEmpSpouseInfoCommand : IRequest<BaseCommandResponse>
    {
        public EmpSpouseInfoDto EmpSpouseInfoDto { get; set; }
    }
}
