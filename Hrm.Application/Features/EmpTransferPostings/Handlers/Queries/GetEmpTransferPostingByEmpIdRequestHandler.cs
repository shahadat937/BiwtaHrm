using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpTransferPosting;
using Hrm.Application.Features.EmpTransferPostings.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTransferPostings.Handlers.Queries
{
    public class GetEmpTransferPostingByEmpIdRequestHandler : IRequestHandler<GetEmpTransferPostingByEmpIdRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.EmpTransferPosting> _EmpTransferPostingRepository;
        private readonly IMapper _mapper;
        public GetEmpTransferPostingByEmpIdRequestHandler(IHrmRepository<Hrm.Domain.EmpTransferPosting> EmpTransferPostingRepository, IMapper mapper)
        {
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpTransferPostingByEmpIdRequest request, CancellationToken cancellationToken)
        {
            var EmpTransferPostings = await _EmpTransferPostingRepository.FindOneAsync(x=> x.Id == request.Id);

            return EmpTransferPostings;
        }
    }
}

