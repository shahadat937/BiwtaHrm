using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SiteVisit;
using Hrm.Application.DTOs.Workday;
using Hrm.Application.Features.Workday.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Workday.Handlers.Queries
{
    public class GetWorkdayRequestHandler:IRequestHandler<GetWorkdayRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.Workday> _WorkdayRepository;
        private readonly IMapper _mapper;

        public GetWorkdayRequestHandler(IHrmRepository<Hrm.Domain.Workday> WorkdayRepsitory, IMapper mapper)
        {
            _WorkdayRepository = WorkdayRepsitory;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetWorkdayRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Workday> Workdays = _WorkdayRepository.Where(x => true)
                .Include(d => d.year)
                .Include(d => d.weekDay);

            Workdays = Workdays.OrderByDescending(x => x.WorkdayId);

            var WorkdayDto = _mapper.Map<List<WorkdayDto>>(await Workdays.ToListAsync());

            return WorkdayDto;
        }
    }
}
