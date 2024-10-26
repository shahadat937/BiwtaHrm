using AutoMapper;
using MediatR;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.NavbarSettings.Requests.Queries;
using Hrm.Application.DTOs.Common.Validators;
using Hrm.Application.DTOs.NavbarSetting;
using Hrm.Application.Models;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.NavbarSettings.Handlers.Queries
{
    public class GetNavbarSettingListRequestHandler : IRequestHandler<GetNavbarSettingListRequest, object>
    {

        private readonly IHrmRepository<NavbarSetting> _NavbarSettingRepository;

        private readonly IMapper _mapper;

        public GetNavbarSettingListRequestHandler(IHrmRepository<NavbarSetting> NavbarSettingRepository, IMapper mapper)
        {
            _NavbarSettingRepository = NavbarSettingRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetNavbarSettingListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();

            IQueryable<NavbarSetting> NavbarSettings = _NavbarSettingRepository.Where(x => true);

            NavbarSettings = NavbarSettings.OrderByDescending(x => x.IsActive).ThenByDescending(x => x.Id);

            var NavbarSettingsDtos = _mapper.Map<List<NavbarSettingDto>>(NavbarSettings);


            return NavbarSettingsDtos;
        }
    }
}
