using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Common.Validators;
using Hrm.Application.DTOs.SiteSetting;
using Hrm.Application.Features.SiteSettings.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SiteSettings.Handlers.Queries
{
    public class GetActiveSiteSettingRequestHandler : IRequestHandler<GetActiveSiteSettingRequest, object>
    {

        private readonly IHrmRepository<SiteSetting> _SiteSettingRepository;

        private readonly IMapper _mapper;

        public GetActiveSiteSettingRequestHandler(IHrmRepository<SiteSetting> SiteSettingRepository, IMapper mapper)
        {
            _SiteSettingRepository = SiteSettingRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetActiveSiteSettingRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();

            var SiteSettings = await _SiteSettingRepository.FindOneAsync(x => x.IsActive == true);


            var SiteSettingsDtos = _mapper.Map<SiteSettingDto>(SiteSettings);


            return SiteSettingsDtos;
        }
    }
}
