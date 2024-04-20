using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Occupation.Requests.Commands
{
    public class DeleteOccupationCommand : IRequest<BaseCommandResponse>
    {
        public int OccupationId { get; set; }
    }
}
