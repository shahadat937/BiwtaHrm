using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.Features.Attendance.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Handlers.Queries
{
    public class GetAttendanceRequestHandler: IRequestHandler<GetAttendanceRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.Attendance> _AttendanceRepository;
        private readonly IMapper _mapper;

        public GetAttendanceRequestHandler(IHrmRepository<Hrm.Domain.Attendance> attendanceRepository, IMapper mapper)
        {
            _AttendanceRepository = attendanceRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAttendanceRequest request, CancellationToken cancellationToken)
        {
            var attendances = _AttendanceRepository.Where(at => true)
                .Include(at => at.AttendanceType)
                .Include(at => at.EmpBasicInfo)
                    .ThenInclude(em=>em.EmpJobDetail)
                .Include(at => at.Office)
                .Include(at => at.OfficeBranch)
                .Include(at => at.ShiftType)
                .Include(at => at.ShiftType)
                    .ThenInclude(ss => ss.ShiftSetting)
                .Include(at => at.DayType)
                .Include(at => at.AttendanceStatus)
                .AsQueryable();

            if(request.Filters.keyword==null)
            {
                request.Filters.keyword = "";
            }

            attendances = attendances.Where(x => x.EmpBasicInfo.IdCardNo.ToLower().Contains(request.Filters.keyword) || (x.EmpBasicInfo.FirstName + " " + x.EmpBasicInfo.LastName).ToLower().Contains(request.Filters.keyword)||request.Filters.keyword=="");

            if(request.Filters.DepartmentId.HasValue)
            {
                attendances = attendances.Where(x => x.EmpBasicInfo.EmpJobDetail.FirstOrDefault().DepartmentId == request.Filters.DepartmentId);
            }

            if(request.Filters.EmpId.HasValue)
            {
                attendances = attendances.Where(x => x.EmpId == request.Filters.EmpId);
            }

            
            if(request.Filters.Month.HasValue)
            {
                attendances = attendances.Where(x=>x.AttendanceDate.Month == request.Filters.Month);
            }

            if(request.Filters.Year.HasValue)
            {
                attendances = attendances.Where(x=>x.AttendanceDate.Year == request.Filters.Year);
            }

            // sort the data
            if(request.Filters.sortColumn!=null&&request.Filters.sortDirection!=null)
            {
                attendances = GetSortedAttendance(attendances,request.Filters.sortDirection,request.Filters.sortColumn);
            } else
            {
                attendances = attendances.OrderByDescending(x => x.AttendanceId);

            }

            int total = await attendances.CountAsync();


            if(request.Filters.PageSize.HasValue&&request.Filters.PageIndex.HasValue)
            {
                int skipFrom = (int)(request.Filters.PageSize * Math.Max(0,(int) request.Filters.PageIndex - 1));

                attendances = attendances.Skip(skipFrom).Take((int)request.Filters.PageSize);
            }



            var AttendancesDtos =  _mapper.Map<List<AttendanceDto>>(attendances);

            return new {TotalCount=total,Result =  AttendancesDtos};

            //return AttendancesDtos;
        }

        private IQueryable<Domain.Attendance> GetSortedAttendance(IQueryable<Domain.Attendance> record, string sortDirection, string sortColumn)
        {
            var sortMappings = new Dictionary<string, Func<IQueryable<Domain.Attendance>, IOrderedQueryable<Domain.Attendance>>>
            {
                { "fullName", attendance => attendance.OrderBy(a=>a.EmpBasicInfo.FirstName).ThenBy(a => a.EmpBasicInfo.LastName) },
                { "attendanceDate", attendance => attendance.OrderBy(a => a.AttendanceDate) },
                { "inTime", attendance => attendance.OrderBy(a => a.InTime) },
                { "outTime", attendance => attendance.OrderBy(a => a.OutTime) },
                { "dayTypeName", attendance => attendance.OrderBy(a => a.DayType.DayTypeName) },
                { "attendanceStatusName", attendance => attendance.OrderBy(a => a.AttendanceStatus.AttendanceStatusName) }
            };

            var sortMappingsDesc = new Dictionary<string, Func<IQueryable<Domain.Attendance>, IOrderedQueryable<Domain.Attendance>>>
            {
                { "fullName", attendance => attendance.OrderByDescending(a => a.EmpBasicInfo.FirstName).ThenByDescending(a => a.EmpBasicInfo.LastName) },
                { "attendanceDate", attendance => attendance.OrderByDescending(a => a.AttendanceDate) },
                { "inTime", attendance => attendance.OrderByDescending(a => a.InTime) },
                { "outTime", attendance => attendance.OrderByDescending(a => a.OutTime) },
                { "dayTypeName", attendance => attendance.OrderByDescending(a => a.DayType.DayTypeName) },
                { "attendanceStatusName", attendance => attendance.OrderByDescending(a => a.AttendanceStatus.AttendanceStatusName) }
            };

            var sortFunc = sortDirection == "asc" ? sortMappings[sortColumn]: sortMappingsDesc[sortColumn];

            var sortedAttendance = sortFunc(record);

            return sortedAttendance;
        }
    }
}
