using Hrm.Application.DTOs.EmpPersonalInfo;
using Hrm.Application.DTOs.EmpFingerPrint;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpFingerPrints.Requests.Commands
{
    public class CreateEmpFingerPrintCommand : IRequest<BaseCommandResponse>
    {
        public CreateEmpFingerPrintDto EmpFingerPrintDto { get; set; }
    }
}