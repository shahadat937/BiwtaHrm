﻿using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Attendance.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using Hrm.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.Attendance.Validators;
using System.Runtime.InteropServices;
using Hrm.Application.DTOs.Attendance;
using Microsoft.EntityFrameworkCore;
using Hrm.Domain;
using Hrm.Application.Enum;

namespace Hrm.Application.Features.Attendance.Handlers.Commands
{
    public class CreateBulkAttendanceCommandHandler:IRequestHandler<CreateBulkAttendanceCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Workday> _workdayRepository;
        private readonly IHrmRepository<Hrm.Domain.Holidays> _holidayRepository;
        private readonly IHrmRepository<Hrm.Domain.Shift> _shiftRepository;
        private readonly IHrmRepository<Hrm.Domain.EmpShiftAssign> _empShiftAssignRepository;
        private readonly IHrmRepository<Hrm.Domain.ShiftSetting> _shiftSettingRepository;
        private readonly IHrmRepository<Hrm.Domain.CancelledWeekend> _cancelledWeekendRepository;

        public CreateBulkAttendanceCommandHandler(IUnitOfWork unitOfWork,
           IHrmRepository<Hrm.Domain.Workday> workdayRepository,
           IHrmRepository<Hrm.Domain.Holidays> holidayRepository,
           IHrmRepository<Hrm.Domain.Shift> shiftRepository,
           IHrmRepository<Hrm.Domain.CancelledWeekend> cancelledWeekendRepository,
           IMapper mapper,
           IHrmRepository<EmpShiftAssign> empShiftAssignRepository,
           IHrmRepository<ShiftSetting> shiftSettingRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _workdayRepository = workdayRepository;
            _holidayRepository = holidayRepository;
            _shiftRepository = shiftRepository;
            _cancelledWeekendRepository = cancelledWeekendRepository;
            _empShiftAssignRepository = empShiftAssignRepository;
            _shiftSettingRepository = shiftSettingRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateBulkAttendanceCommand request, CancellationToken cancellationToken)
        {
            if(request.csvFile == null || request.csvFile.Length <= 0 || !request.csvFile.ContentType.Equals("text/csv", StringComparison.CurrentCultureIgnoreCase))
            {
                
                throw new BadRequestException("File is null or file type is not supported");
            }


            var validator = new CreateAttendanceDtoValidator();

            List<CreateAttendanceDto> attendancedtos = [];
            var tempFilePath = "";
            try
            {
                tempFilePath = Path.GetTempFileName();

                using (var tempFileStream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await request.csvFile.CopyToAsync(tempFileStream);
                }

                try
                {
                    attendancedtos = await CsvFileHelper.GetRecords(tempFilePath);
                }
                catch(FormatException ex)
                {
                    throw new BadRequestException("Date Format is not correct");
                }
                catch (Exception ex)
                {
                    throw new BadRequestException("Got error gettting data from csv. Make sure all data format and header name are correct");
                    //Console.WriteLine(ex);
                } finally
                {
                    if(tempFilePath!= null && File.Exists(tempFilePath))
                    {
                        File.Delete(tempFilePath);
                    }
                }
            } catch (Exception ex)
            {
                if(tempFilePath != null && File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
                throw new Exception(ex.Message);
            }


            // Filter the list to exclude the weekend entry
            attendancedtos = attendancedtos.Where((attendance) =>
            {
                bool IsWeekend = _unitOfWork.Repository<Hrm.Domain.Workday>().Where(x => x.weekDay.WeekDayName == attendance.AttendanceDate.DayOfWeek.ToString() && x.year.YearName == attendance.AttendanceDate.Year).Any();

                bool IsCancelledWeekend = _unitOfWork.Repository<Hrm.Domain.CancelledWeekend>().Where(x => DateOnly.FromDateTime(x.CancelDate) == attendance.AttendanceDate).Any();

                if(attendance.InTime.HasValue==false && attendance.OutTime.HasValue==false)
                {
                    return false;
                }

                return !(!IsCancelledWeekend & IsWeekend);
            }).ToList();





            // set other attendance related field
            foreach(var attendance in attendancedtos)
            {
                attendance.AttendanceTypeId = 1; // Attendance Type Manual

                var employee = await _unitOfWork.Repository<Hrm.Domain.EmpBasicInfo>().Where(x => x.IdCardNo == attendance.Pmis).FirstOrDefaultAsync();

                if(employee == null)
                {
                    throw new NotFoundException("Employee",attendance.Pmis);
                }

                attendance.EmpId = employee.Id;
                attendance.ShiftId = await _empShiftAssignRepository.Where(x => x.EmpId == attendance.EmpId).Select(x => x.ShiftId).FirstOrDefaultAsync();

                if (!attendance.DayTypeId.HasValue)
                {
                    attendance.DayTypeId = AttendanceHelper.SetDayTypeId(attendance, _workdayRepository, _holidayRepository, _cancelledWeekendRepository);
                }

                if (!attendance.AttendanceStatusId.HasValue)
                {
                    attendance.AttendanceStatusId = AttendanceHelper.SetAttendanceStatusByShiftSetting(attendance, _shiftSettingRepository);
                }

                if (!attendance.WorkHour.HasValue)
                {
                    attendance.WorkHour = AttendanceHelper.SetWorkHour(attendance);
                }

                if (!attendance.OverTime.HasValue)
                {
                    attendance.OverTime = AttendanceHelper.SetOverTimeByShiftSetting(attendance, _shiftSettingRepository);
                }

                attendance.ShiftSettingId = await _shiftSettingRepository.Where(x => x.ShiftTypeId == attendance.ShiftId && x.IsActive == true).Select(x => x.Id).FirstOrDefaultAsync();
            }

            // Validate the attendance dto
            foreach (var attendance in attendancedtos)
            {

                var validationResult = await validator.ValidateAsync(attendance);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult);
                }
            }

            var attendances = _mapper.Map<List<Hrm.Domain.Attendance>>(attendancedtos);

            List<Hrm.Domain.Attendance> attendanceForadd = new List<Domain.Attendance>();
            
            foreach(var attendance in attendances)
            {
                var check = await _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x => x.EmpId == attendance.EmpId
                    && x.AttendanceDate == attendance.AttendanceDate).FirstOrDefaultAsync();

                if(check==null)
                {
                    attendanceForadd.Add(attendance);
                    continue;
                }

                if(check.AttendanceStatusId !=(int) AttendanceStatusOption.OnSiteVisit&&check.AttendanceStatusId!=(int)AttendanceStatusOption.OnLeave)
                {
                    continue;
                }

                check.InTime = attendance.InTime;
                check.OutTime = attendance.OutTime;

                await _unitOfWork.Repository<Hrm.Domain.Attendance>().Update(check);
            }

            await _unitOfWork.Repository<Hrm.Domain.Attendance>().AddRangeAsync(attendanceForadd);
            await _unitOfWork.Save();

            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Attendances Added";

            return response;
            
        }
    }
}
