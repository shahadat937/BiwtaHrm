
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.DTOs.Religion;
using Hrm.Application.DTOs.EmployeeType;
using Hrm.Application.DTOs.Gender;

namespace Hrm.Application.Features.Gender.Requests.Queries
{
    public class GetGenderByIdRequest : IRequest<GenderDto>
    {
        public int GenderId { get; set; }
    }
}
