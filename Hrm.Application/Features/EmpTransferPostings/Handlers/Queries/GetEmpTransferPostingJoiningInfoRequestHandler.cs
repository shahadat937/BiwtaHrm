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
    public class GetEmpTransferPostingJoiningInfoRequestHandler : IRequestHandler<GetEmpTransferPostingJoiningInfoRequest, object>
    {

        private readonly IHrmRepository<EmpTransferPosting> _EmpTransferPostingRepository;
        private readonly IMapper _mapper;
        public GetEmpTransferPostingJoiningInfoRequestHandler(IHrmRepository<Hrm.Domain.EmpTransferPosting> EmpTransferPostingRepository, IMapper mapper)
        {
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpTransferPostingJoiningInfoRequest request, CancellationToken cancellationToken)
        {
            IQueryable<EmpTransferPosting> EmpTransferPostings = _EmpTransferPostingRepository.Where(x => x.DeptApproveStatus == true && x.IsJoining == true)
                .Include(x => x.EmpBasicInfo);

            EmpTransferPostings = EmpTransferPostings.OrderBy(x => x.DeptApproveStatus);

            var EmpTransferPostingDtos = _mapper.Map<List<EmpTransferPostingDto>>(EmpTransferPostings);

            return EmpTransferPostingDtos;
        }
    }
}