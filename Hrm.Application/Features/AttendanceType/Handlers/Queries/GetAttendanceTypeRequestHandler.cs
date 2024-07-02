using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.AttendanceType;
using Hrm.Application.Features.AttendanceType.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.AttendanceType.Handlers.Queries
{
    public class GetAttendanceTypeRequestHandler: IRequestHandler<GetAttendanceTypeRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.AttendanceType> _AttendanceTypeRepository;
        private readonly IMapper _mapper;
        public GetAttendanceTypeRequestHandler(IHrmRepository<Domain.AttendanceType> attendanceTypeRepository, IMapper mapper)
        {
            _AttendanceTypeRepository = attendanceTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAttendanceTypeRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.AttendanceType> AttendanceType = _AttendanceTypeRepository.Where(x => true)
                .OrderBy(x => x.AttendanceTypeId);

            var AttendanceTypeDto = _mapper.Map<List<AttendanceTypeDto>>(AttendanceType);

            return AttendanceTypeDto;
        }
    }
}
