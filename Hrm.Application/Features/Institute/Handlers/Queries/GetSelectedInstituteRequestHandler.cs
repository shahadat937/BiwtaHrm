using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Institute.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Institute.Handlers.Queries
{ 
    public class GetSelectedInstituteRequestHandler : IRequestHandler<GetSelectedInstituteRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Institute> _InstituteRepository;


        public GetSelectedInstituteRequestHandler(IHrmRepository<Hrm.Domain.Institute> InstituteRepository)
        {
            _InstituteRepository = InstituteRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedInstituteRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Institute> Institutes = await _InstituteRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Institutes.Select(x => new SelectedModel 
            {
                Name = x.InstituteName,
                Id = x.InstituteId
            }).ToList();
            return selectModels;
        }
    }
}
 