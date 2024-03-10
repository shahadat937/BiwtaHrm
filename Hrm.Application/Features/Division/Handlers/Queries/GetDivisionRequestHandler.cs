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
    public class GetDivisionRequestHandler : IRequestHandler<GetDivisionRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Division> _DivisionRepository;
        private readonly IMapper _mapper;
        public GetDivisionRequestHandler(IHrmRepository<Hrm.Domain.Division> DivisionRepository, IMapper mapper)
        {
            _DivisionRepository = DivisionRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetDivisionRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Division> Divisions = _DivisionRepository.Where(x => true);

            var DivisionDtos = _mapper.Map<List<DivisionDto>>(Divisions);

            return DivisionDtos;
        }
    }
}
