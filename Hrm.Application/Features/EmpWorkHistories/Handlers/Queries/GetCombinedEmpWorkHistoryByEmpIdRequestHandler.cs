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
    public class GetCombinedEmpWorkHistoryByEmpIdRequestHandler : IRequestHandler<GetCombinedEmpWorkHistoryByEmpIdRequest, List<EmpWorkHistoryDto>>
    {

        private readonly IHrmRepository<EmpWorkHistory> _EmpWorkHistoryRepository;
        private readonly IHrmRepository<EmpOtherResponsibility> _EmpOtherResponsibilityRepository;
        private readonly IHrmRepository<EmpTransferPosting> _EmpTransferPostingRepository;
        private readonly IHrmRepository<EmpPromotionIncrement> _EmpPromotionIncrementRepository;
        private readonly IMapper _mapper;
        public GetCombinedEmpWorkHistoryByEmpIdRequestHandler(IHrmRepository<EmpWorkHistory> EmpWorkHistoryRepository, IMapper mapper, IHrmRepository<EmpOtherResponsibility> empOtherResponsibilityRepository, IHrmRepository<EmpTransferPosting> empTransferPostingRepository, IHrmRepository<EmpPromotionIncrement> empPromotionIncrementRepository)
        {
            _EmpWorkHistoryRepository = EmpWorkHistoryRepository;
            _mapper = mapper;
            _EmpOtherResponsibilityRepository = empOtherResponsibilityRepository;
            _EmpTransferPostingRepository = empTransferPostingRepository;
            _EmpPromotionIncrementRepository = empPromotionIncrementRepository;
        }

        public async Task<List<EmpWorkHistoryDto>> Handle(GetCombinedEmpWorkHistoryByEmpIdRequest request, CancellationToken cancellationToken)
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

            var sortedCombinedWorkHistory = combinedWorkHistory.OrderBy(x => x.JoiningDate).ToList();

            return sortedCombinedWorkHistory;
        }
    }
}