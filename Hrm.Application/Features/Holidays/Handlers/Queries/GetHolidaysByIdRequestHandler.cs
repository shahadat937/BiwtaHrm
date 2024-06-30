using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Holidays;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Features.Holidays;
using Hrm.Application.Features.Holidays.Requests.Queries;

namespace Hrm.Application.Features.Holidays.Handlers.Queries
{
    public class GetHolidaysByIdRequestHandler : IRequestHandler<GetHolidaysByIdRequest,HolidayDto>
    {
        private readonly IHrmRepository<Hrm.Domain.Holidays> _HolidaysRepository;
        private readonly IMapper _mapper;

        public GetHolidaysByIdRequestHandler(IHrmRepository<Domain.Holidays> holidaysRepository, IMapper mapper)
        {
            _HolidaysRepository = holidaysRepository;
            _mapper = mapper;
        }

        public async Task<HolidayDto> Handle(GetHolidaysByIdRequest request, CancellationToken cancellationToken )
        {
            var Holidays = await _HolidaysRepository.Get(request.HolidayId);
            var Holidaydto = _mapper.Map<HolidayDto>( Holidays );

            return Holidaydto;
        }
    }
}
