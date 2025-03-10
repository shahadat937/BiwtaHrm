﻿using Hrm.Application.DTOs.FormField;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormField.Requests.Queries
{
    public class GetFormFieldByIdRequest: IRequest<FormFieldDto>
    {
        public int FieldId { get; set; }
    }
}
