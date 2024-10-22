using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.NavbarThems.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.NavbarThems.Handlers.Queries
{ 
    public class GetSelectedNavbarThemRequestHandler : IRequestHandler<GetSelectedNavbarThemRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.NavbarThem> _NavbarThemRepository;


        public GetSelectedNavbarThemRequestHandler(IHrmRepository<Hrm.Domain.NavbarThem> NavbarThemRepository)
        {
            _NavbarThemRepository = NavbarThemRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedNavbarThemRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.NavbarThem> NavbarThems = await _NavbarThemRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = NavbarThems.Select(x => new SelectedModel 
            {
                Name = x.ThemName,
                Id = x.Id
            }).ToList();
            return selectModels;
        }
    }
}
 