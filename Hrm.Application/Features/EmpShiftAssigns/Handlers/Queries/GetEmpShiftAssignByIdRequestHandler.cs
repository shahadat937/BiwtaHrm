using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpShiftAssign;
using Hrm.Application.Features.Employee.Requests.Queries;
using Hrm.Application.Features.EmpShiftAssigns.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpShiftAssigns.Handlers.Queries
{
    public class GetEmpShiftAssignByIdRequestHandler : IRequestHandler<GetEmpShiftAssignByIdRequest, object>
    {
        private readonly IHrmRepository<EmpShiftAssign> _EmpShiftAssignRepository;
        private readonly IMapper _mapper;

        public GetEmpShiftAssignByIdRequestHandler(IHrmRepository<EmpShiftAssign> EmpShiftAssignRepository, IMapper mapper)
        {
            _EmpShiftAssignRepository = EmpShiftAssignRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpShiftAssignByIdRequest request, CancellationToken cancellationToken)
        {
            var EmpShiftAssign = await _EmpShiftAssignRepository.FindOneAsync(x => x.Id == request.Id);


            return EmpShiftAssign;
        }
    }
}