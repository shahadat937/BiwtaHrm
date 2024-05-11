
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.Year;

namespace Hrm.Application.Features.Year.Requests.Queries
{
    public class GetYearDetailRequest : IRequest<YearDto>
    {
        public int YearId { get; set; }
    }
}
