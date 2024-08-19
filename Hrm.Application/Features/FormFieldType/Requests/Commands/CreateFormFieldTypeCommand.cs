using Hrm.Application.DTOs.FormFieldType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormFieldType.Requests.Commands
{
    public class CreateFormFieldTypeCommand: IRequest<BaseCommandResponse>
    {
        public CreateFormFieldTypeDto formFieldTypeDto { get; set; }
    }
}
