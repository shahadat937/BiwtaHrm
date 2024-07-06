using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Holidays;
using Hrm.Application.Features.Holidays.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Holidays.Handlers.Queries
{
    public class GetHolidaysByYearRequestHandler:IRequestHandler<GetHolidaysByYearRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.Holidays> _HolidayRepository;
        private readonly IMapper _mapper;

        public GetHolidaysByYearRequestHandler(IHrmRepository<Domain.Holidays> holidayRepository, IMapper mapper)
        {
            _HolidayRepository = holidayRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetHolidaysByYearRequest request, CancellationToken cancellationToken)
        {
            var holidays = _HolidayRepository.Where(x => x.Year.YearName == request.YearName)
                .Include(e => e.Year)
                .Include(hd => hd.Office)
                .Include(hd => hd.OfficeBranch)
                .OrderByDescending(e => e.HolidayId);

            var holidaydtos = _mapper.Map<List<HolidayDto>>(holidays);

            return holidaydtos;
        }
    }
}
