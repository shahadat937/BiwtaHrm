using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.Reportings.TransferPostingReporting.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.TransferPostingReporting.Handlers.Queries
{
    public class GetTransferPostingResultRequestHandler : IRequestHandler<GetTransferPostingResultRequest, PagedResult<EmpTransferPostingReportingDto>>
    {
        private readonly IHrmRepository<EmpTransferPosting> _EmpTranferPostingRepository;
        public GetTransferPostingResultRequestHandler(IHrmRepository<EmpTransferPosting> EmpTranferPostingRepository)
        {
            _EmpTranferPostingRepository = EmpTranferPostingRepository;
        }
        public async Task<PagedResult<EmpTransferPostingReportingDto>> Handle(GetTransferPostingResultRequest request, CancellationToken cancellationToken)
        {
            IQueryable<EmpTransferPosting> query = _EmpTranferPostingRepository.FilterWithInclude(tp => true &&
             (request.DepartmentFrom == 0 || tp.CurrentDepartmentId == request.DepartmentFrom) &&
             (request.SectionFrom == 0 || tp.CurrentSectionId == request.SectionFrom)
             && (request.DepartmentTo == 0 || tp.TransferDepartmentId == request.DepartmentTo) &&
             (request.SectionTo == 0 || tp.TransferSectionId == request.SectionTo))
                .Include(x => x.EmpBasicInfo)
                .ThenInclude(x => x.EmpJobDetail)
                .ThenInclude(x => x.Department)
                .ThenInclude(x => x.EmpJobDetail)
                .ThenInclude(x => x.Designation)
                .ThenInclude(x => x.DesignationSetup)
                .Include(x => x.EmpBasicInfo)
                .ThenInclude(x => x.EmpJobDetail)
                .ThenInclude(x => x.Section);

            bool hasValidDateRange = request.DateFrom != null && request.DateFrom != DateOnly.MinValue &&
                          request.DateTo != null && request.DateTo != DateOnly.MinValue;

            if (hasValidDateRange)
            {
                query = query.Where(tp =>
                    (tp.JoiningDate >= request.DateFrom && tp.JoiningDate <= request.DateTo) ||
                    (tp.OfficeOrderDate >= request.DateFrom && tp.OfficeOrderDate <= request.DateTo)
                );
            }



            var totalCount = await query.CountAsync(cancellationToken);

            query = query
                   .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                   .Take(request.QueryParams.PageSize);

            var resultData = await query
                    .Select(x => new EmpTransferPostingReportingDto
                    {
                        IdCardNo = x.EmpBasicInfo.IdCardNo ?? "",
                        EmpName = x.EmpBasicInfo.FirstName + " " + x.EmpBasicInfo.LastName,
                        DepartmentFrom = x.CurrentDepartment != null ? x.CurrentDepartment.DepartmentName : "",
                        DepartmentTo = x.TransferDepartment != null ? x.TransferDepartment.DepartmentName : "",
                        PreviousDesignationName = x.CurrentDesignation != null ? x.CurrentDesignation.DesignationSetup.Name : " ",
                        CurrentDesignationName = x.TransferDesignation != null ? x.TransferDesignation.DesignationSetup.Name : "",
                        PreviousSectionName = x.CurrentSection != null ? x.CurrentSection.SectionName : "",
                        CurrentSectionName = x.TransferSection != null ? x.TransferSection.SectionName : "",
                        OrderBy = x.OrderOfficeBy != null ? x.OrderOfficeBy.DepartmentName : "",
                        OfficeOrderDate = x.OfficeOrderDate,
                        DeptReleseDate = x.DeptReleaseDate,
                        JoiningDate = x.JoiningDate,
                        ContactNumber = x.EmpBasicInfo.EmpPersonalInfo.FirstOrDefault().MobileNumber,
                        Email = x.EmpBasicInfo.EmpPersonalInfo.FirstOrDefault().Email ?? "",
                        ApprovedStatus = x.ApplicationStatus


                    })
                    .ToListAsync(cancellationToken);
            var result = new PagedResult<EmpTransferPostingReportingDto>(resultData, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);
            return result;
        }
    }
}
