using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpOtherResponsibility;
using Hrm.Application.DTOs.EmpTransferPosting;
using Hrm.Application.DTOs.EmpWorkHistory;
using Hrm.Application.DTOs.Form;
using Hrm.Application.Features.Form.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Form.Handlers.Queries
{
    public class GetEmployeeJobHistoryForFormRequestHandler: IRequestHandler<GetEmployeeJobHistoryForFormRequest, object>
    {
        private readonly IHrmRepository<EmpWorkHistory> _EmpWorkHistoryRepo;
        private readonly IHrmRepository<EmpTransferPosting> _EmpTransferPostingRepo;
        private readonly IHrmRepository<EmpOtherResponsibility> _EmpOtherResponsibilityRepo;
        private readonly IMapper _mapper;

        public GetEmployeeJobHistoryForFormRequestHandler(IHrmRepository<EmpWorkHistory> empWorkHistoryRepo, IHrmRepository<EmpTransferPosting> empTransferPostingRepo, IHrmRepository<EmpOtherResponsibility> empOtherResponsibilityRepo, IMapper mapper)
        {
            _EmpWorkHistoryRepo = empWorkHistoryRepo;
            _EmpTransferPostingRepo = empTransferPostingRepo;
            _EmpOtherResponsibilityRepo = empOtherResponsibilityRepo;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmployeeJobHistoryForFormRequest request, CancellationToken cancellationToken)
        {
            var workHistory = await _EmpWorkHistoryRepo.Where(x => x.EmpId == request.EmpId && x.JoiningDate < request.endDate && x.ReleaseDate > request.startDate)
                //.Include(x => x.Office)
                //.Include(x => x.Department)
                //.Include(x => x.Section)
                //.Include(x => x.Designation)
                //.ThenInclude(x => x.DesignationSetup)
                .ToListAsync();

            var otherResponsibility = await _EmpOtherResponsibilityRepo.Where(x => x.EmpId == request.EmpId && x.StartDate < request.endDate && x.EndDate > request.startDate)
                .Include(x => x.Office)
                .Include(x => x.Department)
                .Include(x => x.Section)
                .Include(x => x.Designation)
                .ThenInclude(x => x.DesignationSetup).ToListAsync();

            var transferPosting = await _EmpTransferPostingRepo.Where(x => x.EmpId == request.EmpId && x.CurrentDeptJoinDate < request.endDate && x.JoiningDate >= request.startDate && x.ApplicationStatus != null && x.ApplicationStatus == true)
                .Include(x=>x.CurrentDepartment)
                .Include(x=>x.CurrentSection)
                .Include(x=>x.CurrentDesignation)
                .ThenInclude(x=>x.DesignationSetup)
                .ToListAsync();

            var workHistoryDto = _mapper.Map<List<EmpWorkHistoryDto>>(workHistory);
            var otherResponsibilityDto = _mapper.Map<List<EmpOtherResponsibilityDto>>(otherResponsibility);
            var transferPostingDto = _mapper.Map<List<EmpTransferPostingDto>>(transferPosting);

            var workHistories = workHistoryDto.Select(x => new WorkHistoryForFormDto
            {
                EmpId = (int)x.EmpId,
                startDate = (DateOnly) x.JoiningDate,
                endDate = (DateOnly) x.ReleaseDate,
                //DepartmentId = x.DepartmentId,
                //DesignationId = x.DesignationId,
                //SectionId = x.SectionId,
                DepartmentName = x.DepartmentName,
                DesignationName = x.DesignationName,
                SectionName = x.SectionName
            }).ToList();


            var workHistoriesFromOtherResponsibility = otherResponsibilityDto.Select(x => new WorkHistoryForFormDto
            {
                EmpId = (int)x.EmpId,
                startDate = (DateOnly)x.StartDate,
                endDate = (DateOnly)x.EndDate,
                DepartmentId = x.DepartmentId,
                DesignationId = x.DesignationId,
                SectionId = x.SectionId,
                DepartmentName = x.DepartmentName,
                DesignationName = x.DesignationName,
                SectionName = x.SectionName
            }).ToList();

            var workingHistoriesFromTransferPosting = transferPostingDto.Select(x => new WorkHistoryForFormDto
            {
                EmpId = (int)x.EmpId,
                startDate = (DateOnly)x.CurrentDeptJoinDate,
                endDate = (DateOnly)x.JoiningDate,
                DepartmentId = x.CurrentDepartmentId,
                DesignationId = x.CurrentDesignationId,
                SectionId = x.CurrentSectionId,
                DepartmentName = x.DepartmentName,
                DesignationName = x.DesignationName,
                SectionName = x.SectionName
            }).ToList();

            workHistories.Concat(workHistoriesFromOtherResponsibility);
            workHistories.Concat(workingHistoriesFromTransferPosting);
            workHistories.OrderBy(x => x.startDate);


            return workHistories;
        }
    }
}
