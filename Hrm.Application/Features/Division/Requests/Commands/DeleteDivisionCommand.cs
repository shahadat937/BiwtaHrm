using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Division.Requests.Commands
{
    public class DeleteDivisionCommand : IRequest<BaseCommandResponse>
    {
        public int DivisionId { get; set; }
    }
}
