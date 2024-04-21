
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.EyesColor;

namespace Hrm.Application.Features.EyesColors.Requests.Queries
{
    public class GetEyesColorDetailRequest : IRequest<EyesColorDto>
    {
        public int EyesColorId { get; set; }
    }
}
