using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Thana.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana.Handlers.Queries
{ 
    public class GetSelectedThanaRequestHandler : IRequestHandler<GetSelectedThanaRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Thana> _ThanaRepository;


        public GetSelectedThanaRequestHandler(IHrmRepository<Hrm.Domain.Thana> ThanaRepository)
        {
            _ThanaRepository = ThanaRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedThanaRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Thana> Thanas = await _ThanaRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Thanas.Select(x => new SelectedModel 
            {
                Name = x.ThanaName,
                Id = x.ThanaId
            }).ToList();
            return selectModels;
        }
    }
}
 