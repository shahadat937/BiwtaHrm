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
    public class GetCompetenceRequestHandler : IRequestHandler<GetCompetenceRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Competence> _CompetenceRepository;
        private readonly IMapper _mapper;
        public GetCompetenceRequestHandler(IHrmRepository<Hrm.Domain.Competence> CompetenceRepository, IMapper mapper)
        {
            _CompetenceRepository = CompetenceRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetCompetenceRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Competence> Competences = _CompetenceRepository.Where(x => true);

            var CompetenceDtos = _mapper.Map<List<CompetenceDto>>(Competences);

            return CompetenceDtos;
        }
    }
}
