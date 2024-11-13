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
    public class GetInActiveEmpOtherResponsibilityRequestHandler : IRequestHandler<GetInActiveEmpOtherResponsibilityRequest, List<EmpOtherResponsibilityDto>>
    {

        private readonly IHrmRepository<EmpOtherResponsibility> _EmpOtherResponsibilityRepository;
        private readonly IMapper _mapper;
        public GetInActiveEmpOtherResponsibilityRequestHandler(IHrmRepository<EmpOtherResponsibility> EmpOtherResponsibilityRepository, IMapper mapper)
        {
            _EmpOtherResponsibilityRepository = EmpOtherResponsibilityRepository;
            _mapper = mapper;
        }

        public async Task<List<EmpOtherResponsibilityDto>> Handle(GetInActiveEmpOtherResponsibilityRequest request, CancellationToken cancellationToken)
        {
            List<EmpOtherResponsibility> EmpOtherResponsibilities = await _EmpOtherResponsibilityRepository.Where(x => x.EmpId == request.Id && x.ServiceStatus == false)
                .Include(x => x.ResponsibilityType)
                .Include(x => x.Office)
                .Include(x => x.Department)
                .Include(x => x.Section)
                .Include(x => x.Designation)
                    .ThenInclude(ds => ds.DesignationSetup)
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