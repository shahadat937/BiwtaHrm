using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FieldRecord.Requests.Commands
{
    public class DeleteFieldRecordByIdCommand: IRequest<BaseCommandResponse>
    {
        public int FieldRecordId { get; set; }
    }
}
