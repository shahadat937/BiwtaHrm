using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Leave;
using Hrm.Application.DTOs.Leave;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Leave.Requests.Queries;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Leave.Handlers.Queries
{
    public class GetLeaveRequestHandler : IRequestHandler<GetLeaveRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Leave> _LeaveRepository;
        private readonly IMapper _mapper;
        public GetLeaveRequestHandler(IHrmRepository<Hrm.Domain.Leave> LeaveRepository, IMapper mapper)
        {
            _LeaveRepository = LeaveRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetLeaveRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Leave> Leave = _LeaveRepository.Where(x => true);

            var LeaveDtos = _mapper.Map<List<LeaveDto>>(Leave);

            return LeaveDtos;
        }
    }
}