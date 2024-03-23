using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TaskName.Requests.Commands
{

    public class DeleteTaskNameCommand : IRequest
    {
        public int TaskNameId { get; set; }
    }
}
