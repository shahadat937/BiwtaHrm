using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpOtherResponsibility;
using Hrm.Application.Features.EmpOtherResponsibilities.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpOtherResponsibilities.Handlers.Queries
{
    public class GetEmpOtherResponsibilityDetailsRequestHandler : IRequestHandler<GetEmpOtherResponsibilityDetailsRequest, EmpOtherResponsibilityDto>
    {

        private readonly IHrmRepository<EmpOtherResponsibility> _EmpOtherResponsibilityRepository;
        private readonly IMapper _mapper;
        public GetEmpOtherResponsibilityDetailsRequestHandler(IHrmRepository<EmpOtherResponsibility> EmpOtherResponsibilityRepository, IMapper mapper)
        {
            _EmpOtherResponsibilityRepository = EmpOtherResponsibilityRepository;
            _mapper = mapper;
        }

        public async Task<EmpOtherResponsibilityDto> Handle(GetEmpOtherResponsibilityDetailsRequest request, CancellationToken cancellationToken)
        {
            var EmpOtherResponsibilities = await _EmpOtherResponsibilityRepository.Where(x => x.Id == request.Id)
                .Include(x => x.ResponsibilityType)
                .Include(x => x.Office)
                .Include(x => x.Department)
                .Include(x => x.Section)
                .Include(x => x.Designation)
                    .ThenInclude(ds => ds.DesignationSetup)
                .FirstOrDefaultAsync();

            if (EmpOtherResponsibilities == null)
            {
                return null;
            }

            var result = _mapper.Map<EmpOtherResponsibilityDto>(EmpOtherResponsibilities);

            return result;
        }
    }
}