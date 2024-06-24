using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Union;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Union.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Union.Handlers.Queries
{
    public class GetUnionByIdRequestHandler : IRequestHandler<GetUnionByIdRequest, UnionDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Union> _UnionRepository;
        private readonly IMapper _mapper;
        public GetUnionByIdRequestHandler(IHrmRepository<Hrm.Domain.Union> UnionRepositoy, IMapper mapper)
        {
            _UnionRepository = UnionRepositoy;
            _mapper = mapper;
        }

        public async Task<UnionDto> Handle(GetUnionByIdRequest request, CancellationToken cancellationToken)
        {
            var Union = await _UnionRepository.Get(request.UnionId);
            return _mapper.Map<UnionDto>(Union);
        }
    }
}
