using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.CancelledWeekend;
using Hrm.Application.Features.CancelledWeekend.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.CancelledWeekend.Handlers.Queries
{
    public class GetCancelledWeekendByFilterRequestHandler: IRequestHandler<GetCancelledWeekendByFilterRequest, List<CancelledWeekendDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.CancelledWeekend> _repository;
        private readonly IMapper _mapper;

        public GetCancelledWeekendByFilterRequestHandler(IHrmRepository<Domain.CancelledWeekend> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CancelledWeekendDto>> Handle(GetCancelledWeekendByFilterRequest request, CancellationToken cancellationToken)
        {
            var querable = _repository.Where(x => true)
                .Include(x => x.Employee).AsQueryable();
            if(request.filters.Id.HasValue)
            {
                querable = querable.Where(x=>x.Id == request.filters.Id);
            }

            if(request.filters.DateFrom.HasValue&&request.filters.DateTo.HasValue)
            {
                querable = querable.Where(x=> x.CancelDate>=request.filters.DateFrom && x.CancelDate<=request.filters.DateTo);
            }

            if(request.filters.Month.HasValue)
            {
                querable = querable.Where(x=> x.CancelDate.Month ==  request.filters.Month);
            }

            if(request.filters.Year.HasValue)
            {
                querable = querable.Where(x=>x.CancelDate.Year ==  request.filters.Year);
            }

            if(request.filters.IsActive.HasValue)
            {
                querable = querable.Where(x=>x.IsActive == request.filters.IsActive);
            }

            var cancelledWeekends = await querable.ToListAsync();

            var cancelledWeekendDtos = _mapper.Map<List<CancelledWeekendDto>>(cancelledWeekends);

            return cancelledWeekendDtos;
        }
    }
}
