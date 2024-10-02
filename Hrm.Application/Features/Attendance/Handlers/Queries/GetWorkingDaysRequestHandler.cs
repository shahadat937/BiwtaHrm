using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Attendance.Requests.Queries;
using Hrm.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Handlers.Queries
{
    public class GetWorkingDaysRequestHandler: IRequestHandler<GetWorkingDaysRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetWorkingDaysRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetWorkingDaysRequest request, CancellationToken cancellationToken)
        {
            int workingDay = await AttendanceHelper.calculateWorkingDay(request.From, request.To, request.From.Year, _unitOfWork);

            return workingDay;
        }
    }
}
