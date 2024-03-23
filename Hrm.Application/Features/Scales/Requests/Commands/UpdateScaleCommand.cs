using Hrm.Application.DTOs.Scale;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Scales.Requests.Commands
{
 
    public class UpdateScaleCommand : IRequest<BaseCommandResponse>
    {
        public ScaleDto ScaleDto { get; set; }
    }
}
