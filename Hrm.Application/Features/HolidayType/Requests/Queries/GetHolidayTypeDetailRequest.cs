
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.Year;
using Hrm.Application.DTOs.HolidayType;

namespace Hrm.Application.Features.Year.Requests.Queries
{
    public class GetHolidayTypeDetailRequest : IRequest<HolidayTypeDto>
    {
        public int HolidayTypeId { get; set; }
    }
}
