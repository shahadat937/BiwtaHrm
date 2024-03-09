using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.MaritalStatus.Requests.Commands
{
    public class DeleteMaritalStatusCommand : IRequest<BaseCommandResponse>
    {
        public int MaritalStatusId { get; set; }
    }
}
