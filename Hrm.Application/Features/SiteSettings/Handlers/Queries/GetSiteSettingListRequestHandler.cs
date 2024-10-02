using AutoMapper;
using MediatR;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.SiteSettings.Requests.Queries;
using Hrm.Application.DTOs.Common.Validators;
using Hrm.Application.DTOs.SiteSetting;
using Hrm.Application.Models;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.SiteSettings.Handlers.Queries
{
    public class GetSiteSettingListRequestHandler : IRequestHandler<GetSiteSettingListRequest, object>
    {

        private readonly IHrmRepository<SiteSetting> _SiteSettingRepository;

        private readonly IMapper _mapper;

        public GetSiteSettingListRequestHandler(IHrmRepository<SiteSetting> SiteSettingRepository, IMapper mapper)
        {
            _SiteSettingRepository = SiteSettingRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetSiteSettingListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();

            IQueryable<SiteSetting> SiteSettings = _SiteSettingRepository.Where(x => true);

            SiteSettings = SiteSettings.OrderByDescending(x => x.IsActive).ThenByDescending(x => x.Id);

            var SiteSettingsDtos = _mapper.Map<List<SiteSettingDto>>(SiteSettings);


            return SiteSettingsDtos;
        }
    }
}
