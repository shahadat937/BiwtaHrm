using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Features.LeaveType.Requests.Queries;
using Hrm.Shared.Models;
using Hrm.Application.Contracts.Persistence;
using AutoMapper;
using System.Diagnostics.Contracts;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.DTOs.LeaveType;
using Hrm.Application.Features.GradeType.Requests.Queries;

namespace Hrm.Application.Features.LeaveType.Handlers.Queries
{
    public class GetLeaveTypeRequestHandler: IRequestHandler<GetLeaveTypeRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.LeaveType> _LeaveTypeRepository;
        private readonly IMapper _mapper;

        public GetLeaveTypeRequestHandler(IHrmRepository<Domain.LeaveType> leaveTypeRepository, IMapper mapper)
        {
            _LeaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetLeaveTypeRequest request, CancellationToken cancellationToken)
        {
            var leaveTypes =  _LeaveTypeRepository.Where(x => x.IsActive == true);

            if(request.ShowReport.HasValue)
            {
                leaveTypes = leaveTypes.Where(x => x.ShowReport == request.ShowReport);
            }

            

            var leaveTypeDtos = _mapper.Map<List<LeaveTypeDto>>(await leaveTypes.ToListAsync());

            return leaveTypeDtos;
        }


    }
}
