using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DepReleaseInfo.Requests.Commands
{
    public class DeleteDepReleaseInfoCommand : IRequest<BaseCommandResponse>
    {
        public int DepReleaseInfoId { get; set; }
    }
}
