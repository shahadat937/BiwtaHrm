using Hrm.Application.DTOs.FormField;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormField.Requests.Commands
{
    public class UpdateFormFieldCommand: IRequest<BaseCommandResponse>
    {
        public FormFieldDto formFieldDto { get; set; }
    }
}
