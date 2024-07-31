using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.Enum;
using Hrm.Application.Features.SiteVisit.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Helpers
{
    public class SiteVisitAtdHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public int siteVisitId {get; set; }
        public SiteVisitAtdHelper(IUnitOfWork unitOfWork, IMapper mapper) { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<Hrm.Domain.Attendance> getAttendance()
        {
            if(this.siteVisitId == 0)
            {
                return new List<Hrm.Domain.Attendance>();
            }

            var attendanceList = _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x => x.SiteVisitId == siteVisitId).ToList();

            return attendanceList;
        }

        public async Task<bool> deleteAttendance()
        {
            var attendanceList = this.getAttendance();
            foreach(var attendance in attendanceList)
            {
                await _unitOfWork.Repository<Hrm.Domain.Attendance>().Delete(attendance);
            }

            await _unitOfWork.Save();
            return true;
        }

        public async Task<bool> saveAttendance(DateOnly from, DateOnly to, int empId)
        {
            List<CreateAttendanceDto> list = new List<CreateAttendanceDto>();

            for(DateOnly curDate = from; curDate <= to; curDate = curDate.AddDays(1))
            {
                list.Add(new CreateAttendanceDto
                {
                    EmpId = empId,
                    AttendanceDate = curDate,
                    AttendanceStatusId = (int) AttendanceStatusOption.OnSiteVisit,
                    SiteVisitId = siteVisitId
                   
                });
            }

            var attendances = _mapper.Map<List<Hrm.Domain.Attendance>>(list);

            await _unitOfWork.Repository<Hrm.Domain.Attendance>().AddRangeAsync(attendances);
            await _unitOfWork.Save();
            return true;
        } 
    }
}
