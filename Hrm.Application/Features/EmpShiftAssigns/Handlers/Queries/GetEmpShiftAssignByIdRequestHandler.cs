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
            var EmpShiftAssign = _EmpShiftAssignRepository.Where(x => x.EmpId == request.Id)
                .Include(x => x.EmpBasicInfo)
                    .ThenInclude(ebi => ebi.EmpJobDetail)
                    .ThenInclude(ejd => ejd.Department)
                .Include(x => x.EmpBasicInfo)
                    .ThenInclude(ebi => ebi.EmpJobDetail)
                    .ThenInclude(ejd => ejd.Designation)
                .Include(x => x.Shift)
                .FirstOrDefaultAsync(cancellationToken);

            if (EmpShiftAssign == null)
            {
                return null;
            }

            var EmpShiftAssignDto = _mapper.Map<EmpShiftAssignDto>(EmpShiftAssign);

            return EmpShiftAssignDto;
        }
    }
}