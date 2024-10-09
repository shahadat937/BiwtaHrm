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
    public class CreateBloodCommand : IRequest<BaseCommandResponse>
    {
        public CreateDesignationSetupDto DesignationSetupDto { get; set; }
    }
}
