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
    public class GetHolidaysRequestHandler:IRequestHandler<GetHolidaysRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.Holidays> _HolidaysRepository;
        private readonly IMapper _mapper;

        public GetHolidaysRequestHandler(IHrmRepository<Domain.Holidays> holidaysRepository, IMapper mapper)
        {
            _HolidaysRepository = holidaysRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetHolidaysRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Holidays> holidays = _HolidaysRepository.Where(x => true)
                .Include(hd => hd.Year)
                .Include(hd => hd.HolidayType)
                .OrderByDescending(x => x.HolidayId);

            var HolidaysDto = _mapper.Map<List<HolidayDto>>(holidays);

            return HolidaysDto;
        }
    }
}
