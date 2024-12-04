using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.NavbarSetting;
using Hrm.Application.Features.NavbarSettings.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Hrm.Application.Features.NavbarSettings.Handlers.Queries
{
    public class GetNavbarSettingsDetailRequestHandler : IRequestHandler<GetNavbarSettingDetailRequest, NavbarSettingDto>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<NavbarSetting> _NavbarSettingRepository;
        public GetNavbarSettingsDetailRequestHandler(IHrmRepository<NavbarSetting> NavbarSettingRepository, IMapper mapper)
        {
            _NavbarSettingRepository = NavbarSettingRepository;
            _mapper = mapper;
        }
        public async Task<NavbarSettingDto> Handle(GetNavbarSettingDetailRequest request, CancellationToken cancellationToken)
        {
            var NavbarSetting = await _NavbarSettingRepository.Where(x => x.Id == request.Id)
                .Include(x => x.NavbarThem)
                .FirstOrDefaultAsync();
            return _mapper.Map<NavbarSettingDto>(NavbarSetting);
        }
    }
}
