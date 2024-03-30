using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Modules.Requests.Queries;
using Hrm.Domain;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Modules.Handlers.Queries
{
    public class GetSelectedModuleByTypeHandler : IRequestHandler<GetSelectedModuleRequests, List<SelectedModel>>
    {
        private readonly IHrmRepository<Module> _ModuleRepository;


        public GetSelectedModuleByTypeHandler(IHrmRepository<Module> ModuleRepository)
        {
            _ModuleRepository = ModuleRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedModuleRequests request, CancellationToken cancellationToken)
        {
            ICollection<Module> Modules = await _ModuleRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Modules.Select(x => new SelectedModel
            {
                Name = x.Title,
                Id = x.ModuleId
            }).ToList();
            return selectModels;
        }
    }
}
