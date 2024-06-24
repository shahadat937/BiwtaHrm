
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.Pool;

namespace Hrm.Application.Features.Pools.Requests.Queries
{
    public class GetPoolDetailRequest : IRequest<PoolDto>
    {
        public int PoolId { get; set; }
    }
}
