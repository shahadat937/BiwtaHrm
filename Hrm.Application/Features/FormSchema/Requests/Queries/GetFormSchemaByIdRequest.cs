using Hrm.Application.DTOs.FormSchema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormSchema.Requests.Queries
{
    public class GetFormSchemaByIdRequest: IRequest<FormSchemaDto>
    {
        public int SchemaId { get; set; }
    }
}
