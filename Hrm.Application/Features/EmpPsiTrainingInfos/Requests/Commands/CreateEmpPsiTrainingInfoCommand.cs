using Hrm.Application.DTOs.EmpPsiTrainingInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPsiTrainingInfos.Requests.Commands
{
    public class CreateEmpPsiTrainingInfoCommand : IRequest<BaseCommandResponse>
    {
        public List<CreateEmpPsiTrainingInfoDto> EmpPsiTrainingInfoDto { get; set; }
    }
}
