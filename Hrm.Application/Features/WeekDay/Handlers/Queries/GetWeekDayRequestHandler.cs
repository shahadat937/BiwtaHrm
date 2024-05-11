using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.WeekDay;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Weekend.Handlers.Queries
{
    public class GetWeekDayRequestHandler : IRequestHandler<GetWeekDayRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.WeekDay> _WeekendRepository;
        private readonly IMapper _mapper;
        public GetWeekDayRequestHandler(IHrmRepository<Hrm.Domain.WeekDay> WeekendRepository, IMapper mapper)
        {
            _WeekendRepository = WeekendRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetWeekDayRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.WeekDay> WeekDay = _WeekendRepository.Where(x => true);

            var WeekendDtos = _mapper.Map<List<WeekDayDto>>(WeekDay);

            return WeekendDtos;
        }
    }
}
