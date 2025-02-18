using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.DTOs.FormRecord;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormRecord.Requests.Queries
{
    public class GetFormRecordRequest: IRequest<PagedResult<FormRecordDto>>
    {
        public FormRecordFilterDto Filters { get; set; }
    }
}
