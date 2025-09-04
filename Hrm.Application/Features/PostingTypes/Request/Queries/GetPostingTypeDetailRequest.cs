
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.PostingType;

namespace Hrm.Application.Features.PostingTypes.Request.Queries
{
    public class GetPostingTypeDetailRequest : IRequest<PostingTypeDto>
    {
        public int PostingTypeId { get; set; }
    }
}
