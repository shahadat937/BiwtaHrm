using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.Leaves.Requests.Queries;
using Hrm.Application.DTOs.Leave;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.Leaves.Handlers.Queries
{
    public class GetLeaveDetailRequestHandler : IRequestHandler<GetLeaveDetailRequest, LeaveDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Leave> _LeaveRepository;
        public GetLeaveDetailRequestHandler(IHrmRepository<Hrm.Domain.Leave> LeaveRepository, IMapper mapper)
        {
            _LeaveRepository = LeaveRepository;
            _mapper = mapper;
        }
        public async Task<LeaveDto> Handle(GetLeaveDetailRequest request, CancellationToken cancellationToken)
        {
            var Leave = await _LeaveRepository.Get(request.LeaveId);
            return _mapper.Map<LeaveDto>(Leave);
        }
    }
}
