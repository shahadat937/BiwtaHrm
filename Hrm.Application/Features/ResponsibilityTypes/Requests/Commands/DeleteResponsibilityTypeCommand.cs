using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ResponsibilityType.Requests.Commands
{
    public class DeleteResponsibilityTypeCommand : IRequest<BaseCommandResponse>
    {
        public int ResponsibilityTypeId { get; set; }
    }
}
