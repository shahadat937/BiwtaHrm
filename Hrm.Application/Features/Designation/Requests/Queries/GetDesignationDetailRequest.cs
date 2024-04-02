
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.Designation;

namespace Hrm.Application.Features.Designations.Requests.Queries
{
    public class GetDesignationDetailRequest : IRequest<DesignationDto>
    {
        public int DesignationId { get; set; }
    }
}
