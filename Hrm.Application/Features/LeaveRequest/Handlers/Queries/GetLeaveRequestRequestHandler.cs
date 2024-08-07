using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.LeaveRequest;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application;

public class GetLeaveRequestHandler : IRequestHandler<GetLeaveRequestRequest, List<LeaveRequestDto>>
{
    private readonly IHrmRepository<Hrm.Domain.LeaveRequest> _LeaveRequestRepository;
    private readonly IMapper _mapper;

    public GetLeaveRequestHandler(IHrmRepository<Hrm.Domain.LeaveRequest> LeaveRequestRepository, IMapper mapper) {
        _LeaveRequestRepository = LeaveRequestRepository;
        _mapper = mapper;
    }
    public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestRequest request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _LeaveRequestRepository.Where(x=>true)
                            .Include(x=>x.Employee)
                            .Include(x=>x.Country)
                            .Include(x=>x.LeaveType)
                            .ToListAsync();
        
        var leaveRequestDto = _mapper.Map<List<LeaveRequestDto>>(leaveRequest);

        return leaveRequestDto;
    }
}
