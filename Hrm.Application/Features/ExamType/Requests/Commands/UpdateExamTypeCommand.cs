using Hrm.Application.DTOs.ExamType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ExamType.Requests.Commands
{
    public class UpdateExamTypeCommand : IRequest<BaseCommandResponse>
    {
        public ExamTypeDto ExamTypeDto { get; set; }
    }
}
