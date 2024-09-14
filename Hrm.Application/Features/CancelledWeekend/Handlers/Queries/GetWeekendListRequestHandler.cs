using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.CancelledWeekend.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.CancelledWeekend.Handlers.Queries
{
    public class GetWeekendListRequestHandler: IRequestHandler<GetWeekendListRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.CancelledWeekend> _CancelledWeekendRepo;
        private readonly IHrmRepository<Hrm.Domain.Workday> _WorkdayRepo;
        private readonly IHrmRepository<Hrm.Domain.Year> _YearRepo; 
        

        public GetWeekendListRequestHandler(IHrmRepository<Domain.CancelledWeekend> cancelledWeekendRepo, IHrmRepository<Domain.Workday> workdayRepo, IHrmRepository<Hrm.Domain.Year> yearRepo)
        {
            _CancelledWeekendRepo = cancelledWeekendRepo;
            _WorkdayRepo = workdayRepo;
            _YearRepo = yearRepo;
        }

        public async Task<object> Handle(GetWeekendListRequest request, CancellationToken cancellationToken)
        {
            var weekend = await _WorkdayRepo.Where(x => x.YearId == request.YearId)
                .Include(x=>x.weekDay)
                .ToListAsync();
            var year = await _YearRepo.Get(request.YearId);

            if(year == null)
            {
                throw new NotFoundException(nameof(year), request.YearId);
            }

            List<object> list = new List<object>();


            DateTime startDate = new DateTime(year.YearName, 1, 1);
            DateTime endDate = new DateTime(year.YearName, 12, 31);

            var CancelledWeekend = await _CancelledWeekendRepo.Where(x => x.CancelDate >= startDate && x.CancelDate <= endDate).ToListAsync();

            for(DateTime curDate = startDate; curDate<=endDate; curDate = curDate.AddDays(1))
            {
                if(weekend.Where(x=>x.weekDay.WeekDayName==curDate.DayOfWeek.ToString()).Any())
                {
                    list.Add(new
                    {
                        Date = curDate,
                        DayName = curDate.DayOfWeek.ToString(),
                        IsActive = !CancelledWeekend.Where(x => x.CancelDate == curDate).Any(),
                        CancelId = CancelledWeekend.Where(x=>x.CancelDate == curDate).FirstOrDefault()
                    });
                }
            }

            return list;
        }
    }
}
