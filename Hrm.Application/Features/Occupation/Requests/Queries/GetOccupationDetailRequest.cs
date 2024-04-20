
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.Occupation;

namespace Hrm.Application.Features.Occupations.Requests.Queries
{
    public class GetOccupationDetailRequest : IRequest<OccupationDto>
    {
        public int OccupationId { get; set; }
    }
}
