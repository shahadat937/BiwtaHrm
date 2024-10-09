using Hrm.Application.DTOs.DesignationSetup;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DesignationSetups.Requests.Commands
{
    public class UpdateDesignationSetupCommand : IRequest<BaseCommandResponse>
    {
        public DesignationSetupDto DesignationSetupDto { get; set; }
    }
}
