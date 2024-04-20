using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Office.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Office.Handlers.Queries
{ 
    public class GetSelectedOfficeRequestHandler : IRequestHandler<GetSelectedOfficeRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Office> _OfficeRepository;


        public GetSelectedOfficeRequestHandler(IHrmRepository<Hrm.Domain.Office> OfficeRepository)
        {
            _OfficeRepository = OfficeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedOfficeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Office> Offices = await _OfficeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Offices.Select(x => new SelectedModel 
            {
                Name = x.OfficeName,
                Id = x.OfficeId
            }).ToList();
            return selectModels;
        }
    }
}
 