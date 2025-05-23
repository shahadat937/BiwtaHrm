﻿using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance.Validators;
using Hrm.Application.Features.Attendance.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Helpers;

namespace Hrm.Application.Features.Attendance.Handlers.Commands
{
    public class CreateManualAttendanceCommandHandler : IRequestHandler<CreateManualAttendanceCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Workday> _WorkdayRepository;
        private readonly IHrmRepository<Hrm.Domain.Holidays> _HolidaysRepository;
        private readonly IHrmRepository<Hrm.Domain.Shift> _ShiftRepository;
        private readonly IHrmRepository<Hrm.Domain.ShiftSetting> _shiftSettingRepository;
        private readonly IHrmRepository<Hrm.Domain.CancelledWeekend> _CancelledWeekendRepo;

        public CreateManualAttendanceCommandHandler (IUnitOfWork unitOfWork, IMapper mapper, 
            IHrmRepository<Domain.Workday> workdayRepository, 
            IHrmRepository<Domain.Holidays> holidaysRepository,
            IHrmRepository<Domain.CancelledWeekend> cancelledWeekendRepo,
            IHrmRepository<Domain.Shift> shiftRepository,
            IHrmRepository<Hrm.Domain.ShiftSetting> shiftSettingRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _WorkdayRepository = workdayRepository;
            _HolidaysRepository = holidaysRepository;
            _ShiftRepository = shiftRepository;
            _CancelledWeekendRepo = cancelledWeekendRepo;
            _shiftSettingRepository = shiftSettingRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateManualAttendanceCommand request, CancellationToken cancellationToken)
        {
            if(request.Attendancedto==null)
            {
                throw new BadRequestException("Bad Request");
            }
            request.Attendancedto.AttendanceTypeId = 1; // Manual Attendance type should be set to 1 in database.

            var response = new BaseCommandResponse();
            var validator = new CreateAttendanceDtoValidator();
            var validationResult = await validator.ValidateAsync(request.Attendancedto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            if(!request.Attendancedto.DayTypeId.HasValue)
            {
                request.Attendancedto.DayTypeId = AttendanceHelper.SetDayTypeId(request.Attendancedto, _WorkdayRepository, _HolidaysRepository, _CancelledWeekendRepo);
            }

            if(!request.Attendancedto.AttendanceStatusId.HasValue)
            {
                request.Attendancedto.AttendanceStatusId = AttendanceHelper.SetAttendanceStatusByShiftSetting(request.Attendancedto, _shiftSettingRepository);
            }

            if(!request.Attendancedto.WorkHour.HasValue)
            {
                request.Attendancedto.WorkHour = AttendanceHelper.SetWorkHour(request.Attendancedto);
            }

            if(!request.Attendancedto.OverTime.HasValue)
            {
                request.Attendancedto.OverTime = AttendanceHelper.SetOverTimeByShiftSetting(request.Attendancedto, _shiftSettingRepository);
            }

            var attendance = _mapper.Map<Hrm.Domain.Attendance>(request.Attendancedto);

            attendance.ShiftSettingId = _shiftSettingRepository.Where(x => x.IsActive == true && x.ShiftTypeId == request.Attendancedto.ShiftId).Select(x => x.Id).FirstOrDefault();

            attendance = await _unitOfWork.Repository<Hrm.Domain.Attendance>().Add(attendance);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = attendance.AttendanceId;

            return response;

        }
    }
}
