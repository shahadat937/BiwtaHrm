using Hrm.Application.DTOs.TaskName;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TaskName.Requests.Commands
{

    public class CreateTaskNameCommand : IRequest<BaseCommandResponse>
    {
        public CreateTaskNameDto TaskNameDto { get; set; }
    }
}
