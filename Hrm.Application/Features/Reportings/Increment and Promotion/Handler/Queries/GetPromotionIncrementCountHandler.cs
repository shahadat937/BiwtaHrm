using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.Reportings.Increment_and_Promotion.Request.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.Increment_and_Promotion.Handler.Queries
{
    public class GetPromotionIncrementCountHandler : IRequestHandler<GetPromotionIncrementReportingCountRequest, PagedResult<IncrementPromotionCountDto>>
    {
        private readonly IHrmRepository<EmpPromotionIncrement> _EmpPromotionIncrementRepository;

        public GetPromotionIncrementCountHandler(IHrmRepository<EmpPromotionIncrement> EmpPromotionIncrementRepository)
        {
            _EmpPromotionIncrementRepository = EmpPromotionIncrementRepository;
        }

        public async Task<PagedResult<IncrementPromotionCountDto>> Handle(GetPromotionIncrementReportingCountRequest request, CancellationToken cancellationToken)
        {
            IQueryable<EmpPromotionIncrement> query = _EmpPromotionIncrementRepository.FilterWithInclude(x =>
           (request.CurrentDepartmentId == 0 || x.CurrentDepartmentId == request.CurrentDepartmentId) &&

           (request.OrderDateFrom == null || x.OrderDate >= request.OrderDateFrom) &&
            (request.OrderDateTo == null || x.OrderDate <= request.OrderDateTo) &&

            (request.ApproveFrom == null || x.ApproveDate >= request.ApproveFrom) &&
            (request.ApproveTo == null || x.ApproveDate <= request.ApproveTo) &&

            (request.EffectiveDateFrom == null || x.EffectiveDate >= request.EffectiveDateFrom) &&
            (request.EffectiveDateTo == null || x.EffectiveDate <= request.EffectiveDateTo) &&

           //(request.ApproveDate == null || x.ApproveDate.Date == request.ApproveDate.Value.Date) &&
           //(request.EffectiveDate == null || x.EffectiveDate.Date == request.EffectiveDate.Value.Date) &&
           (string.IsNullOrEmpty(request.PromotionType) || x.PromotionIncrementType == request.PromotionType)
       );

            var resultData = new IncrementPromotionCountDto
            {
                TotalPromotionCount = await query.CountAsync(x => x.PromotionIncrementType == "Promotion", cancellationToken),
                TotalIncrementCount = await query.CountAsync(x => x.PromotionIncrementType == "Increment", cancellationToken),
                TotalUpdateDesgnation = await query.CountAsync(x => x.UpdateDesignationId != null, cancellationToken),
              
                TotalIncrementPromotion = await query.CountAsync(x => x.PromotionIncrementType == "Promotion" || x.PromotionIncrementType == "Increment", cancellationToken),
                Totals = await query.CountAsync(cancellationToken),
                TotalApprove = await query.CountAsync(x => x.ApplicationStatus == true, cancellationToken),
                TotalReject = await query.CountAsync(x => x.ApplicationStatus == false, cancellationToken),
                TotalPending = await query.CountAsync(x => x.ApplicationStatus == null, cancellationToken)
            };

            return new PagedResult<IncrementPromotionCountDto>(new List<IncrementPromotionCountDto> { resultData }, 1, 1, 1);
        }
    }

}
