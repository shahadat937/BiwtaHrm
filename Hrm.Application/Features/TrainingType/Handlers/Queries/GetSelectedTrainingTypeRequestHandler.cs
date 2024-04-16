using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.TrainingType.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TrainingType.Handlers.Queries
{ 
    public class GetSelectedTrainingTypeRequestHandler : IRequestHandler<GetSelectedTrainingTypeRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.TrainingType> _TrainingTypeRepository;


        public GetSelectedTrainingTypeRequestHandler(IHrmRepository<Hrm.Domain.TrainingType> TrainingTypeRepository)
        {
            _TrainingTypeRepository = TrainingTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedTrainingTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.TrainingType> TrainingTypes = await _TrainingTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = TrainingTypes.Select(x => new SelectedModel 
            {
                Name = x.TrainingTypeName,
                Id = x.TrainingTypeId
            }).ToList();
            return selectModels;
        }
    }
}
 