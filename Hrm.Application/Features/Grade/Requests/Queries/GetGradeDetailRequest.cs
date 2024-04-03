
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.Grade;

namespace Hrm.Application.Features.Grades.Requests.Queries
{
    public class GetGradeDetailRequest : IRequest<GradeDto>
    {
        public int GradeId { get; set; }
    }
}
