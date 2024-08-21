using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormRecord.Requests.Commands
{
    public class DeleteFormRecordCommand: IRequest<BaseCommandResponse>
    {
        public int RecordId { get; set; }
    }
}
