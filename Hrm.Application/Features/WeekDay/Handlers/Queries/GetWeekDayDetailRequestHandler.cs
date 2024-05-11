using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.WeekDay;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Application.Features.WeekDay.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.WeekDay.Handlers.Queries
{
    public class GetWeekDayDetailRequestHandler : IRequestHandler<GetWeekDayDetailRequest, WeekDayDto>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.WeekDay> _WeekDayRepository;
        public GetWeekDayDetailRequestHandler(IHrmRepository<Hrm.Domain.WeekDay> WeekDayRepository, IMapper mapper)
        {
            _WeekDayRepository = WeekDayRepository;
            _mapper = mapper;
        }
        public async Task<WeekDayDto> Handle(GetWeekDayDetailRequest request, CancellationToken cancellationToken)
        {
            var WeekDay = await _WeekDayRepository.Get(request.WeekDayId);
            return _mapper.Map<WeekDayDto>(WeekDay);
        }
    }
}

