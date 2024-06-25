using Hrm.Application.DTOs.appraisalFormType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.appraisalFormType.Requests.Commands
{
    public class CreateappraisalFormTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateappraisalFormTypeDto appraisalFormTypeDto { get; set; }
    }
}
