
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.Scale;

namespace Hrm.Application.Features.Scales.Requests.Queries
{
    public class GetScaleDetailRequest : IRequest<ScaleDto>
    {
        public int ScaleId { get; set; }
    }
}
