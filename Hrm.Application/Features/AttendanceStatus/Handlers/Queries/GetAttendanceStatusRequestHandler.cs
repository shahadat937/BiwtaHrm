using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.AttendanceStatus;
using Hrm.Application.Features.AttendanceStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.AttendanceStatus.Handlers.Queries
{
    public class GetAttendanceStatusRequestHandler:IRequestHandler<GetAttendanceStatusRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.AttendanceStatus> _AttendanceStatusRepository;
        public readonly IMapper _mapper;

        public GetAttendanceStatusRequestHandler (IHrmRepository<Domain.AttendanceStatus> attendanceStatusRepository, IMapper mapper)
        {
            _AttendanceStatusRepository = attendanceStatusRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAttendanceStatusRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.AttendanceStatus> AttendanceStatus = _AttendanceStatusRepository.Where(x => true)
            .OrderBy(x => x.AttendanceStatusId);

            var AttendanceStatusDtos = _mapper.Map<List<AttendanceStatusDto>>(AttendanceStatus);

            return AttendanceStatusDtos;
        }
    }
}
