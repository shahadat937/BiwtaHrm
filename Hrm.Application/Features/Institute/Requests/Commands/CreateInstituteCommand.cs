using Hrm.Application.DTOs.Institute;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Institute.Requests.Commands
{
    public class CreateInstituteCommand : IRequest<BaseCommandResponse>
    {
        public CreateInstituteDto InstituteDto { get; set; }
    }
}
