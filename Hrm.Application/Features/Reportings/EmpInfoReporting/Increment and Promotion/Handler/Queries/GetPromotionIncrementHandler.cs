using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpPromotionIncrement;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.Reportings.EmpInfoReporting.EmployeeTypes.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Increment_and_Promotion.Request.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmpInfoReporting.Increment_and_Promotion.Handler.Queries
{
    public class GetPromotionIncrementHandler : IRequestHandler<GetPromotionIncrementReportingRequest, PagedResult<EmpPromotionIncrementDto>>
    {
        private readonly IHrmRepository<EmpPromotionIncrement> _EmpPromotionIncrementRepository;

        public GetPromotionIncrementHandler(IHrmRepository<EmpPromotionIncrement> empPromotionIncrementRepository)
        {
            _EmpPromotionIncrementRepository = empPromotionIncrementRepository;
        }

        public async Task<PagedResult<EmpPromotionIncrementDto>> Handle(GetPromotionIncrementReportingRequest request, CancellationToken cancellationToken)
        {
           
            IQueryable<EmpPromotionIncrement> query = _EmpPromotionIncrementRepository.FilterWithInclude(x =>
                (request.EmpId == null || x.EmpId == request.EmpId) &&  
                (request.CurrentDepartmentId == null || x.CurrentDepartmentId == request.CurrentDepartmentId) && 
                (request.CurrentDesignationId == null || x.CurrentDesignationId == request.CurrentDesignationId) &&
                (request.UpdateDesignationId == null || x.UpdateDesignationId == request.UpdateDesignationId)&&
                (request.PromotionIncrementType == null || x.PromotionIncrementType == request.PromotionIncrementType) &&
                //(request.OrderDate == null || x.OrderDate == request.OrderDate) && 
                //(request.EffectiveDate == null || x.EffectiveDate == request.EffectiveDate) && 
                (request.isApproval == null || x.IsApproval == request.isApproval) 
            )
            .Include(x => x.CurrentDesignation) 
            .Include(x => x.UpdateDesignation)
            .Include(x => x.CurrentGrade)
            .Include(x => x.UpdateGrade)
            .Include(x => x.CurrentScale)
            .Include(x => x.UpdateScale);

            
            var totalCount = await query.CountAsync(cancellationToken);

           
            query = query
                .OrderByDescending(x => x.OrderDate)
                .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize);

          
            var resultData = await query
                .Select(x => new EmpPromotionIncrementDto
                {
                    EmpId = x.EmpId,
                    CurrentDepartmentId = x.CurrentDepartmentId,
                    CurrentDesignationId = x.CurrentDesignationId,
                    CurrentGradeId = x.CurrentGradeId,
                    CurrentScaleId = x.CurrentScaleId,
                    UpdateGradeId = x.UpdateGradeId,
                    UpdateScaleId = x.UpdateScaleId,
                    UpdateBasicPay = x.UpdateBasicPay,
                    UpdateDesignationId = x.UpdateDesignationId,
                    PromotionIncrementType = x.PromotionIncrementType,
                    OrderById = x.OrderById,
                    OrderDate = x.OrderDate,
                    EffectiveDate = x.EffectiveDate,
                    ApproveStatus = x.ApproveStatus,
                    ApplicationStatus = x.ApplicationStatus,
                    Remark = x.Remark ?? "",
                    IsApproval = x.IsApproval,
                    OrderNo = x.OrderNo ?? "",
                    CurrentDeptJoinDate = x.CurrentDeptJoinDate
                })
                .ToListAsync(cancellationToken);

            
            var result = new PagedResult<EmpPromotionIncrementDto>(resultData, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);

            return result;
        }
    }

}
