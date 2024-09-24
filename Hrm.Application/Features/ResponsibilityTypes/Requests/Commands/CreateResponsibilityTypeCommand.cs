using Hrm.Application.DTOs.ResponsibilityType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ResponsibilityType.Requests.Commands
{
    public class CreateResponsibilityTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateResponsibilityTypeDto ResponsibilityTypeDto { get; set; }
    }
}
