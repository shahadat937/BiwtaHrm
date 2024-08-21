using Hrm.Application.DTOs.FieldRecord;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FieldRecord.Requests.Queries
{
    public class GetFieldRecordByIdRequest: IRequest<FieldRecordDto>
    {
        public int FieldRecordId { get; set; }
    }
}
