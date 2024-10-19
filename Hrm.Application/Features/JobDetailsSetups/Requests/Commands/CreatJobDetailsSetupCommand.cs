using Hrm.Application.DTOs.JobDetailsSetup;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.JobDetailsSetups.Requests.Commands
{
    public class CreateJobDetailsSetupCommand :IRequest<BaseCommandResponse>
    {
        public CreateJobDetailsSetupDto JobDetailsSetupDto { get; set; }
    }
}
