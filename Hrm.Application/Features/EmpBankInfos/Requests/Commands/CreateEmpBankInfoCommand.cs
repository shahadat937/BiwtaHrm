using Hrm.Application.DTOs.EmpBankInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBankInfos.Requests.Commands
{
    public class CreateEmpBankInfoCommand : IRequest<BaseCommandResponse>
    {
        public List<CreateEmpBankInfoDto> EmpBankInfoDto { get; set; }
    }
}
