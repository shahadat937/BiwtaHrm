
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.DTOs.Religion;
using Hrm.Application.DTOs.EmployeeType;

namespace Hrm.Application.Features.EmployeeType.Requests.Queries
{
    public class GetEmployeeTypeByIdRequest : IRequest<EmployeeTypeDto>
    {
        public int EmployeeTypeId { get; set; }
    }
}
