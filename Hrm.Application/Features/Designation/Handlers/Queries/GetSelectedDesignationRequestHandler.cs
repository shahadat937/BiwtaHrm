using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Designations.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designations.Handlers.Queries
{ 
    public class GetSelectedDesignationRequestHandler : IRequestHandler<GetSelectedDesignationRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;


        public GetSelectedDesignationRequestHandler(IHrmRepository<Hrm.Domain.Designation> DesignationRepository)
        {
            _DesignationRepository = DesignationRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDesignationRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Designation> Designations = await _DesignationRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Designations.Select(x => new SelectedModel 
            {
                Name = x.DesignationName,
                Id = x.DesignationId
            }).ToList();
            return selectModels;
        }
    }
}
 