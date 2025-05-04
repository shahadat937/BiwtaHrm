using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpShiftAssign;
using Hrm.Application.DTOs.Employee;
using Hrm.Application.Features.Employee.Requests.Queries;
using Hrm.Application.Features.EmpShiftAssigns.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.EmpShiftAssigns.Handlers.Queries
{
    public class GetAllEmpShiftAssignRequestHandler : IRequestHandler<GetAllEmpShiftAssignRequest, object>
    {

        private readonly IHrmRepository<EmpShiftAssign> _EmpShiftAssignRepository;
        private readonly IMapper _mapper;
        public GetAllEmpShiftAssignRequestHandler(IHrmRepository<EmpShiftAssign> EmpShiftAssignRepository, IMapper mapper)
        {
            _EmpShiftAssignRepository = EmpShiftAssignRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAllEmpShiftAssignRequest request, CancellationToken cancellationToken)
        {
            IQueryable<EmpShiftAssign> EmpShiftAssign = _EmpShiftAssignRepository.Where(x => true)
            .Include(x => x.EmpBasicInfo)
                .ThenInclude(ebi => ebi.EmpJobDetail)
                .ThenInclude(ejd => ejd.Department)
            .Include(x => x.EmpBasicInfo)
                .ThenInclude(ebi => ebi.EmpJobDetail)
                .ThenInclude(ejd => ejd.Designation)
                    .ThenInclude(ds => ds.DesignationSetup)
            .Include(x => x.ShiftType);

            EmpShiftAssign = EmpShiftAssign.OrderByDescending(x => x.Id);

            var EmpShiftAssignDtos = _mapper.Map<List<EmpShiftAssignDto>>(EmpShiftAssign);

            return EmpShiftAssignDtos;
        }
    }
}