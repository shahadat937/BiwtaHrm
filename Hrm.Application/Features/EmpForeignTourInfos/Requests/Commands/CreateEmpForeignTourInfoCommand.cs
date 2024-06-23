using Hrm.Application.DTOs.EmpForeignTourInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpForeignTourInfos.Requests.Commands
{
    public class CreateEmpForeignTourInfoCommand : IRequest<BaseCommandResponse>
    {
        public List<CreateEmpForeignTourInfoDto> EmpForeignTourInfoDto { get; set; }
    }
}
