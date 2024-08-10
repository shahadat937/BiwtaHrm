using Hrm.Application.DTOs.EmpPromotionIncrement;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPromotionIncrements.Requests.Commands
{
    public class UpdateEmpPromotionIncrementCommand : IRequest<BaseCommandResponse>
    {
        public CreateEmpPromotionIncrementDto EmpPromotionIncrementDto { get; set; }
    }
}
