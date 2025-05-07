using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.Features.Attendance.Requests.Commands;
using Hrm.Application.Helpers;
using Hrm.Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Handlers.Commands
{
    public class UpdateAttendanceQueryRequestHandler : IRequestHandler<UpdateAttendanceQueryRequest, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Workday> _WorkdayRepository;
        private readonly IHrmRepository<Hrm.Domain.Holidays> _HolidaysRepository;
        private readonly IHrmRepository<Hrm.Domain.Shift> _ShiftRepository;
        private readonly IHrmRepository<Hrm.Domain.ShiftSetting> _shiftSettingRepository;
        private readonly IHrmRepository<Hrm.Domain.EmpShiftAssign> _shiftAssignRepository;
        private readonly IHrmRepository<Hrm.Domain.Attendance> _AttendanceRepository;
        private readonly IHrmRepository<Hrm.Domain.CancelledWeekend> _CancelledWeekendRepo;

        public UpdateAttendanceQueryRequestHandler(IUnitOfWork unitOfWork, IMapper mapper,
            IHrmRepository<Domain.Workday> workdayRepository,
            IHrmRepository<Domain.Holidays> holidaysRepository,
            IHrmRepository<Domain.Shift> shiftRepository,
            IHrmRepository<Domain.CancelledWeekend> cancelledWeekendRepo,
            IHrmRepository<Domain.Attendance> attendanceRepository,
            IHrmRepository<Hrm.Domain.ShiftSetting> shiftSettingRepository,
            IHrmRepository<Hrm.Domain.EmpShiftAssign> shiftAssignRepository
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _WorkdayRepository = workdayRepository;
            _HolidaysRepository = holidaysRepository;
            _ShiftRepository = shiftRepository;
            _AttendanceRepository = attendanceRepository;
            _CancelledWeekendRepo = cancelledWeekendRepo;
            _shiftSettingRepository = shiftSettingRepository;
            _shiftAssignRepository = shiftAssignRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateAttendanceQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var attendanceList = await _AttendanceRepository.Where(x => x.AttendanceDate >= request.FromDate && x.AttendanceDate <= request.ToDate && (x.AttendanceStatusId != 4 || x.AttendanceStatusId != 5)).ToListAsync();

            if (attendanceList.Count() == 0)
            {
                response.Success = false;
                response.Message = "No Record Found";
            }
            else
            {
                foreach (var attendance in attendanceList)
                {
                    var attendanceDto = _mapper.Map<CreateAttendanceDto>(attendance);

                    attendance.ShiftId = await _shiftAssignRepository.Where(x => x.EmpId == attendance.EmpId).Select(x => x.ShiftId).FirstOrDefaultAsync();

                    attendanceDto.ShiftId = attendance.ShiftId;

                    attendance.DayTypeId = AttendanceHelper.SetDayTypeId(attendanceDto, _WorkdayRepository, _HolidaysRepository, _CancelledWeekendRepo);

                    attendance.AttendanceStatusId = AttendanceHelper.SetAttendanceStatusByShiftSetting(attendanceDto, _shiftSettingRepository);

                    attendance.WorkHour = AttendanceHelper.SetWorkHour(attendanceDto);

                    attendance.OverTime = AttendanceHelper.SetOverTimeByShiftSetting(attendanceDto, _shiftSettingRepository);

                    attendance.ShiftSettingId = await _shiftSettingRepository.Where(x => x.IsActive == true && x.ShiftTypeId == attendance.ShiftId).Select(x => x.Id).FirstOrDefaultAsync();

                    await _unitOfWork.Repository<Hrm.Domain.Attendance>().Update(attendance);

                }

                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Update Successful";
            }
            return response;
        }

    }
}
