using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Queries;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.EmpJobDetail;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.EmpJobDetails.Handlers.Queries
{
    public class GetEmpJobDetailByIdRequestHandler : IRequestHandler<GetEmpJobDetailByIdRequest, object>
    {

        private readonly IHrmRepository<EmpJobDetail> _EmpJobDetailsRepository;
        private readonly IMapper _mapper;
        public GetEmpJobDetailByIdRequestHandler(IHrmRepository<EmpJobDetail> EmpJobDetailsRepository, IMapper mapper)
        {
            _EmpJobDetailsRepository = EmpJobDetailsRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpJobDetailByIdRequest request, CancellationToken cancellationToken)
        {
            var EmpJobDetails = await _EmpJobDetailsRepository.Where(x => x.EmpId == request.Id)
                .Include(x => x.Office)
                .Include(x => x.Department)
                .Include(x => x.Designation)
                    .ThenInclude(ds => ds.DesignationSetup)
                .Include(x => x.Section)
                .Include(x => x.PresentGrade)
                .Include(x => x.PresentScale)
                .Include(x => x.FirstDepartment)
                .Include(x => x.FirstSection)
                .Include(x => x.FirstDesignation)
                .Include(x => x.FirstGrade)
                .Include(x => x.FirstScale)
                .FirstOrDefaultAsync(cancellationToken);

            if (EmpJobDetails == null)
            {
                return null;
            }

            var EmpJobDetailsDto = _mapper.Map<EmpJobDetailDto>(EmpJobDetails);

            return EmpJobDetailsDto;
        }
    }
}