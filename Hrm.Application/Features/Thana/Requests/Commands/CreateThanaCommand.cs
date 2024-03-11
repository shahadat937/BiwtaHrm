using Hrm.Application.DTOs.Thana;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana.Requests.Commands
{
    public class CreateThanaCommand : IRequest<BaseCommandResponse>
    {
        public CreateThanaDto ThanaDto { get; set; }
    }
}
