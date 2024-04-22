using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.Pool;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Pool.Requests.Commands
{
    public class CreatePoolCommand : IRequest<BaseCommandResponse>
    {
        public CreatePoolDto PoolDto { get; set; }
    }
}
