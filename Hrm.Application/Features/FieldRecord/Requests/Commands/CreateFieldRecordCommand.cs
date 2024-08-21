using Hrm.Application.DTOs.FieldRecord;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FieldRecord.Requests.Commands
{
    public class CreateFieldRecordCommand: IRequest<BaseCommandResponse>
    {
        public CreateFieldRecordDto FieldRecordDto { get; set; }
    }
}
