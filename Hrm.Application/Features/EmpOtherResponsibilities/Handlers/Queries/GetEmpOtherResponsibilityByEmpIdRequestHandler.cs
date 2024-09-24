using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpOtherResponsibilities.Requests.Queries;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.EmpOtherResponsibility;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.EmpOtherResponsibilities.Handlers.Queries
{
    public class GetEmpOtherResponsibilityByEmpIdRequestHandler : IRequestHandler<GetEmpOtherResponsibilityByEmpIdRequest, List<EmpOtherResponsibilityDto>>
    {

        private readonly IHrmRepository<EmpOtherResponsibility> _EmpOtherResponsibilityRepository;
        private readonly IMapper _mapper;
        public GetEmpOtherResponsibilityByEmpIdRequestHandler(IHrmRepository<EmpOtherResponsibility> EmpOtherResponsibilityRepository, IMapper mapper)
        {
            _EmpOtherResponsibilityRepository = EmpOtherResponsibilityRepository;
            _mapper = mapper;
        }

        public async Task<List<EmpOtherResponsibilityDto>> Handle(GetEmpOtherResponsibilityByEmpIdRequest request, CancellationToken cancellationToken)
        {
            List<EmpOtherResponsibility> EmpOtherResponsibilities = await _EmpOtherResponsibilityRepository.Where(x => x.EmpId == request.Id)
                .Include(x => x.Office)
                .Include(x => x.Department)
                .Include(x => x.Section)
                .Include(x => x.Designation)
                .ToListAsync(cancellationToken);

            if (EmpOtherResponsibilities == null)
            {
                return null;
            }

            List<EmpOtherResponsibilityDto> result = _mapper.Map<List<EmpOtherResponsibilityDto>>(EmpOtherResponsibilities);

            return result;
        }
    }
}