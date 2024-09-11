using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.Features.Attendance.Requests.Queries;
using Hrm.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Helpers;

namespace Hrm.Application.Features.Attendance.Handlers.Queries
{
    public class GetAttendanceSummaryByEmpRequestHandler: IRequestHandler<GetAttendanceSummaryByEmpRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.Attendance> _AttendanceRepository;
        private readonly IHrmRepository<Hrm.Domain.Holidays> _HolidayRepository;
        private readonly IHrmRepository<Hrm.Domain.EmpBasicInfo> _EmpBasicInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAttendanceSummaryByEmpRequestHandler(IHrmRepository<Hrm.Domain.Attendance> AttendanceRepository, 
            IHrmRepository<Hrm.Domain.Holidays> HolidaysRepository,
            IHrmRepository<Hrm.Domain.EmpBasicInfo> EmpBasicInfoRepository,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            _AttendanceRepository = AttendanceRepository;
            _HolidayRepository = HolidaysRepository;
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(GetAttendanceSummaryByEmpRequest request, CancellationToken cancellationToken)
        {
            var EmpInfo = await _EmpBasicInfoRepository.Get(request.AtdSummaryDto.EmpId);

            if(EmpInfo == null)
            {
                throw new NotFoundException(nameof(EmpInfo), request.AtdSummaryDto.EmpId);
            }

            var AtdSummary = new AttendanceSummaryDto();
            // Setting up basic fields
            AtdSummary.EmpId = request.AtdSummaryDto.EmpId;
            AtdSummary.From = request.AtdSummaryDto.From;
            AtdSummary.To = request.AtdSummaryDto.To;

            AtdSummary.EmpFirstName = EmpInfo.FirstName;
            AtdSummary.EmpLastName = EmpInfo.LastName;

            AtdSummary.TotalWorkHour =AtdReportHelper.totalWorkingHour(request.AtdSummaryDto.From, request.AtdSummaryDto.To, request.AtdSummaryDto.EmpId, _AttendanceRepository);

            AtdSummary.TotalOverTime = AtdReportHelper.totalOverTime(request.AtdSummaryDto.From, request.AtdSummaryDto.To, request.AtdSummaryDto.EmpId, _AttendanceRepository);

            AtdSummary.TotalAbsent = await AtdReportHelper.totalAbsent(request.AtdSummaryDto.From, request.AtdSummaryDto.To, request.AtdSummaryDto.EmpId, _AttendanceRepository, _HolidayRepository, _unitOfWork);

            AtdSummary.TotalWorkingDay = await AtdReportHelper.calculateWorkingDay(request.AtdSummaryDto.From, request.AtdSummaryDto.To, request.AtdSummaryDto.From.Year, _unitOfWork);

            AtdSummary.TotalPresent =(int) AtdSummary.TotalWorkingDay - AtdSummary.TotalAbsent;

            AtdSummary.TotalSiteVisit = AtdReportHelper.totalSiteVisit(request.AtdSummaryDto.From, request.AtdSummaryDto.To, request.AtdSummaryDto.EmpId, _AttendanceRepository);


            return AtdSummary;


        }
    }
}
