using Hrm.Application.DTOs.FormFieldType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormFieldType.Requests.Queries
{
    public class GetFormFieldTypeRequest: IRequest<List<FormFieldTypeDto>>
    {

    }
}
