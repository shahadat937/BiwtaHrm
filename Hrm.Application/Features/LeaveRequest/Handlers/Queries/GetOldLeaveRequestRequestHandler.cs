using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.LeaveRequest;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application;

public class GetOldLeaveRequestRequestHandler : IRequestHandler<GetOldLeaveRequestRequest, List<LeaveRequestDto>>
{
    private readonly IHrmRepository<Hrm.Domain.LeaveRequest> _LeaveRequestRepository;
    private readonly IMapper _mapper;

    public GetOldLeaveRequestRequestHandler(IHrmRepository<Hrm.Domain.LeaveRequest> LeaveRequestRepository, IMapper mapper) {
        _LeaveRequestRepository = LeaveRequestRepository;
        _mapper = mapper;
    }
    public async Task<List<LeaveRequestDto>> Handle(GetOldLeaveRequestRequest request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _LeaveRequestRepository.Where(x => x.IsOldLeave == true)
                            .Include(x=>x.Employee)
                            .Include(x=>x.Country)
                            .Include(x=>x.LeaveType)
                            .ToListAsync();
        
        var leaveRequestDto = _mapper.Map<List<LeaveRequestDto>>(leaveRequest);

        return leaveRequestDto;
    }
}
