using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.DTOs.Dashboard.Widgets;
using Hrm.Application.Enum;
using Hrm.Application.Features.Dashboards.Requests.Queries;
using Hrm.Domain;  // Ensure this is the correct namespace
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Dashboards.Handlers.Queries
{
    public class GetDashboardWidgetsRequestHandler : IRequestHandler<GetDashboardWidgetsRequest, object>
    {
        private readonly IHrmRepository<Domain.Attendance> _AttendanceRepository;
        private readonly IHrmRepository<Domain.EmpBasicInfo> _EmpBasicInfoRepository;
        private readonly IMapper _mapper;

        public GetDashboardWidgetsRequestHandler(
            IHrmRepository<Domain.Attendance> AttendanceRepository,
            IMapper mapper,
            IHrmRepository<EmpBasicInfo> empBasicInfoRepository)
        {
            _AttendanceRepository = AttendanceRepository;
            _mapper = mapper;
            _EmpBasicInfoRepository = empBasicInfoRepository;
        }

        public async Task<object> Handle(GetDashboardWidgetsRequest request, CancellationToken cancellationToken)
        {
            // Step 1: Get the last 7 days' dates as DateOnly
            var last7Days = Enumerable.Range(0, 7)
                .Select(i => DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-i)))
                .OrderBy(date => date)
                .ToList();

            // Step 2: Fetch attendance data for the last 7 days using DateOnly comparison
            var attendanceRecords = await _AttendanceRepository
                .Where(a => last7Days.Contains(a.AttendanceDate))
                .ToListAsync(cancellationToken);

            // Step 3: Fetch all employee IDs from EmpBasicInfoRepository
            var allEmployeeIds = await _EmpBasicInfoRepository
                .Where(x => true)
                .Select(e => e.Id)
                .ToListAsync(cancellationToken);

            // Step 4: Group and count data by date and attendance status
            var attendanceGroupedByStatus = attendanceRecords
                .GroupBy(a => a.AttendanceStatusId)
                .Select(group => new
                {
                    AttendanceStatusId = group.Key,
                    DailyCounts = group.GroupBy(g => g.AttendanceDate)
                                       .Select(g => new { Date = g.Key, Count = g.Count() })
                                       .OrderBy(d => d.Date)
                                       .ToList()
                }).ToList();

            // Step 5: Initialize result list with all enum values
            var result = new List<WidgetsDto>();

            // Ensure the AttendanceStatusOption enum is accessible here
            var attendanceStatusOptions = AttendanceStatusOption.GetValues(typeof(AttendanceStatusOption))
                                              .Cast<AttendanceStatusOption>();

            foreach (var statusOption in attendanceStatusOptions)
            {
                // Determine WidgetName and Label based on AttendanceStatusOption
                var widgetName = statusOption.ToString();
                var label = $"Total {widgetName}";

                List<int> data = new List<int>();
                List<string> labels = last7Days.Select(date => date.ToString("dd-MM-yyyy")).ToList();

                if (statusOption == AttendanceStatusOption.Absent)
                {
                    // Calculate absent count for each day
                    foreach (var date in last7Days)
                    {
                        var presentEmployeeIds = attendanceRecords
                            .Where(a => a.AttendanceDate == date)
                            .Select(a => a.EmpId)
                            .Distinct()
                            .ToList();

                        // Count Ids from EmpBasicInfoRepository that are NOT in presentEmployeeIds
                        var absentCount = allEmployeeIds
                            .Count(empId => !presentEmployeeIds.Contains(empId));

                        data.Add(absentCount);
                    }
                }
                else if (statusOption == AttendanceStatusOption.Present)
                {
                    // Calculate "Present" count which includes Present, Late, and OnSiteVisit
                    foreach (var date in last7Days)
                    {
                        var presentCount = attendanceRecords
                            .Where(a => a.AttendanceDate == date &&
                                        (a.AttendanceStatusId == (int)AttendanceStatusOption.Present ||
                                         a.AttendanceStatusId == (int)AttendanceStatusOption.Late ||
                                         a.AttendanceStatusId == (int)AttendanceStatusOption.OnSiteVisit))
                            .Count();

                        data.Add(presentCount);
                    }
                }
                else
                {
                    // Fetch data for current status option from grouped data
                    var statusGroup = attendanceGroupedByStatus.FirstOrDefault(s => s.AttendanceStatusId == (int)statusOption);

                    data = last7Days.Select(date =>
                        statusGroup?.DailyCounts.FirstOrDefault(dc => dc.Date == date)?.Count ?? 0).ToList();
                }

                // Add the DTO to the result list
                result.Add(new WidgetsDto
                {
                    WidgetName = widgetName + " Today",
                    Label = label,
                    Labels = labels,
                    Data = data,
                    TotalPercentage = data.Sum()
                });
            }

            return result;
        }
    }
}
