using AutoMapper;
using MediatR;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.ShiftSettings.Requests.Queries;
using Hrm.Application.DTOs.Common.Validators;
using Hrm.Application.DTOs.ShiftSetting;
using Hrm.Application.Models;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.ShiftSettings.Handlers.Queries
{
    public class GetShiftSettingListRequestHandler : IRequestHandler<GetShiftSettingListRequest, object>
    {

        private readonly IHrmRepository<ShiftSetting> _ShiftSettingRepository;

        private readonly IMapper _mapper;

        public GetShiftSettingListRequestHandler(IHrmRepository<ShiftSetting> ShiftSettingRepository, IMapper mapper)
        {
            _ShiftSettingRepository = ShiftSettingRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetShiftSettingListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();

            IQueryable<ShiftSetting> ShiftSettings = _ShiftSettingRepository.Where(x => true)
                .Include(x => x.ShiftType);

            ShiftSettings = ShiftSettings.OrderByDescending(x => x.IsActive).ThenByDescending(x => x.Id);

            var ShiftSettingsDtos = _mapper.Map<List<ShiftSettingDto>>(ShiftSettings);


            return ShiftSettingsDtos;
        }
    }
}
