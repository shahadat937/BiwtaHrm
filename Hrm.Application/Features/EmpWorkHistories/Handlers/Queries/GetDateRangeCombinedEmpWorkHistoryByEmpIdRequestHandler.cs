using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpWorkHistory;
using Hrm.Application.Features.EmpWorkHistories.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpWorkHistories.Handlers.Queries
{
    public class GetDateRangeCombinedEmpWorkHistoryByEmpIdRequestHandler : IRequestHandler<GetDateRangeCombinedEmpWorkHistoryByEmpIdRequest, List<EmpWorkHistoryDto>>
    {

        private readonly IHrmRepository<EmpWorkHistory> _EmpWorkHistoryRepository;
        private readonly IHrmRepository<EmpOtherResponsibility> _EmpOtherResponsibilityRepository;
        private readonly IHrmRepository<EmpTransferPosting> _EmpTransferPostingRepository;
        private readonly IHrmRepository<EmpPromotionIncrement> _EmpPromotionIncrementRepository;
        private readonly IHrmRepository<EmpJobDetail> _EmpJobDetailRepository;
        private readonly IMapper _mapper;
        public GetDateRangeCombinedEmpWorkHistoryByEmpIdRequestHandler(IHrmRepository<EmpWorkHistory> EmpWorkHistoryRepository, IMapper mapper, IHrmRepository<EmpOtherResponsibility> empOtherResponsibilityRepository, IHrmRepository<EmpTransferPosting> empTransferPostingRepository, IHrmRepository<EmpPromotionIncrement> empPromotionIncrementRepository, IHrmRepository<EmpJobDetail> empJobDetailRepository)
        {
            _EmpWorkHistoryRepository = EmpWorkHistoryRepository;
            _mapper = mapper;
            _EmpOtherResponsibilityRepository = empOtherResponsibilityRepository;
            _EmpTransferPostingRepository = empTransferPostingRepository;
            _EmpPromotionIncrementRepository = empPromotionIncrementRepository;
            _EmpJobDetailRepository = empJobDetailRepository;
        }

        public async Task<List<EmpWorkHistoryDto>> Handle(GetDateRangeCombinedEmpWorkHistoryByEmpIdRequest request, CancellationToken cancellationToken)
        {
            List<EmpWorkHistoryDto> combinedWorkHistory = new List<EmpWorkHistoryDto>();

            var workHistory = await _EmpWorkHistoryRepository.Where(x => x.EmpId == request.EmpId)
                //.Include(x => x.Office)
                //.Include(x => x.Department)
                //.Include(x => x.Section)
                //.Include(x => x.DesignationSetup)
                //.Include(x => x.Designation)
                //    .ThenInclude(x => x.DesignationSetup)
                .ToListAsync(cancellationToken);
            if (workHistory != null && workHistory.Any())
            {
                var mappedWorkHistory = _mapper.Map<List<EmpWorkHistoryDto>>(workHistory);
                combinedWorkHistory.AddRange(mappedWorkHistory);
            }

            var otherResponsibilities = await _EmpOtherResponsibilityRepository.Where(x => x.EmpId == request.EmpId)
                .Include(x => x.Office)
                .Include(x => x.Department)
                .Include(x => x.Section)
                .Include(x => x.Designation)
                    .ThenInclude(x => x.DesignationSetup)
                .Include(x => x.ResponsibilityType)
                .ToListAsync(cancellationToken);
            if (otherResponsibilities != null && otherResponsibilities.Any())
            {
                var mappedResponsibilities = otherResponsibilities.Select(responsibility => new EmpWorkHistoryDto
                {
                    EmpId = responsibility.EmpId,
                    //OfficeId = responsibility.OfficeId,
                    //DepartmentId = responsibility.DepartmentId,
                    //SectionId = responsibility.SectionId,
                    //DesignationId = responsibility.DesignationId,
                    JoiningDate = responsibility.StartDate,
                    ReleaseDate = responsibility.EndDate,
                    Remark = responsibility.Remark,
                    IsActive = responsibility.IsActive,
                    //OfficeName = responsibility.Office?.OfficeName,
                    DepartmentName = responsibility.Department?.DepartmentName,
                    SectionName = responsibility.Section?.SectionName,
                    DesignationName = responsibility.Designation?.DesignationSetup.Name + " ("+ responsibility.ResponsibilityType?.Name + ")"
                }).ToList();

                combinedWorkHistory.AddRange(mappedResponsibilities);
            }

            var empTransferPosting = await _EmpTransferPostingRepository.Where(x => x.EmpId == request.EmpId && x.ApplicationStatus == true)
                .Include(x => x.CurrentDepartment)
                .Include(x => x.CurrentSection)
                .Include(x => x.CurrentDesignation)
                    .ThenInclude(x => x.DesignationSetup)
                .ToListAsync(cancellationToken);
            if (empTransferPosting != null && empTransferPosting.Any())
            {
                var mappedTransferPosting = empTransferPosting.Select(transferPosting => new EmpWorkHistoryDto
                {
                    EmpId = transferPosting.EmpId,
                    //OfficeId = transferPosting.CurrentOfficeId,
                    //DepartmentId = transferPosting.CurrentDepartmentId,
                    //SectionId = transferPosting.CurrentSectionId,
                    //DesignationId = transferPosting.CurrentDesignationId,
                    JoiningDate = transferPosting.CurrentDeptJoinDate,
                    ReleaseDate = transferPosting.JoiningDate?.AddDays(-1),
                    Remark = transferPosting.Remark,
                    IsActive = transferPosting.IsActive,
                    //OfficeName = transferPosting.CurrentOffice?.OfficeName,
                    DepartmentName = transferPosting.CurrentDepartment?.DepartmentName,
                    SectionName = transferPosting.CurrentSection?.SectionName,
                    DesignationName = transferPosting.CurrentDesignation?.DesignationSetup.Name
                }).ToList();

                combinedWorkHistory.AddRange(mappedTransferPosting);
            }


            var empPormotionIncrement = await _EmpPromotionIncrementRepository.Where(x => x.EmpId == request.EmpId && x.ApplicationStatus == true && x.UpdateDesignationId != null)
                .Include(x => x.CurrentDepartment)
                .Include(x => x.CurrentSection)
                .Include(x => x.CurrentDesignation)
                    .ThenInclude(x => x.DesignationSetup)
                .ToListAsync(cancellationToken);
            if (empPormotionIncrement != null && empPormotionIncrement.Any())
            {
                var mappedPromotionIncrement = empPormotionIncrement.Select(promotionIncrement => new EmpWorkHistoryDto
                {
                    EmpId = promotionIncrement.EmpId,
                    //DepartmentId = promotionIncrement.CurrentDepartmentId,
                    //SectionId = promotionIncrement.CurrentSectionId,
                    //DesignationId = promotionIncrement.CurrentDesignationId,
                    JoiningDate = promotionIncrement.CurrentDeptJoinDate,
                    ReleaseDate = promotionIncrement.EffectiveDate?.AddDays(-1),
                    Remark = promotionIncrement.Remark,
                    IsActive = promotionIncrement.IsActive,
                    DepartmentName = promotionIncrement.CurrentDepartment?.DepartmentName,
                    SectionName = promotionIncrement.CurrentSection?.SectionName,
                    DesignationName = promotionIncrement.CurrentDesignation?.DesignationSetup.Name
                }).ToList();

                combinedWorkHistory.AddRange(mappedPromotionIncrement);
            }

            var transferPostings = _EmpTransferPostingRepository
                .Where(tp => tp.EmpId == request.EmpId && tp.ApplicationStatus == true);

            var workHistories = _EmpWorkHistoryRepository
                .Where(wh => wh.EmpId == request.EmpId);

            var jobDetails = _EmpJobDetailRepository
                .Where(jd => jd.EmpId == request.EmpId);

            var promotionIncrements = _EmpPromotionIncrementRepository
                .Where(pi => pi.EmpId == request.EmpId && pi.UpdateDesignationId != null);

            var latestTransferPostingJoiningDate = transferPostings
                .OrderByDescending(tp => tp.JoiningDate)
                .Select(tp => tp.JoiningDate)
                .FirstOrDefault();

            var latestWorkHistoryReleaseDate = workHistories
                .OrderByDescending(wh => wh.ReleaseDate)
                .Select(wh => wh.ReleaseDate)
                .FirstOrDefault();

            var extendedlatestWorkHistoryReleaseDate = latestWorkHistoryReleaseDate?.AddDays(1);

            var latestJobDetailJoiningDate = jobDetails
                .OrderByDescending(jd => jd.JoiningDate)
                .Select(jd => jd.JoiningDate)
                .FirstOrDefault();

            var latestPromotionIncrementsDate = promotionIncrements
                .OrderByDescending(wh => wh.EffectiveDate)
                .Select(wh => wh.EffectiveDate)
                .FirstOrDefault();

            var latestDate = new[] { latestTransferPostingJoiningDate, extendedlatestWorkHistoryReleaseDate, latestJobDetailJoiningDate, latestPromotionIncrementsDate }
                .Where(d => d != null).Max();


            var jobDetailsInfo = await _EmpJobDetailRepository
                .Where(jd => jd.EmpId == request.EmpId)
                .Include(x => x.Department)
                .Include(x => x.Section)
                .Include(x => x.Designation)
                    .ThenInclude(x => x.DesignationSetup)
                .FirstOrDefaultAsync();

            if (jobDetailsInfo != null)
            {
                var currentJobDetailsInfo = new EmpWorkHistoryDto
                {
                    EmpId = jobDetailsInfo.EmpId,
                    //DepartmentId = promotionIncrement.CurrentDepartmentId,
                    //SectionId = promotionIncrement.CurrentSectionId,
                    //DesignationId = promotionIncrement.CurrentDesignationId,
                    JoiningDate = latestDate,
                    Remark = jobDetailsInfo.Remark,
                    IsActive = jobDetailsInfo.IsActive,
                    DepartmentName = jobDetailsInfo.Department?.DepartmentName,
                    SectionName = jobDetailsInfo.Section?.SectionName,
                    DesignationName = jobDetailsInfo.Designation?.DesignationSetup.Name
                };

                combinedWorkHistory.Add(currentJobDetailsInfo);
            }

            var sortedCombinedWorkHistory = combinedWorkHistory.Where(x => x.JoiningDate <= request.EndDate && x.ReleaseDate >= request.StartDate).OrderBy(x => x.JoiningDate).ToList();

            return sortedCombinedWorkHistory;
        }
    }
}