using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Upazila.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Upazila.Handlers.Queries
{ 
    public class GetSelectedUpazilaRequestHandler : IRequestHandler<GetSelectedUpazilaRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Upazila> _UpazilaRepository;


        public GetSelectedUpazilaRequestHandler(IHrmRepository<Hrm.Domain.Upazila> UpazilaRepository)
        {
            _UpazilaRepository = UpazilaRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedUpazilaRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Upazila> Upazilas = await _UpazilaRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Upazilas.Select(x => new SelectedModel 
            {
                Name = x.UpazilaName,
                Id = x.UpazilaId
            }).ToList();
            return selectModels;
        }
    }
}
 