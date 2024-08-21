using Hrm.Application.DTOs.FormSchema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormSchema.Requests.Queries
{
    public class GetFormSchemaByFilterRequest: IRequest<List<FormSchemaDto>>
    {
        public FormSchemaFilterDto filters { get; set; }
    }
}
