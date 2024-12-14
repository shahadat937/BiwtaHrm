using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EyesColors.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EyesColors.Handlers.Queries
{ 
    public class GetSelectedEyesColorRequestHandler : IRequestHandler<GetSelectedEyesColorRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.EyesColor> _EyesColorRepository;


        public GetSelectedEyesColorRequestHandler(IHrmRepository<Hrm.Domain.EyesColor> EyesColorRepository)
        {
            _EyesColorRepository = EyesColorRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedEyesColorRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.EyesColor> EyesColors = await _EyesColorRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = EyesColors.Select(x => new SelectedModel 
            {
                Name = x.EyesColorName,
                Id = x.EyesColorId
            }).OrderBy(x => x.Name).ToList();
            return selectModels;
        }
    }
}
 