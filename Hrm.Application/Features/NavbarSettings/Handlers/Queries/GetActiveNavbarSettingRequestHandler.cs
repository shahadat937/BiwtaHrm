using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Common.Validators;
using Hrm.Application.DTOs.NavbarSetting;
using Hrm.Application.Features.NavbarSettings.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.NavbarSettings.Handlers.Queries
{
    public class GetActiveNavbarSettingRequestHandler : IRequestHandler<GetActiveNavbarSettingRequest, object>
    {

        private readonly IHrmRepository<NavbarSetting> _NavbarSettingRepository;

        private readonly IMapper _mapper;

        public GetActiveNavbarSettingRequestHandler(IHrmRepository<NavbarSetting> NavbarSettingRepository, IMapper mapper)
        {
            _NavbarSettingRepository = NavbarSettingRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetActiveNavbarSettingRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();

            var NavbarSettings = await _NavbarSettingRepository.Where(x => x.IsActive == true)
                .Include(x => x.NavbarThem)
                .FirstOrDefaultAsync();


            var NavbarSettingsDtos = _mapper.Map<NavbarSettingDto>(NavbarSettings);


            return NavbarSettingsDtos;
        }
    }
}
