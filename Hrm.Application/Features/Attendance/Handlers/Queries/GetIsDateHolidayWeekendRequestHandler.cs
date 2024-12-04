using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Attendance.Requests.Queries;
using Hrm.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Handlers.Queries
{
    public class GetIsDateHolidayWeekendRequestHandler: IRequestHandler<GetIsDateHolidayWeekendRequest,object>
    {
        private readonly IHrmRepository<Domain.Holidays> _HolidayRepo;
        private readonly IHrmRepository<Domain.Workday> _WorkdayRepo;
        private readonly IHrmRepository<Domain.CancelledWeekend> _CancelledWeekendRepo;
        private readonly IMapper _mapper;

        public GetIsDateHolidayWeekendRequestHandler(IHrmRepository<Domain.Holidays> HolidayRepo, IHrmRepository<Domain.Workday> WorkdayRepo, IHrmRepository<Domain.CancelledWeekend> CancelledWeekendRepo, IMapper mapper)
        {
            _HolidayRepo = HolidayRepo;
            _WorkdayRepo = WorkdayRepo;
            _CancelledWeekendRepo = CancelledWeekendRepo;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetIsDateHolidayWeekendRequest request, CancellationToken cancellationToken)
        {
            bool[] IsWeekendOrHolidays = Enumerable.Repeat(false, 32).ToArray();
            DateOnly from = new DateOnly(request.Year, request.Month, 1);
            DateOnly to = new DateOnly(request.Year, request.Month, DateTime.DaysInMonth(request.Year, request.Month));
            for(DateOnly curDate = from; curDate <= to; curDate = curDate.AddDays(1))
            {
                int day = curDate.Day;
                IsWeekendOrHolidays[day] = AttendanceHelper.IsHoliday(curDate,_HolidayRepo) | (!AttendanceHelper.IsWeekDay(curDate, _WorkdayRepo, _CancelledWeekendRepo));
            }

            return IsWeekendOrHolidays;
        }
    }
}
