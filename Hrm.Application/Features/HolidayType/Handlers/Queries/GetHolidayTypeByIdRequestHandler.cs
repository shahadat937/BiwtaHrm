using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.HolidayType;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.HolidayType.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.HolidayType.Handlers.Queries
{
    public class GetHolidayTypeByIdRequestHandler : IRequestHandler<GetHolidayTypeByIdRequest, HolidayTypeDto>
    {

        private readonly IHrmRepository<Hrm.Domain.HolidayType> _HolidayTypeRepository;
        private readonly IMapper _mapper;
        public GetHolidayTypeByIdRequestHandler(IHrmRepository<Hrm.Domain.HolidayType> HolidayTypeRepositoy, IMapper mapper)
        {
            _HolidayTypeRepository = HolidayTypeRepositoy;
            _mapper = mapper;
        }

        public async Task<HolidayTypeDto> Handle(GetHolidayTypeByIdRequest request, CancellationToken cancellationToken)
        {
            var HolidayType = await _HolidayTypeRepository.Get(request.HolidayTypeId);
            return _mapper.Map<HolidayTypeDto>(HolidayType);
        }
    }
}
