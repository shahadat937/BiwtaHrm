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
    public class UpdateReleaseTypeCommand : IRequest<BaseCommandResponse>
    {
        public ReleaseTypeDto ReleaseTypeDto { get; set; }
    }
}
