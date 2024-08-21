using Hrm.Application.DTOs.FieldRecord;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FieldRecord.Requests.Queries
{
    public class GetFieldRecordByFormRecordIdRequest: IRequest<List<FieldRecordDto>>
    {
        public int FormRecordId { get; set; }
    }
}
