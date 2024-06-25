using Hrm.Application.DTOs.GradeClass;
using Hrm.Application.DTOs.GradeType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.GradeClass.Requests.Commands
{
    public class UpdateGradeClassCommand : IRequest<BaseCommandResponse>
    {
        public required GradeClassDto GradeClassDto { get; set; }
    }
}
