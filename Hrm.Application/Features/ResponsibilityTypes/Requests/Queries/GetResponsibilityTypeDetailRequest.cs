
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.ResponsibilityType;

namespace Hrm.Application.Features.ResponsibilityTypes.Requests.Queries
{
    public class GetResponsibilityTypeDetailRequest : IRequest<ResponsibilityTypeDto>
    {
        public int ResponsibilityTypeId { get; set; }
    }
}
