using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Department;
using Hrm.Application.DTOs.Organograms;
using Hrm.Application.Features.Organogram.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Organogram.Handlers.Queries
{
    public class GetTopLavelDepartmentsRequestHandler : IRequestHandler<GetTopLavelDepartmentsRequest, List<DepartmentDto>>
    {

        private readonly IHrmRepository<Hrm.Domain.Department> _DepartmentRepository;
        private readonly IMapper _mapper;
        public GetTopLavelDepartmentsRequestHandler(IHrmRepository<Hrm.Domain.Department> DepartmentRepository, IMapper mapper)
        {
            _DepartmentRepository = DepartmentRepository;
            _mapper = mapper;
        }

        public async Task<List<DepartmentDto>> Handle(GetTopLavelDepartmentsRequest request, CancellationToken cancellationToken)
        {
            var topLavelDept = _DepartmentRepository.FilterWithInclude(x => x.UpperDepartmentId == null);
            var departmentDtos = _mapper.Map<List<DepartmentDto>>(topLavelDept);
            return departmentDtos;
        }
    }
}
