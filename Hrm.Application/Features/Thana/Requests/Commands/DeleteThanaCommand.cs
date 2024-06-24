using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana.Requests.Commands
{
    public class DeleteThanaCommand : IRequest<BaseCommandResponse>
    {
        public int ThanaId { get; set; }
    }
}
