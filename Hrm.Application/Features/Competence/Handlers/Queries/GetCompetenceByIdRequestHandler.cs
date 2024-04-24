using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Competence;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Competence.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Competence.Handlers.Queries
{
    public class GetCompetenceByIdRequestHandler : IRequestHandler<GetCompetenceByIdRequest, CompetenceDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Competence> _CompetenceRepository;
        private readonly IMapper _mapper;
        public GetCompetenceByIdRequestHandler(IHrmRepository<Hrm.Domain.Competence> CompetenceRepositoy, IMapper mapper)
        {
            _CompetenceRepository = CompetenceRepositoy;
            _mapper = mapper;
        }

        public async Task<CompetenceDto> Handle(GetCompetenceByIdRequest request, CancellationToken cancellationToken)
        {
            var Competence = await _CompetenceRepository.Get(request.CompetenceId);
            return _mapper.Map<CompetenceDto>(Competence);
        }
    }
}
