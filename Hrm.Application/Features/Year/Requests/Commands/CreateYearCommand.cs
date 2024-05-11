using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.Year;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Year.Requests.Commands
{
    public class CreateYearCommand : IRequest<BaseCommandResponse>
    {
        public CreateYearDto YearDto { get; set; }
    }
}
