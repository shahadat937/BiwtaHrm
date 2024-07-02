using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AttendanceType.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.AttendanceType.Handlers.Queries
{
    public class GetSelectedAttendanceTypeRequestHandler:IRequestHandler<GetSelectedAttendanceTypeRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.AttendanceType> _AttendanceTypeRepository;

        public GetSelectedAttendanceTypeRequestHandler(IHrmRepository<Domain.AttendanceType> attendanceTypeRepository)
        {
            _AttendanceTypeRepository = attendanceTypeRepository;
        }

        public async Task<object> Handle(GetSelectedAttendanceTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.AttendanceType> AttendanceTypes = await _AttendanceTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = AttendanceTypes.Select(x => new SelectedModel
            {
                Name = x.AttendanceTypeName,
                Id = x.AttendanceTypeId
            }).ToList();
            return selectModels;
        }
    }
}
