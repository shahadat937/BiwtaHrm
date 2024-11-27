using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.LeaveRequest.Requests.Queries;
using Hrm.Domain;
using Hrm.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.LeaveRequest;

namespace Hrm.Application.Features.LeaveRequest.Handlers.Queries
{
    public class GetLeaveRequestByFilterRequestHandler: IRequestHandler<GetLeaveRequestByFilterRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.LeaveRequest> _LeaveRequestRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestByFilterRequestHandler(IHrmRepository<Domain.LeaveRequest> leaveRequestRepository, IMapper mapper)
        {
            _LeaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetLeaveRequestByFilterRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.LeaveRequest> leaveRequesQuery = _LeaveRequestRepository.Where(x => true)
                .Include(l => l.Employee)
                .Include(l => l.Country)
                .Include(l => l.LeaveType)
                .AsQueryable();

            if(request.filterDto==null)
            {
                throw new BadRequestException("Query String should be given");
            }

            if(request.filterDto.LeaveTypeId.HasValue)
            {
                leaveRequesQuery = leaveRequesQuery.Where(x=> x.LeaveTypeId == request.filterDto.LeaveTypeId);
            }

            if(request.filterDto.LeaveRequestId.HasValue)
            {
                leaveRequesQuery = leaveRequesQuery.Where(x=> x.LeaveRequestId == request.filterDto.LeaveRequestId);
            }

            if(request.filterDto.FromDate.HasValue&&request.filterDto.ToDate.HasValue)
            {
                leaveRequesQuery = leaveRequesQuery.Where(x => x.FromDate == request.filterDto.FromDate && x.ToDate == request.filterDto.ToDate);
            }

            if(request.filterDto.EmpId.HasValue)
            {
                leaveRequesQuery = leaveRequesQuery.Where(x=>x.EmpId == request.filterDto.EmpId);
            }

            if(request.filterDto.ReviewedBy.HasValue)
            {
                leaveRequesQuery = leaveRequesQuery.Where(x=>x.ReviewedBy == request.filterDto.ReviewedBy || x.ReviewedBy == null);
            }

            if(request.filterDto.ApprovedBy.HasValue)
            {
                leaveRequesQuery = leaveRequesQuery.Where(x=>x.ApprovedBy == request.filterDto.ApprovedBy || x.ApprovedBy == null);
            }

            if(request.filterDto.Status!=null&&request.filterDto.Status.Count>0)
            {
                leaveRequesQuery = leaveRequesQuery.Where(x => request.filterDto.Status.Contains((int)x.Status));
            }

            var LeaveRequests = await leaveRequesQuery.OrderByDescending(x => x.LeaveRequestId).ToListAsync();
            var leaveRequestdto = _mapper.Map<List<LeaveRequestDto>>(LeaveRequests);

            return leaveRequestdto;
        }
    }
}
