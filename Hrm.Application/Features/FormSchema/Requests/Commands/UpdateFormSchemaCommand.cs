﻿using Hrm.Application.DTOs.FormSchema;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormSchema.Requests.Commands
{
    public class UpdateFormSchemaCommand: IRequest<BaseCommandResponse>
    {
        public FormSchemaDto FormSchemaDto { get; set; }
    }
}
