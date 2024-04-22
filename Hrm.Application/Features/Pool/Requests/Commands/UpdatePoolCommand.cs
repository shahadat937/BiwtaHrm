using Hrm.Application.DTOs.Pool;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Pool.Requests.Commands
{
    public class UpdatePoolCommand : IRequest<Unit>
    {
        public PoolDto PoolDto { get; set; }
    }
}
