using Hrm.Application.DTOs.FormRecord;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormRecord.Requests.Queries
{
    public class GetFormRecordByFormIdRequest: IRequest<List<FormRecordDto>>
    {
        public int FormId { get; set; }
    }
}
