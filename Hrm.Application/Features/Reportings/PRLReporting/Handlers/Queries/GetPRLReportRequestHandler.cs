using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.DTOs.Result;
using Hrm.Application.Features.Reportings.EmployeeList.Requests.Queries;
using Hrm.Application.Features.Reportings.VacancyReport.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Hrm.Application.Features.Reportings.VacancyReport.Handlers.Queries
{
    public class GetPRLReportRequestHandler : IRequestHandler<GetPRLReportRequest, object>
    {

        private readonly IHrmRepository<object> _PRLReportRepository;

        public GetPRLReportRequestHandler(IHrmRepository<object> PRLReportRepository)
        {
            _PRLReportRepository = PRLReportRepository;
        }

        public async Task<object> Handle(GetPRLReportRequest request, CancellationToken cancellationToken)
        {
            try
            {
                string query = $@"
            EXEC [dbo].[GetEmpListByPRLAgeWithFilters] 
                @CurrentDate = '{request.CurrentDate}', 
                @StartDate = '{request.StartDate}', 
                @EndDate = '{request.EndDate}', 
                @DepartmentId = {request.DepartmentId}, 
                @SectionId = {request.SectionId}, 
                @DesignationId = {request.DesignationId}, 
                @IsPRL = {request.IsPRL}, 
                @IsRetirment = {request.IsRetirment}, 
                @IsGone = {request.IsGone}, 
                @IsWillGone = {request.IsWillGone}, 
                @PageIndex = {request.QueryParams.PageIndex}, 
                @PageSize = {request.QueryParams.PageSize}";

                var result = _PRLReportRepository.ExecWithSqlQuery(query);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing PRL Report Handler: {ex.Message}", ex);
            }
        }

    }
}
