using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.LeaveType.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveType.Handlers.Queries
{
    internal class GetSelectedLeaveTypeRequestHandler: IRequestHandler<GetSelectedLeaveTypeRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.LeaveType> _LeaveTypeRepository;
        private readonly IMapper _mapper;

        public GetSelectedLeaveTypeRequestHandler(IHrmRepository<Domain.LeaveType> leaveTypeRepository, IMapper mapper)
        {
            _LeaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetSelectedLeaveTypeRequest request, CancellationToken cancellationToken)
        {
            var leaveTypeSelected = await _LeaveTypeRepository.Where(x => x.IsActive == true).Select(x => new SelectedModel
            {
                Id = x.LeaveTypeId,
                Name = x.LeaveTypeName
            }).ToListAsync();

            return leaveTypeSelected;
        }
    }
}
