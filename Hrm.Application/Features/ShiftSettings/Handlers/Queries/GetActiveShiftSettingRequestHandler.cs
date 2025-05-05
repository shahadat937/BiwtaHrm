using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Common.Validators;
using Hrm.Application.DTOs.ShiftSetting;
using Hrm.Application.Features.ShiftSettings.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ShiftSettings.Handlers.Queries
{
    public class GetActiveShiftSettingRequestHandler : IRequestHandler<GetActiveShiftSettingRequest, object>
    {

        private readonly IHrmRepository<ShiftSetting> _ShiftSettingRepository;

        private readonly IMapper _mapper;

        public GetActiveShiftSettingRequestHandler(IHrmRepository<ShiftSetting> ShiftSettingRepository, IMapper mapper)
        {
            _ShiftSettingRepository = ShiftSettingRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetActiveShiftSettingRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();

            var ShiftSettings = _ShiftSettingRepository.Where(x => x.IsActive == true && x.ShiftTypeId == request.ShiftTypeId).Include(x => x.ShiftType);


            var ShiftSettingsDtos = _mapper.Map<ShiftSettingDto>(ShiftSettings);


            return ShiftSettingsDtos;
        }
    }
}
