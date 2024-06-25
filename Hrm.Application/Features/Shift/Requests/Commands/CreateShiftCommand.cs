using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.Shift;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Shift.Requests.Commands
{
    public class CreateShiftCommand : IRequest<BaseCommandResponse>
    {
        public CreateShiftDto ShiftDto { get; set; }
    }
}
