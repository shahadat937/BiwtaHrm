using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.ResponsibilityTypes.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ResponsibilityTypes.Handlers.Queries
{ 
    public class GetSelectedResponsibilityTypeRequestHandler : IRequestHandler<GetSelectedResponsibilityTypeRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.ResponsibilityType> _ResponsibilityTypeRepository;


        public GetSelectedResponsibilityTypeRequestHandler(IHrmRepository<Hrm.Domain.ResponsibilityType> ResponsibilityTypeRepository)
        {
            _ResponsibilityTypeRepository = ResponsibilityTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedResponsibilityTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.ResponsibilityType> ResponsibilityTypes = await _ResponsibilityTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = ResponsibilityTypes.Select(x => new SelectedModel 
            {
                Name = x.Name,
                Id = x.Id
            }).ToList();
            return selectModels;
        }
    }
}
 