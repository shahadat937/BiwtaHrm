using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormSchema.Requests.Commands
{
    public class DeleteFormSchemaCommand: IRequest<BaseCommandResponse>
    {
        public int SchemaId { get; set; }
    }
}
