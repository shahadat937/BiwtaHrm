using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.DayType;
using Hrm.Application.Features.DayType.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DayType.Handlers.Queries
{
    public class GetDayTypeRequestHandler:IRequestHandler<GetDayTypeRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.DayType> _DayTypeRepository;
        private readonly IMapper _mapper;

        public GetDayTypeRequestHandler(IHrmRepository<Domain.DayType> dayTypeRepository, IMapper mapper)
        {
            _DayTypeRepository = dayTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetDayTypeRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.DayType> DayTypes = _DayTypeRepository.Where(x => true)
                .OrderByDescending(dt => dt.DayTypeId);

            return _mapper.Map<List<DayTypeDto>>(DayTypes);
        }
    }
}
