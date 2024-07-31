using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpTransferPosting;
using Hrm.Application.Features.EmpTransferPostings.Requests.Queries;
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
    public class GetEmpTransferPostingApprovalListRequestHandler : IRequestHandler<GetEmpTransferPostingApprovalListRequest, object>
    {

        private readonly IHrmRepository<EmpTransferPosting> _EmpTransferPostingRepository;
        private readonly IMapper _mapper;
        public GetEmpTransferPostingApprovalListRequestHandler(IHrmRepository<Hrm.Domain.EmpTransferPosting> EmpTransferPostingRepository, IMapper mapper)
        {
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpTransferPostingApprovalListRequest request, CancellationToken cancellationToken)
        {
            IQueryable<EmpTransferPosting> EmpTransferPostings = _EmpTransferPostingRepository.Where(x => x.IsTransferApprove == true)
                .Include(x => x.EmpBasicInfo)
                .Include(x => x.ApplicationBy)
                .Include(x => x.OrderBy)
                .Include(x => x.TransferApproveBy)
                .Include(x => x.DeptReleaseBy)
                .Include(x => x.JoiningReportingBy)
                .Include(x => x.CurrentOffice)
                .Include(x => x.TransferOffice)
                .Include(x => x.CurrentDepartment)
                .Include(x => x.TransferDepartment)
                .Include(x => x.CurrentDesignation)
                .Include(x => x.TransferDesignation)
                .Include(x => x.CurrentSection)
                .Include(x => x.TransferSection)
                .Include(x => x.ReleaseType)
                .Include(x => x.DeptReleaseType);

            EmpTransferPostings = EmpTransferPostings.OrderBy(x => x.TransferApproveStatus);

            var EmpTransferPostingDtos = _mapper.Map<List<EmpTransferPostingDto>>(EmpTransferPostings);

            return EmpTransferPostingDtos;
        }
    }
}

