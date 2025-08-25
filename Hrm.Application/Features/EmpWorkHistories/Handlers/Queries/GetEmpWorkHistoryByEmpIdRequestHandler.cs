using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpWorkHistories.Requests.Queries;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.EmpWorkHistory;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.EmpWorkHistories.Handlers.Queries
{
    public class GetEmpWorkHistoryByEmpIdRequestHandler : IRequestHandler<GetEmpWorkHistoryByEmpIdRequest, List<EmpWorkHistoryDto>>
    {

        private readonly IHrmRepository<EmpWorkHistory> _EmpWorkHistoryRepository;
        private readonly IHrmRepository<EmpJobDetail> _EmpJobDetailRepository;
        private readonly IMapper _mapper;
        public GetEmpWorkHistoryByEmpIdRequestHandler(IHrmRepository<EmpWorkHistory> EmpWorkHistoryRepository, IMapper mapper, IHrmRepository<EmpJobDetail> EmpJobDetailRepository)
        {
            _EmpWorkHistoryRepository = EmpWorkHistoryRepository;
            _EmpJobDetailRepository = EmpJobDetailRepository;
            _mapper = mapper;
        }

        public async Task<List<EmpWorkHistoryDto>> Handle(GetEmpWorkHistoryByEmpIdRequest request, CancellationToken cancellationToken)
        {
            List<EmpWorkHistory> EmpWorkHistories = await _EmpWorkHistoryRepository.Where(x => x.EmpId == request.Id)
                //.Include(x => x.Office)
                //.Include(x => x.Department)
                //.Include(x => x.Section)
                //.Include(x => x.DesignationSetup)
                //.Include(x => x.Designation)
                //        .ThenInclude(ds => ds.DesignationSetup)
                .ToListAsync(cancellationToken);

            if (EmpWorkHistories == null)
            {
                return null;
            }

            var empJobDetails = await _EmpJobDetailRepository.Where(x => x.EmpId == request.Id)
                .Include(x => x.Department)
                .Include(x => x.Section)
                .Include(x => x.Designation)
                    .ThenInclude(x => x.DesignationSetup)
                .FirstOrDefaultAsync();

            var currentJobHistory = new EmpWorkHistoryDto();


            List<EmpWorkHistoryDto> result = _mapper.Map<List<EmpWorkHistoryDto>>(EmpWorkHistories);

            if (empJobDetails != null)
            {

                currentJobHistory.DepartmentName = empJobDetails.Department?.DepartmentName ?? "";
                currentJobHistory.DepartmentNameBangla = empJobDetails.Department?.DepartmentNameBangla ?? "";
                currentJobHistory.SectionName = empJobDetails.Section?.SectionName ?? "";
                currentJobHistory.SectionNameBangla = empJobDetails.Section?.SectionNameBangla ?? "";
                currentJobHistory.DesignationName = empJobDetails.Designation?.DesignationSetup.Name ?? "";
                currentJobHistory.DesignationNameBangla = empJobDetails.Designation?.DesignationSetup.NameBangla ?? "";
                currentJobHistory.IsCurrentJob = true;
                currentJobHistory.JoiningDate = empJobDetails.JoiningDate ?? null;

                result.Add(currentJobHistory);
            }

            List<EmpWorkHistoryDto> orderResult = result.OrderByDescending(x => x.IsCurrentJob)
                .ThenByDescending(x => x.JoiningDate).ToList();

            return orderResult;
        }
    }
}