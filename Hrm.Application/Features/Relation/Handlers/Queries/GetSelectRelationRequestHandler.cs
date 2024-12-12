using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Relation.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Relation.Handlers.Queries
{

    public class GetSelectRelationRequestHandler : IRequestHandler<GetSelectRelationRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Relation> _RelationRepository;


        public GetSelectRelationRequestHandler(IHrmRepository<Hrm.Domain.Relation> RelationRepository)
        {
            _RelationRepository = RelationRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectRelationRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Relation> Relations = await _RelationRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Relations.Select(x => new SelectedModel
            {
                Name = x.RelationName,
                Id = x.RelationId
            }).OrderBy(x => x.Name).ToList();
            return selectModels;
        }
    }
}
