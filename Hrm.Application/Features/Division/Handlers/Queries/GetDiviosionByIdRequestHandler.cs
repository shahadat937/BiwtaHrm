using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Division;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Division.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Division.Handlers.Queries
{
    public class GetDivisionByIdRequestHandler : IRequestHandler<GetDivisionByIdRequest, DivisionDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Division> _DivisionRepository;
        private readonly IMapper _mapper;
        public GetDivisionByIdRequestHandler(IHrmRepository<Hrm.Domain.Division> DivisionRepositoy, IMapper mapper)
        {
            _DivisionRepository = DivisionRepositoy;
            _mapper = mapper;
        }

        public async Task<DivisionDto> Handle(GetDivisionByIdRequest request, CancellationToken cancellationToken)
        {
            var Division = await _DivisionRepository.Get(request.DivisionId);
            return _mapper.Map<DivisionDto>(Division);
        }
    }
}
