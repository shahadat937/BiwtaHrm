using AutoMapper;
using Hrm.Application.Contracts.Persistence;
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
    public class GetAllEmpTransferPostingRequestHandler : IRequestHandler<GetAllEmpTransferPostingRequest, PagedResult<EmpTransferPostingDto>>
    {

        private readonly IHrmRepository<EmpTransferPosting> _EmpTransferPostingRepository;
        private readonly IMapper _mapper;
        public GetAllEmpTransferPostingRequestHandler(IHrmRepository<Hrm.Domain.EmpTransferPosting> EmpTransferPostingRepository, IMapper mapper)
        {
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<EmpTransferPostingDto>> Handle(GetAllEmpTransferPostingRequest request, CancellationToken cancellationToken)
        {
            IQueryable<EmpTransferPosting> query = _EmpTransferPostingRepository.FilterWithInclude(x => (x.EmpBasicInfo.IdCardNo.ToLower().Contains(request.QueryParams.SearchText) ||
                    x.EmpBasicInfo.FirstName.ToLower().Contains(request.QueryParams.SearchText) ||
                    x.EmpBasicInfo.LastName.ToLower().Contains(request.QueryParams.SearchText) ||
                    String.IsNullOrEmpty(request.QueryParams.SearchText))
                    && (request.Id == 0 || x.Id == request.Id))
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
