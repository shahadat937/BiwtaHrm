using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.ReleaseType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ReleaseTypes.Requests.Commands
{
    public class CreateReleaseTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateReleaseTypeDto ReleaseTypeDto { get; set; }
    }
}
