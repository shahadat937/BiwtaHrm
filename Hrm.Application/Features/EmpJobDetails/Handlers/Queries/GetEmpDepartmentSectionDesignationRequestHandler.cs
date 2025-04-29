using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpJobDetail;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpJobDetails.Handlers.Queries
{
    public class GetEmpDepartmentSectionDesignationRequestHandler : IRequestHandler<GetEmpDepartmentSectionDesignationRequest, List<EmpDepatmentSectionAndDesignationInfoDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.EmpJobDetail> _JobDetailsRepository;
        private readonly IHrmRepository<Hrm.Domain.EmpOtherResponsibility> _OtherResponsibility;
        public GetEmpDepartmentSectionDesignationRequestHandler(IHrmRepository<Hrm.Domain.EmpJobDetail> JobDetailsRepository, IHrmRepository<Hrm.Domain.EmpOtherResponsibility> OtherResponsibility)
        {
            _JobDetailsRepository = JobDetailsRepository;
            _OtherResponsibility = OtherResponsibility;

        }
        public async Task<List<EmpDepatmentSectionAndDesignationInfoDto>> Handle(GetEmpDepartmentSectionDesignationRequest request, CancellationToken cancellationToken)
        {
            // Fetching main designation and other responsibility info for the employee
            var mainDesignationInfo = await _JobDetailsRepository.Where(x => x.EmpId == request.EmpId)
                .Include(x => x.Department)
                .Include(x => x.Designation)
                    .ThenInclude(ds => ds.DesignationSetup)
                .Include(x => x.Section).ToListAsync(cancellationToken);
            var otherDesigationInfo = await _OtherResponsibility.Where(x => x.EmpId == request.EmpId)
                .Include(x => x.Department)
                .Include(x => x.Designation)
                    .ThenInclude(ds => ds.DesignationSetup)
                .Include(x => x.ResponsibilityType)
                .Include(x => x.Section).ToListAsync(cancellationToken);

            // Combine and map to DTO
            var result = new List<EmpDepatmentSectionAndDesignationInfoDto>();

            // Process main designation info
            foreach (var job in mainDesignationInfo)
            {
               
                string department = job.Department?.DepartmentName?? string.Empty;
                string designation = job.Designation?.DesignationSetup.Name ?? string.Empty;
                string sectionName = job.Section?.SectionName ?? string.Empty;

                // Build the Name field based on the available values
                result.Add(new EmpDepatmentSectionAndDesignationInfoDto
                {
                    Name = string.Join("_", new[] { department, sectionName, designation }.Where(s => !string.IsNullOrEmpty(s))),
                    DepartmentId = job.DepartmentId,
                    SectionId = job.SectionId,
                    DesignationId = job.DesignationId,
                    CombainedIds = $"D{job.DepartmentId}-S{job.SectionId}-Dg{job.DesignationId}"
                });
            }

            // Process other designation info
            foreach (var responsibility in otherDesigationInfo)
            {
              
                string department = responsibility.Department?.DepartmentName ?? string.Empty;
                string designation = responsibility.Designation?.DesignationSetup?.Name ?? string.Empty;
                string sectionName = responsibility.Section?.SectionName ?? string.Empty;
                string responsiableType = responsibility.ResponsibilityType?.Name ?? string.Empty;

                result.Add(new EmpDepatmentSectionAndDesignationInfoDto
                {
                    Name = string.Join("_", new[] { department, sectionName, designation }.Where(s => !string.IsNullOrEmpty(s))) +
               (string.IsNullOrEmpty(responsiableType) ? string.Empty : $"({responsiableType})"),
                    DepartmentId = responsibility.DepartmentId,
                    SectionId = responsibility.SectionId,
                    DesignationId = responsibility.DesignationId,
                    ResponsibilityTypeId = responsibility.ResponsibilityTypeId,
                    CombainedIds = $"D{responsibility.DepartmentId}-S{responsibility.SectionId}-Dg{responsibility.DesignationId}"
                });
            }

            return result;
        }


    }
}
