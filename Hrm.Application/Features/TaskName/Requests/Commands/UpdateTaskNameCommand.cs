using Hrm.Application.DTOs.TaskName;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TaskName.Requests.Commands
{
 
    public class UpdateTaskNameCommand : IRequest<Unit>
    {
        public TaskNameDto TaskNameDto { get; set; }

    }

}
