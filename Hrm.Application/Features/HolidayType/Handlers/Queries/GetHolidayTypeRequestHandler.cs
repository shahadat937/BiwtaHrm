using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.HolidayType;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.HolidayType.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.HolidayType.Handlers.Queries
{
    public class GetHolidayTypeRequestHandler : IRequestHandler<GetHolidayTypeRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.HolidayType> _HolidayTypeRepository;
        private readonly IMapper _mapper;
        public GetHolidayTypeRequestHandler(IHrmRepository<Hrm.Domain.HolidayType> HolidayTypeRepository, IMapper mapper)
        {
            _HolidayTypeRepository = HolidayTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetHolidayTypeRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.HolidayType> HolidayTypes = _HolidayTypeRepository.Where(x => true);
            HolidayTypes = HolidayTypes.OrderByDescending(x => x.HolidayTypeId);

            var HolidayTypeDtos = _mapper.Map<List<HolidayTypeDto>>(HolidayTypes);

            return HolidayTypeDtos;
        }
    }
}
