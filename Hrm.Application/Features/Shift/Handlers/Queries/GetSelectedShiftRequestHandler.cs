using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Shift.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Shift.Handlers.Queries
{ 
    public class GetSelectedShiftRequestHandler : IRequestHandler<GetSelectedShiftRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Shift> _ShiftRepository;


        public GetSelectedShiftRequestHandler(IHrmRepository<Hrm.Domain.Shift> ShiftRepository)
        {
            _ShiftRepository = ShiftRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedShiftRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Shift> Shifts = await _ShiftRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Shifts.Select(x => new SelectedModel 
            {
                Name = x.ShiftName,
                Id = x.ShiftId
            }).ToList();
            return selectModels;
        }
    }
}
 