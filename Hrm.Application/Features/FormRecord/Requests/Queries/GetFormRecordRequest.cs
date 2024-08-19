using Hrm.Application.DTOs.FormRecord;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormRecord.Requests.Queries
{
    public class GetFormRecordRequest: IRequest<List<FormRecordDto>>
    {

    }
}
