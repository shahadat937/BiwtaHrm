using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.MaritalStatus.Requests.Commands
{
    public class DeleteMaritalStatusCommand : IRequest
    {
        public int MaritalStatusId { get; set; }
    }
}
