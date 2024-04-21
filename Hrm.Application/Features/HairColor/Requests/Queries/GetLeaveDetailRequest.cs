
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.HairColor;

namespace Hrm.Application.Features.HairColors.Requests.Queries
{
    public class GetHairColorDetailRequest : IRequest<HairColorDto>
    {
        public int HairColorId { get; set; }
    }
}
