﻿using System;
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
using Hrm.Application.Enum;
using Hrm.Application.Features.AttendanceDevice.Interfaces;
using Hrm.Application.Features.AttendanceDevice.Requests.Commands;
using Hrm.Application.Helpers;
using Hrm.Infrastructure.SignalRHub;
using MediatR;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHrmRepository<Hrm.Domain.ShiftSetting> _shiftSettingRepository;
        private readonly IAttendanceDevice _attendanceDevice;
        private readonly IHubContext<NotificationHub> _notificationHub;
        private readonly IMapper _mapper;

        public DeviceTableDataCommandHandler(IUnitOfWork unitOfWork,
            IHrmRepository<Hrm.Domain.Workday> workDayRepo,
            IHrmRepository<Hrm.Domain.Holidays> holidayRepo,
            IHrmRepository<Hrm.Domain.CancelledWeekend> cancelledWeekendRepo,
            IHrmRepository<Hrm.Domain.Shift> shiftRepo,
            IMapper mapper, IAttendanceDevice attendanceDevice,
            IHubContext<NotificationHub> notificationHub,
            IHrmRepository<Hrm.Domain.ShiftSetting> shiftSettingRepository
            )
        {
            _unitOfWork = unitOfWork;
            _workDayRepo = workDayRepo;
            _holidayRepo = holidayRepo;
            _cancelledWeekendRepo = cancelledWeekendRepo;
            _shiftRepo = shiftRepo;
            _attendanceDevice = attendanceDevice;
            _mapper = mapper;
            _notificationHub = notificationHub;
            _shiftSettingRepository = shiftSettingRepository;
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

            await _notificationHub.Clients.All.SendAsync("newAtd", "new attendance available");
            //await _unitOfWork.Save();

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



            if (attendance == null)
            {
                var createAtdDto = new CreateAttendanceDto();
                createAtdDto.EmpId = employee.Id;
                createAtdDto.ShiftId = attShift.ShiftId;
                createAtdDto.AttendanceDate = PunchDto.PunchDate;
                createAtdDto.InTime = PunchDto.PunchTime;
                createAtdDto.DayTypeId = AttendanceHelper.SetDayTypeId(createAtdDto, _workDayRepo, _holidayRepo, _cancelledWeekendRepo);
                createAtdDto.AttendanceStatusId = AttendanceHelper.SetAttendanceStatusByShiftSetting(createAtdDto, _shiftSettingRepository);
                var attendanceRecord = _mapper.Map<Hrm.Domain.Attendance>(createAtdDto);


                attendanceRecord.ShiftSettingId = _shiftSettingRepository.Where(x => x.IsActive == true && x.ShiftTypeId == attendanceRecord.ShiftId).Select(x => x.Id).FirstOrDefault();

                await _unitOfWork.Repository<Hrm.Domain.Attendance>().Add(attendanceRecord);
                await _unitOfWork.Save();
            } else
            {
                if(attendance.InTime.HasValue && !attendance.OutTime.HasValue)
                {
                    if(attendance.InTime > PunchDto.PunchTime)
                    {
                        attendance.OutTime = attendance.InTime;
                        attendance.InTime = PunchDto.PunchTime;
                        attendance.AttendanceStatusId = GetAttendanceStatus(attendance);
                    } else
                    {
                        attendance.OutTime = PunchDto.PunchTime;
                    }
                } else if(attendance.InTime.HasValue && attendance.OutTime.HasValue)
                {
                    if(attendance.InTime > PunchDto.PunchTime)
                    {
                        attendance.InTime = PunchDto.PunchTime;
                        attendance.AttendanceStatusId = GetAttendanceStatus(attendance);
                    } else if(attendance.OutTime < PunchDto.PunchTime)
                    {
                        attendance.OutTime = PunchDto.PunchTime;
                    }
                } else if(!attendance.InTime.HasValue)
                {
                    attendance.InTime = PunchDto.PunchTime;
                    attendance.AttendanceStatusId = GetAttendanceStatus(attendance);
                }


                attendance.ShiftSettingId = _shiftSettingRepository.Where(x => x.IsActive == true && x.ShiftTypeId == attendance.ShiftId).Select(x => x.Id).FirstOrDefault();

                await _unitOfWork.Repository<Hrm.Domain.Attendance>().Update(attendance);
                await _unitOfWork.Save();
                return true;
            }

            return true;
        }

        private int? GetAttendanceStatus(Domain.Attendance dto)
        {
            CreateAttendanceDto createAtdDto = new CreateAttendanceDto();
            createAtdDto.EmpId = dto.EmpId;
            createAtdDto.ShiftId = dto.ShiftId;
            createAtdDto.AttendanceDate = dto.AttendanceDate;
            createAtdDto.InTime = dto.InTime;
            createAtdDto.OutTime = dto.OutTime;
            if(!dto.AttendanceStatusId.HasValue || (dto.AttendanceStatusId != (int) AttendanceStatusOption.OnSiteVisit && dto.AttendanceStatusId != (int) AttendanceStatusOption.OnLeave) )
            {
                return AttendanceHelper.SetAttendanceStatusByShiftSetting(createAtdDto, _shiftSettingRepository);
            }

            return dto.AttendanceStatusId;
        }

    }
}
