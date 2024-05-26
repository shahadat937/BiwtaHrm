using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Department;
using Hrm.Application.Features.Department.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Department.Handlers.Queries
{
    public class GetDepartmentRequestHandler : IRequestHandler<GetDepartmentRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Department> _DepartmentRepository;
        private readonly IMapper _mapper;
        public GetDepartmentRequestHandler(IHrmRepository<Hrm.Domain.Department> DepartmentRepository, IMapper mapper)
        {
            _DepartmentRepository = DepartmentRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetDepartmentRequest request, CancellationToken cancellationToken)
        {
            var departments = _DepartmentRepository
            .Where(x => true)
            .Include(d => d.Office)
            .Include(d => d.UpperDepartment)
            .OrderByDescending(x => x.DepartmentId);

            var departmentDtos = _mapper.Map<List<DepartmentDto>>(await departments.ToListAsync(cancellationToken));

            return departmentDtos;
        }
    }
}
