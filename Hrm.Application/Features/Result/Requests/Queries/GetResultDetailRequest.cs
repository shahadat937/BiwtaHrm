
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.Result;

namespace Hrm.Application.Features.Results.Requests.Queries
{
    public class GetResultDetailRequest : IRequest<ResultDto>
    {
        public int ResultId { get; set; }
    }
}
