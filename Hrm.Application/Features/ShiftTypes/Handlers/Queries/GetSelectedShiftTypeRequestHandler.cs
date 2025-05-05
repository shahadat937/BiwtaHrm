using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.ShiftTypes.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ShiftTypes.Handlers.Queries
{ 
    public class GetSelectedShiftTypeRequestHandler : IRequestHandler<GetSelectedShiftTypeRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.ShiftType> _ShiftTypeRepository;


        public GetSelectedShiftTypeRequestHandler(IHrmRepository<Hrm.Domain.ShiftType> ShiftTypeRepository)
        {
            _ShiftTypeRepository = ShiftTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedShiftTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.ShiftType> ShiftTypes = await _ShiftTypeRepository.FilterAsync(x => x.IsActive == true);
            List<SelectedModel> selectModels = ShiftTypes.Select(x => new SelectedModel 
            {
                Name = x.ShiftName,
                Id = x.Id
            }).ToList();
            return selectModels;
        }
    }
}
 