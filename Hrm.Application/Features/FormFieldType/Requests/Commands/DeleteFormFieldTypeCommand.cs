using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormFieldType.Requests.Commands
{
    public class DeleteFormFieldTypeCommand : IRequest<BaseCommandResponse>
    {
        public int FieldTypeId { get; set; }
    }
}
