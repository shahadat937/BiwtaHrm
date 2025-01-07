using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.DTOs.EmpTransferPosting;
using Hrm.Application.Features.EmpTransferPostings.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTransferPostings.Handlers.Queries
{
    public class GetEmpTransferPostingDeptApprovalRequestHandler : IRequestHandler<GetEmpTransferPostingDeptApprovalRequest, PagedResult<EmpTransferPostingDto>>
    {

        private readonly IHrmRepository<EmpTransferPosting> _EmpTransferPostingRepository;
        private readonly IHrmRepository<EmpJobDetail> _EmpJobDetailRepository;
        private readonly IMapper _mapper;
        public GetEmpTransferPostingDeptApprovalRequestHandler(IHrmRepository<Hrm.Domain.EmpTransferPosting> EmpTransferPostingRepository, IMapper mapper, IHrmRepository<EmpJobDetail> empJobDetailRepository)
        {
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
            _mapper = mapper;
            _EmpJobDetailRepository = empJobDetailRepository;
        }

        public async Task<PagedResult<EmpTransferPostingDto>> Handle(GetEmpTransferPostingDeptApprovalRequest request, CancellationToken cancellationToken)
        {
            if (request.EmpId != 0)
            {
                var empDepartmentId = await _EmpJobDetailRepository.Where(x => x.EmpId == request.EmpId).Select(x => x.DepartmentId).FirstOrDefaultAsync();

                IQueryable<EmpTransferPosting> query = _EmpTransferPostingRepository.FilterWithInclude(x => (x.EmpBasicInfo.IdCardNo.ToLower().Contains(request.QueryParams.SearchText) ||
                    x.EmpBasicInfo.FirstName.ToLower().Contains(request.QueryParams.SearchText) ||
                    x.EmpBasicInfo.LastName.ToLower().Contains(request.QueryParams.SearchText) ||
                    String.IsNullOrEmpty(request.QueryParams.SearchText))
                    && (request.Id == 0 || x.Id == request.Id)
                    && (x.TransferApproveStatus == true || x.IsTransferApprove == false) && x.IsDepartmentApprove == true && x.CurrentDepartmentId == empDepartmentId)
                    .Include(x => x.EmpBasicInfo)
                    .Include(x => x.ApplicationBy)
                    .Include(x => x.OrderOfficeBy)
                    .Include(x => x.CurrentGrade)
                    .Include(x => x.UpdateGrade)
                    .Include(x => x.CurrentScale)
                    .Include(x => x.UpdateScale)
                    .Include(x => x.TransferApproveBy)
                    .Include(x => x.DeptReleaseBy)
                    .Include(x => x.JoiningReportingBy)
                    .Include(x => x.CurrentOffice)
                    .Include(x => x.TransferOffice)
                    .Include(x => x.CurrentDepartment)
                    .Include(x => x.TransferDepartment)
                    .Include(x => x.CurrentDesignation)
                        .ThenInclude(x => x.DesignationSetup)
                    .Include(x => x.TransferDesignation)
                        .ThenInclude(x => x.DesignationSetup)
                    .Include(x => x.CurrentSection)
                    .Include(x => x.TransferSection)
                    .Include(x => x.ReleaseType)
                    .Include(x => x.DeptReleaseType);

                var totalCount = await query.CountAsync();

                var queryFilter = await query.OrderByDescending(x => x.Id)
                    .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                    .Take(request.QueryParams.PageSize)
                    .ToListAsync(cancellationToken);


                var EmpTransferPostingDtos = _mapper.Map<List<EmpTransferPostingDto>>(queryFilter);

                var result = new PagedResult<EmpTransferPostingDto>(EmpTransferPostingDtos, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);

                return result;
            }
            else
            {
                IQueryable<EmpTransferPosting> query = _EmpTransferPostingRepository.FilterWithInclude(x => (x.EmpBasicInfo.IdCardNo.ToLower().Contains(request.QueryParams.SearchText) ||
                    x.EmpBasicInfo.FirstName.ToLower().Contains(request.QueryParams.SearchText) ||
                    x.EmpBasicInfo.LastName.ToLower().Contains(request.QueryParams.SearchText) ||
                    String.IsNullOrEmpty(request.QueryParams.SearchText))
                    && (request.Id == 0 || x.Id == request.Id)
                    && (x.TransferApproveStatus == true || x.IsTransferApprove == false) && x.IsDepartmentApprove == true)
                    .Include(x => x.EmpBasicInfo)
                    .Include(x => x.ApplicationBy)
                    .Include(x => x.OrderOfficeBy)
                    .Include(x => x.CurrentGrade)
                    .Include(x => x.UpdateGrade)
                    .Include(x => x.CurrentScale)
                    .Include(x => x.UpdateScale)
                    .Include(x => x.TransferApproveBy)
                    .Include(x => x.DeptReleaseBy)
                    .Include(x => x.JoiningReportingBy)
                    .Include(x => x.CurrentOffice)
                    .Include(x => x.TransferOffice)
                    .Include(x => x.CurrentDepartment)
                    .Include(x => x.TransferDepartment)
                    .Include(x => x.CurrentDesignation)
                        .ThenInclude(x => x.DesignationSetup)
                    .Include(x => x.TransferDesignation)
                        .ThenInclude(x => x.DesignationSetup)
                    .Include(x => x.CurrentSection)
                    .Include(x => x.TransferSection)
                    .Include(x => x.ReleaseType)
                    .Include(x => x.DeptReleaseType);

                var totalCount = await query.CountAsync();

                var queryFilter = await query.OrderByDescending(x => x.Id)
                    .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                    .Take(request.QueryParams.PageSize)
                    .ToListAsync(cancellationToken);


                var EmpTransferPostingDtos = _mapper.Map<List<EmpTransferPostingDto>>(queryFilter);

                var result = new PagedResult<EmpTransferPostingDto>(EmpTransferPostingDtos, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);

                return result;
            }
                
        }
    }
}