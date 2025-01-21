using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.FormSection.Requests.Commands
{
    public class DeleteFormSectionByIdCommand : IRequest<BaseCommandResponse>
    {
        public int FormSectionId { get; set; }
    }
}
