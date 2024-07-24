using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpTransferPosting;
using Hrm.Application.Features.EmpTransferPostings.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTransferPostings.Handlers.Queries
{
    public class GetAllEmpTransferPostingRequestHandler : IRequestHandler<GetAllEmpTransferPostingRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.EmpTransferPosting> _EmpTransferPostingRepository;
        private readonly IMapper _mapper;
        public GetAllEmpTransferPostingRequestHandler(IHrmRepository<Hrm.Domain.EmpTransferPosting> EmpTransferPostingRepository, IMapper mapper)
        {
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAllEmpTransferPostingRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.EmpTransferPosting> EmpTransferPostings = _EmpTransferPostingRepository.Where(x => true);
            EmpTransferPostings = EmpTransferPostings.OrderByDescending(x => x.Id);

            var EmpTransferPostingDtos = _mapper.Map<List<EmpTransferPostingDto>>(EmpTransferPostings);

            return EmpTransferPostingDtos;
        }
    }
}
