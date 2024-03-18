using Hrm.Application.DTOs.Grade;
using Hrm.Application.DTOs.GradeType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Grade.Requests.Commands
{
    public class UpdateGradeCommand : IRequest<BaseCommandResponse>
    {
        public required GradeDto GradeDto { get; set; }
    }
}
