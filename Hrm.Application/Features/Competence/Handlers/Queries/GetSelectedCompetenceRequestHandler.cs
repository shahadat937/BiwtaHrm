using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Competence.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Competence.Handlers.Queries
{ 
    public class GetSelectedCompetenceRequestHandler : IRequestHandler<GetSelectedCompetenceRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Competence> _CompetenceRepository;


        public GetSelectedCompetenceRequestHandler(IHrmRepository<Hrm.Domain.Competence> CompetenceRepository)
        {
            _CompetenceRepository = CompetenceRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedCompetenceRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Competence> Competences = await _CompetenceRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Competences.Select(x => new SelectedModel 
            {
                Name = x.CompetenceName,
                Id = x.CompetenceId
            }).OrderBy(x => x.Name).ToList();
            return selectModels;
        }
    }
}
 