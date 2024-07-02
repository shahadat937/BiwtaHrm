using Hrm.Application.DTOs.DayType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DayType.Requests.Commands
{
    public class CreateDayTypeCommand:IRequest<BaseCommandResponse>
    {
        public CreateDayTypeDto DayTypeDto { get; set; }
    }
}
