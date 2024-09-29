using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SiteSetting;
using Hrm.Application.Features.SiteSettings.Requests.Queries;
using Hrm.Domain;
using MediatR;


namespace Hrm.Application.Features.SiteSettings.Handlers.Queries
{
    public class GetSiteSettingsDetailRequestHandler : IRequestHandler<GetSiteSettingDetailRequest, SiteSettingDto>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<SiteSetting> _SiteSettingRepository;
        public GetSiteSettingsDetailRequestHandler(IHrmRepository<SiteSetting> SiteSettingRepository, IMapper mapper)
        {
            _SiteSettingRepository = SiteSettingRepository;
            _mapper = mapper;
        }
        public async Task<SiteSettingDto> Handle(GetSiteSettingDetailRequest request, CancellationToken cancellationToken)
        {
            var SiteSetting = await _SiteSettingRepository.Get(request.Id);
            return _mapper.Map<SiteSettingDto>(SiteSetting);
        }
    }
}
