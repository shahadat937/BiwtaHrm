using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ShiftSetting;
using Hrm.Application.Features.ShiftSettings.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Hrm.Application.Features.ShiftSettings.Handlers.Queries
{
    public class GetShiftSettingsDetailRequestHandler : IRequestHandler<GetShiftSettingDetailRequest, ShiftSettingDto>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<ShiftSetting> _ShiftSettingRepository;
        public GetShiftSettingsDetailRequestHandler(IHrmRepository<ShiftSetting> ShiftSettingRepository, IMapper mapper)
        {
            _ShiftSettingRepository = ShiftSettingRepository;
            _mapper = mapper;
        }
        public async Task<ShiftSettingDto> Handle(GetShiftSettingDetailRequest request, CancellationToken cancellationToken)
        {
            var ShiftSetting = _ShiftSettingRepository.FilterWithInclude(x => x.Id == request.Id).Include(x => x.ShiftType);
            return _mapper.Map<ShiftSettingDto>(ShiftSetting);
        }
    }
}
