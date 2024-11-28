using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.AttDevice;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.Features.AttendanceDevice.Interfaces;
using Hrm.Application.Features.AttendanceDevice.Requests.Commands;
using Hrm.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Commands
{
    public class DeviceTableDataCommandHandler : IRequestHandler<DeviceTableDataCommand,object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHrmRepository<Hrm.Domain.Workday> _workDayRepo;
        private readonly IHrmRepository<Hrm.Domain.Holidays> _holidayRepo;
        private readonly IHrmRepository<Hrm.Domain.CancelledWeekend> _cancelledWeekendRepo;
        private readonly IHrmRepository<Hrm.Domain.Shift> _shiftRepo;
        private readonly IAttendanceDevice _attendanceDevice;
        private readonly IMapper _mapper;

        public DeviceTableDataCommandHandler(IUnitOfWork unitOfWork,
            IHrmRepository<Hrm.Domain.Workday> workDayRepo,
            IHrmRepository<Hrm.Domain.Holidays> holidayRepo,
            IHrmRepository<Hrm.Domain.CancelledWeekend> cancelledWeekendRepo,
            IHrmRepository<Hrm.Domain.Shift> shiftRepo,
            IMapper mapper, IAttendanceDevice attendanceDevice)
        {
            _unitOfWork = unitOfWork;
            _workDayRepo = workDayRepo;
            _holidayRepo = holidayRepo;
            _cancelledWeekendRepo = cancelledWeekendRepo;
            _shiftRepo = shiftRepo;
            _attendanceDevice = attendanceDevice;
            _mapper = mapper;
        }

        public async Task<object> Handle(DeviceTableDataCommand request, CancellationToken cancellationToken)
        {
            var IsAuthorizedDevice = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.SN == request.SN && x.Status == true).AnyAsync();

            if(!IsAuthorizedDevice)
            {
                return "Invalid Options";
            }

            if(request.Table != "ATTLOG")
            {
                return "OK\n";
            }

            List<AttPunchDto> AttPunches = await _attendanceDevice.ParseDeviceAttendance(request.RequestBody);
            int count = 0;
            foreach(var punch in AttPunches)
            {
                if(await this.HandleAttendance(punch))
                {
                    count = count + 1;
                }
            }
            await _unitOfWork.Save();

            return $"OK:{count}\n";
        }

        private async Task<bool> HandleAttendance(AttPunchDto PunchDto)
        {
            var employee = await _unitOfWork.Repository<Hrm.Domain.EmpBasicInfo>().Where(x => x.IdCardNo == PunchDto.IdCardNo).FirstOrDefaultAsync();

            if(employee==null)
            {
                return false;
            }

            var attShift = await _unitOfWork.Repository<Hrm.Domain.EmpShiftAssign>().Where(x => x.EmpId == employee.Id).FirstOrDefaultAsync();

            if(attShift == null)
            {
                return false;
            }

            var attendance = await _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x => x.EmpId == employee.Id && x.AttendanceDate == PunchDto.PunchDate).FirstOrDefaultAsync();

            if(attendance == null)
            {
                var createAtdDto = new CreateAttendanceDto();
                createAtdDto.EmpId = employee.Id;
                createAtdDto.ShiftId = attShift.ShiftId;
                createAtdDto.AttendanceDate = PunchDto.PunchDate;
                createAtdDto.InTime = PunchDto.PunchTime;
                createAtdDto.DayTypeId = AttendanceHelper.SetDayTypeId(createAtdDto, _workDayRepo, _holidayRepo, _cancelledWeekendRepo);
                createAtdDto.AttendanceStatusId = AttendanceHelper.SetAttendanceStatus(createAtdDto, _shiftRepo);
                var attendanceRecord = _mapper.Map<Hrm.Domain.Attendance>(createAtdDto);
                await _unitOfWork.Repository<Hrm.Domain.Attendance>().Add(attendanceRecord);
            } else
            {
                if(attendance.InTime.HasValue)
                {
                    attendance.OutTime = PunchDto.PunchTime;
                } else
                {
                    attendance.InTime = PunchDto.PunchTime;
                }
                await _unitOfWork.Repository<Hrm.Domain.Attendance>().Update(attendance);
                return true;
            }

            return true;
        }
    }
}
