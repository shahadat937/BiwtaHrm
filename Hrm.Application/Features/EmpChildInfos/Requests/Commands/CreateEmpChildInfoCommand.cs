using Hrm.Application.DTOs.EmpChildInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpChildInfos.Requests.Commands
{
    public class CreateEmpChildInfoCommand : IRequest<BaseCommandResponse>
    {
        public List<CreateEmpChildInfoDto> EmpChildInfoDto { get; set; }
    }
}
