using Hrm.Application.DTOs.Pool;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Pool.Requests.Commands
{
    public class UpdatePoolCommand : IRequest<BaseCommandResponse>
    {
        public required PoolDto PoolDto { get; set; }
    }
}
