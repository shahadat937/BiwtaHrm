using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.appraisalFormType.Requests.Commands
{
    public class DeleteappraisalFormTypeCommand : IRequest<BaseCommandResponse>
    {
        public int appraisalFormTypeId { get; set; }
    }
}
