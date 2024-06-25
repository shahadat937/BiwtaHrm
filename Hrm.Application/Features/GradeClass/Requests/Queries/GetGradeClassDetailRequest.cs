
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.GradeClass;

namespace Hrm.Application.Features.GradeClasss.Requests.Queries
{
    public class GetGradeClassDetailRequest : IRequest<GradeClassDto>
    {
        public int GradeClassId { get; set; }
    }
}
