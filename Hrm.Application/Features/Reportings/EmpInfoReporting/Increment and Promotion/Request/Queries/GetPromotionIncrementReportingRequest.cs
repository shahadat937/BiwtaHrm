using Hrm.Application.DTOs.Common;
using Hrm.Application.DTOs.EmpPromotionIncrement;
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
    public class GetPromotionIncrementReportingRequest : IRequest<PagedResult<EmpPromotionIncrementDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int? EmpId { get; set; }
        public int? CurrentDepartmentId { get; set; }
        public int? CurrentDesignationId { get; set; }
        public int? CurrentGradeId { get; set; }
        public int? CurrentScaleId { get; set; }
        public int? UpdateGradeId { get; set; }
        public int? UpdateScaleId { get; set; }
        public string? PromotionIncrementType { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public bool? isApproval { get; set; }

    }
}
