using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Year.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Year.Handlers.Queries
{ 
    public class GetSelectedHolidayTypeRequestHandler : IRequestHandler<GetSelectedHolidayTypeRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.HolidayType> _HolidayTypeRepository;


        public GetSelectedHolidayTypeRequestHandler(IHrmRepository<Hrm.Domain.HolidayType> HolidayTypeRepository)
        {
            _HolidayTypeRepository = HolidayTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedHolidayTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.HolidayType> Years = await _HolidayTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Years.Select(x => new SelectedModel 
            {
                Name = x.HolidayTypeName,
                Id = x.HolidayTypeId
            }).ToList();
            return selectModels;
        }
    }
}
 