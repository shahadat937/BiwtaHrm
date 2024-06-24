using Hrm.Application.DTOs.ExamType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ExamType.Requests.Queries
{
    public class GetExamTypeByIdRequest : IRequest<ExamTypeDto>
    {
        public int ExamTypeId { get; set; }
    }
}
