using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.TrainingName.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TrainingName.Handlers.Queries
{ 
    public class GetSelectedTrainingNameRequestHandler : IRequestHandler<GetSelectedTrainingNameRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.TrainingName> _TrainingNameRepository;


        public GetSelectedTrainingNameRequestHandler(IHrmRepository<Hrm.Domain.TrainingName> TrainingNameRepository)
        {
            _TrainingNameRepository = TrainingNameRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedTrainingNameRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.TrainingName> TrainingNames = await _TrainingNameRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = TrainingNames.Select(x => new SelectedModel 
            {
                Name = x.TrainingNames,
                Id = x.TrainingNameId
            }).ToList();
            return selectModels;
        }
    }
}
 