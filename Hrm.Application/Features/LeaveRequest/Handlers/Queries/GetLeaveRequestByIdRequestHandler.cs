    using AutoMapper;
using Hrm.Application.Exceptions;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.LeaveRequest;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Hrm.Application;

public class GetLeaveRequestByIdRequestHandler: IRequestHandler<GetLeaveRequestByIdRequest,LeaveRequestDto>
{
    private readonly IHrmRepository<Hrm.Domain.LeaveRequest> _LeaveRequestRepository;
    private readonly IMapper _mapper;

    public GetLeaveRequestByIdRequestHandler(IHrmRepository<Hrm.Domain.LeaveRequest> LeaveRequestRepository, IMapper mapper) {
        _LeaveRequestRepository = LeaveRequestRepository;
        _mapper = mapper;
    }
    public async Task<LeaveRequestDto> Handle(GetLeaveRequestByIdRequest request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _LeaveRequestRepository.Where(x => x.LeaveRequestId == request.LeaveRequestId)
            .Include(x => x.LeaveType)
            .Include(x => x.Country)
            .Include(x => x.Employee).ToListAsync();

        if (leaveRequest.Count()<=0)
        {
            throw new NotFoundException(nameof(leaveRequest),request.LeaveRequestId);
        }

        var leaveRequestDo = leaveRequest[0];
        
        var leaveRequestDto = _mapper.Map<LeaveRequestDto>(leaveRequestDo);

        return leaveRequestDto;
    }
}
