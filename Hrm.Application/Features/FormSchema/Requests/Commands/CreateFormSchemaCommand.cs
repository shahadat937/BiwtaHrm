using Hrm.Application.DTOs.FormSchema;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormSchema.Requests.Commands
{
    public class CreateFormSchemaCommand : IRequest<BaseCommandResponse>
    {
        public CreateFormSchemaDto FormSchemaDto { get; set; }
    }
}
