using Hrm.Application.DTOs.EmpTrainingInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTrainingInfos.Requests.Commands
{
    public class CreateEmpTrainingInfoCommand : IRequest<BaseCommandResponse>
    {
        public List<CreateEmpTrainingInfoDto> EmpTrainingInfoDto { get; set; }
    }
}
