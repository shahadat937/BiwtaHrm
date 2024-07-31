using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Workday;
using Hrm.Application.Features.Workday.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Workday.Handlers.Queries
{
    public class GetSelectedWorkdayByYearRequestHandler: IRequestHandler<GetSelectedWorkdayByYearRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.Workday> _workdayRepository;
        private readonly IMapper _mapper;

        public GetSelectedWorkdayByYearRequestHandler(IHrmRepository<Domain.Workday> workdayRepository, IMapper mapper)
        {
            _workdayRepository = workdayRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetSelectedWorkdayByYearRequest request, CancellationToken cancellationToken)
        {

            var workdays = _workdayRepository.Where(x => x.YearId == request.yearId)
                .Include(x => x.weekDay)
                .AsQueryable();

           var workdaySelected = await workdays.Select(x => new SelectedModel
            {
                Id = x.WorkdayId,
                Name = x.weekDay.WeekDayName
            }).ToListAsync() ;

            return workdaySelected;
        }
    }
}
