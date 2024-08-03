using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.LeaveType.Requests.Queries;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.LeaveType;

namespace Hrm.Application.Features.LeaveType.Handlers.Queries
{
    public class GetLeaveTypeByIdRequestHandler: IRequestHandler<GetLeaveTypeByIdRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.LeaveType> _LeaveTypeRepository;
        private readonly IMapper _mapper;

        public GetLeaveTypeByIdRequestHandler(IHrmRepository<Domain.LeaveType> leaveTypeRepository, IMapper mapper)
        {
            _LeaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetLeaveTypeByIdRequest request, CancellationToken cancellationToken)
        {
            var leaveType = await _LeaveTypeRepository.Get(request.LeaveTypeId);
            if(leaveType == null)
            {
                throw new NotFoundException(nameof(LeaveType),request.LeaveTypeId);
            }

            var leaveTypedto = _mapper.Map<LeaveTypeDto>(leaveType);

            return leaveTypedto;
        }
    }
}
