using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.FormGroup;
using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.FormGroup.Requests.Commands
{
    public class UpdateFormGroupByIdCommand : IRequest<BaseCommandResponse>
    {
        public FormGroupDto FormGroup { get; set; }
    }
}
