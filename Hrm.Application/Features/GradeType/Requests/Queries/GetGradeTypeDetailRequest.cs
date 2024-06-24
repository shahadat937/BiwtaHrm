
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.GradeType;

namespace Hrm.Application.Features.GradeTypes.Requests.Queries
{
    public class GetGradeTypeDetailRequest : IRequest<GradeTypeDto>
    {
        public int GradeTypeId { get; set; }
    }
}
