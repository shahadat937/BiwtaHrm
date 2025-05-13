using Hrm.Application.DTOs.EmpPersonalInfo;
using Hrm.Application.DTOs.EmpJobDetail;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpJobDetails.Requests.Commands
{
    public class UpdateEmpJobDetailCommand : IRequest<BaseCommandResponse>
    {
        public CreateEmpJobDetailDto EmpJobDetailDto { get; set; }
    }
}

