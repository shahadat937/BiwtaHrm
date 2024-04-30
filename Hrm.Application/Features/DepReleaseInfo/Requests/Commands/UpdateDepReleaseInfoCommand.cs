using Hrm.Application.DTOs.DepReleaseInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DepReleaseInfo.Requests.Commands
{
    public class UpdateDepReleaseInfoCommand : IRequest<BaseCommandResponse>
    {
        public DepReleaseInfoDto DepReleaseInfoDto { get; set; }
    }
}
