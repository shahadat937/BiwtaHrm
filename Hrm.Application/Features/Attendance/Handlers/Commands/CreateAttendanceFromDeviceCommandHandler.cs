using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance.Validators;
using Hrm.Application.Features.Attendance.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using Hrm.Application.Enum;
using Hrm.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.Attendance;

namespace Hrm.Application.Features.Attendance.Handlers.Commands
{
    public class CreateAttendanceFromDeviceCommandHandler: IRequestHandler<CreateAttendanceFromDeviceCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHrmRepository<Hrm.Domain.Workday> _WorkdayRepository;
        private readonly IHrmRepository<Hrm.Domain.Holidays> _HolidayRepository;
        private readonly IHrmRepository<Hrm.Domain.Shift> _ShiftRepository;
        private readonly IHrmRepository<Hrm.Domain.CancelledWeekend> _cancelledWeekendRepo;
        private readonly IMapper _mapper;

        public CreateAttendanceFromDeviceCommandHandler(IUnitOfWork unitOfWork, 
            IHrmRepository<Hrm.Domain.Workday> WorkdayRepository,
            IHrmRepository<Hrm.Domain.Holidays> HolidayRepository,
            IHrmRepository<Hrm.Domain.CancelledWeekend> cancelledWeekendRepo,
            IHrmRepository<Hrm.Domain.Shift> ShiftRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _WorkdayRepository = WorkdayRepository;
            _HolidayRepository = HolidayRepository;
            _cancelledWeekendRepo = cancelledWeekendRepo;
            _ShiftRepository = ShiftRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateAttendanceFromDeviceCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateAttendanceDtoValidator();
            var validationResult = await validator.ValidateAsync(request.Attendancedto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }
            


            //Set DayType
            request.Attendancedto.DayTypeId = AttendanceHelper.SetDayTypeId(request.Attendancedto, _WorkdayRepository, _HolidayRepository, _cancelledWeekendRepo);

            // Set Attendance Status
            request.Attendancedto.AttendanceStatusId = AttendanceHelper.SetAttendanceStatus(request.Attendancedto, _ShiftRepository);

            // Set Work Hour
            request.Attendancedto.WorkHour = AttendanceHelper.SetWorkHour(request.Attendancedto);

            // Set OverTime
            request.Attendancedto.OverTime = AttendanceHelper.SetOverTime(request.Attendancedto, _ShiftRepository);


            var attendance = _mapper.Map<Hrm.Domain.Attendance>(request.Attendancedto);
            attendance = await _unitOfWork.Repository<Hrm.Domain.Attendance>().Add(attendance);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = attendance.AttendanceId;
            return response;
            

        }

       /* private bool IsWeekDay(DateOnly GivenDate)
        {
            var IsWeekDay = _WorkdayRepository.Where(x => x.weekDay.WeekDayName == GivenDate.DayOfWeek.ToString()).Any();
            return IsWeekDay;
        }

        private bool IsHoliday(DateOnly GivenDate)
        {
            var IsHoliday = _HolidayRepository.Where(x => x.Year.YearName == GivenDate.Year && x.HolidayStart <= GivenDate && x.HolidayEnd >= GivenDate).Any();
            return IsHoliday;
        }

        private int? SetAttendanceStatus(CreateAttendanceDto dto)
        {
            if(dto.ShiftId==null||dto.InTime==null)
            {
                return null;
            }

            var shift = _ShiftRepository.Get((int)dto.ShiftId).Result;

            if(shift==null)
            {
                return null;
            }

            if(dto.InTime>shift.AbsentTime)
            {
                return (int)AttendanceStatusOption.Absent;
            }

            if(dto.InTime>shift.BufferTime)
            {
                return (int)AttendanceStatusOption.Late;
            }

            return (int)AttendanceStatusOption.Present;
        } */
    }
}
