using Hrm.Application.DTOs.FormRecord;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormRecord.Requests.Commands
{
    public class CreateFormRecordCommand: IRequest<BaseCommandResponse>
    {
        public CreateFormRecordDto FormRecordDto { get; set; }
    }
}
