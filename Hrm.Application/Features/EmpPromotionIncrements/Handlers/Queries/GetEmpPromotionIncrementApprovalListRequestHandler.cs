using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpPromotionIncrement;
using Hrm.Application.Features.EmpPromotionIncrements.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Hrm.Application.Features.EmpPromotionIncrements.Handlers.Queries
{
    public class GetEmpPromotionIncrementApprovalListRequestHandler : IRequestHandler<GetEmpPromotionIncrementApprovalListRequest, PagedResult<EmpPromotionIncrementDto>>
    {

        private readonly IHrmRepository<EmpPromotionIncrement> _EmpPromotionIncrementRepository;
        private readonly IHrmRepository<EmpJobDetail> _EmpJobDetailRepository;
        private readonly IMapper _mapper;
        public GetEmpPromotionIncrementApprovalListRequestHandler(IHrmRepository<Hrm.Domain.EmpPromotionIncrement> EmpPromotionIncrementRepository, IMapper mapper, IHrmRepository<EmpJobDetail> empJobDetailRepository)
        {
            _EmpPromotionIncrementRepository = EmpPromotionIncrementRepository;
            _mapper = mapper;
            _EmpJobDetailRepository = empJobDetailRepository;
        }

        public async Task<PagedResult<EmpPromotionIncrementDto>> Handle(GetEmpPromotionIncrementApprovalListRequest request, CancellationToken cancellationToken)
        {
            if (request.EmpId != 0)
            {
                var empDepartmentId = await _EmpJobDetailRepository.Where(x => x.EmpId == request.EmpId).Select(x => x.DepartmentId).FirstOrDefaultAsync();

                IQueryable<EmpPromotionIncrement> query = _EmpPromotionIncrementRepository.FilterWithInclude(x => (x.EmpBasicInfo.IdCardNo.ToLower().Contains(request.QueryParams.SearchText) ||
                    x.EmpBasicInfo.FirstName.ToLower().Contains(request.QueryParams.SearchText) ||
                    x.EmpBasicInfo.LastName.ToLower().Contains(request.QueryParams.SearchText) ||
                    String.IsNullOrEmpty(request.QueryParams.SearchText))
                    && (request.Id == 0 || x.Id == request.Id) && 
                    (x.IsApproval == true && x.CurrentDepartmentId == empDepartmentId))
                .Include(x => x.EmpBasicInfo)
                .Include(x => x.ApplicationBy)
                .Include(x => x.OrderBy)
                .Include(x => x.ApproveBy)
                .Include(x => x.CurrentDepartment)
                .Include(x => x.CurrentSection)
                .Include(x => x.CurrentDesignation)
                    .ThenInclude(x => x.DesignationSetup)
                .Include(x => x.CurrentGrade)
                .Include(x => x.CurrentScale)
                .Include(x => x.UpdateDesignation)
                    .ThenInclude(x => x.DesignationSetup)
                .Include(x => x.UpdateGrade)
                .Include(x => x.UpdateScale);

                var totalCount = await query.CountAsync();

                var queryFilter = await query.OrderByDescending(x => x.Id)
                    .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                    .Take(request.QueryParams.PageSize)
                    .ToListAsync(cancellationToken);

                var EmpPromotionIncrementDtos = _mapper.Map<List<EmpPromotionIncrementDto>>(queryFilter);

                var result = new PagedResult<EmpPromotionIncrementDto>(EmpPromotionIncrementDtos, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);

                return result;
            }
            else
            {
                IQueryable<EmpPromotionIncrement> query = _EmpPromotionIncrementRepository.FilterWithInclude(x => (x.EmpBasicInfo.IdCardNo.ToLower().Contains(request.QueryParams.SearchText) ||
                    x.EmpBasicInfo.FirstName.ToLower().Contains(request.QueryParams.SearchText) ||
                    x.EmpBasicInfo.LastName.ToLower().Contains(request.QueryParams.SearchText) ||
                    String.IsNullOrEmpty(request.QueryParams.SearchText))
                    && (request.Id == 0 || x.Id == request.Id) && x.IsApproval == true)
                .Include(x => x.EmpBasicInfo)
                .Include(x => x.ApplicationBy)
                .Include(x => x.OrderBy)
                .Include(x => x.ApproveBy)
                .Include(x => x.CurrentDepartment)
                .Include(x => x.CurrentSection)
                .Include(x => x.CurrentDesignation)
                    .ThenInclude(x => x.DesignationSetup)
                .Include(x => x.CurrentGrade)
                .Include(x => x.CurrentScale)
                .Include(x => x.UpdateDesignation)
                    .ThenInclude(x => x.DesignationSetup)
                .Include(x => x.UpdateGrade)
                .Include(x => x.UpdateScale);

                var totalCount = await query.CountAsync();

                var queryFilter = await query.OrderByDescending(x => x.Id)
                    .Skip((request.QueryParams.PageIndex - 1) * request.QueryParams.PageSize)
                    .Take(request.QueryParams.PageSize)
                    .ToListAsync(cancellationToken);

                var EmpPromotionIncrementDtos = _mapper.Map<List<EmpPromotionIncrementDto>>(queryFilter);

                var result = new PagedResult<EmpPromotionIncrementDto>(EmpPromotionIncrementDtos, totalCount, request.QueryParams.PageIndex, request.QueryParams.PageSize);

                return result;
            }
            
        }
    }
}

