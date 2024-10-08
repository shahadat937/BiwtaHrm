using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.DesignationSetups.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DesignationSetups.Handlers.Queries
{ 
    public class GetSelectedDesignationSetupRequestHandler : IRequestHandler<GetSelectedDesignationSetupRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.DesignationSetup> _DesignationSetupRepository;


        public GetSelectedDesignationSetupRequestHandler(IHrmRepository<Hrm.Domain.DesignationSetup> DesignationSetupRepository)
        {
            _DesignationSetupRepository = DesignationSetupRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDesignationSetupRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.DesignationSetup> DesignationSetups = await _DesignationSetupRepository.FilterAsync(x => x.IsActive == true);
            List<SelectedModel> selectModels = DesignationSetups.Select(x => new SelectedModel 
            {
                Name = x.Name,
                Id = x.Id
            }).ToList();
            return selectModels;
        }
    }
}
 