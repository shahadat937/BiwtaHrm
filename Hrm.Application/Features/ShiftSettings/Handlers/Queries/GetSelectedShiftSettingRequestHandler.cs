using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.ShiftSettings.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ShiftSettings.Handlers.Queries
{ 
    public class GetSelectedShiftSettingRequestHandler : IRequestHandler<GetSelectedShiftSettingRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.ShiftSetting> _ShiftSettingRepository;


        public GetSelectedShiftSettingRequestHandler(IHrmRepository<Hrm.Domain.ShiftSetting> ShiftSettingRepository)
        {
            _ShiftSettingRepository = ShiftSettingRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedShiftSettingRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.ShiftSetting> ShiftSettings = await _ShiftSettingRepository.FilterAsync(x => x.IsActive == true);
            List<SelectedModel> selectModels = ShiftSettings.Select(x => new SelectedModel 
            {
                Name = x.SettingName,
                Id = x.Id
            }).ToList();
            return selectModels;
        }
    }
}
 