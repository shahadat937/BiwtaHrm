using Hrm.Application.DTOs.Common;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmpInfoReporting.Increment_and_Promotion.Request.Queries
{
    public class GetPromotionIncrementReportingCountRequest : IRequest<PagedResult<IncrementPromotionCountDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int CurrentDepartmentId { get; set; }
        public DateOnly? OrderDateFrom { get; set; }
        public DateOnly? OrderDateTo { get; set; }
        public DateOnly? ApproveFrom { get; set; }
        public DateOnly? ApproveTo { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateOnly? EffectiveDateFrom { get; set; }
        public DateOnly? EffectiveDateTo { get; set; }
        public String? PromotionType {  get; set; }
    }
}
