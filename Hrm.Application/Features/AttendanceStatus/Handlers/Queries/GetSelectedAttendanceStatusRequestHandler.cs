using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AttendanceStatus.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.AttendanceStatus.Handlers.Queries
{
    public class GetSelectedAttendanceStatusRequestHandler:IRequestHandler<GetSelectedAttendanceStatusRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.AttendanceStatus> _AttendanceStatusRepository;

        public GetSelectedAttendanceStatusRequestHandler(IHrmRepository<Domain.AttendanceStatus> attendanceStatusRepository)
        {
            _AttendanceStatusRepository = attendanceStatusRepository;
        }

        public async Task<object> Handle(GetSelectedAttendanceStatusRequest request,CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.AttendanceStatus> AttendanceStatus = _AttendanceStatusRepository.Where(x=>true);
            List<SelectedModel> selectModels = AttendanceStatus.Select(x => new SelectedModel
            {
                Name = x.AttendanceStatusName,
                Id = x.AttendanceStatusId
            }).ToList();
            return selectModels;
        }
    }
}
