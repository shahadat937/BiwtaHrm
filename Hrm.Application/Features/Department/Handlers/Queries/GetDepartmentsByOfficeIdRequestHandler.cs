using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Department;
using Hrm.Application.Features.Department.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Department.Handlers.Queries
{
    public class GetDepartmentsByOfficeIdRequestHandler : IRequestHandler<GetDepartmentsByOfficeIdRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Department> _DepartmentRepository;
        private readonly IMapper _mapper;
        public GetDepartmentsByOfficeIdRequestHandler(IHrmRepository<Hrm.Domain.Department> DepartmentRepositoy, IMapper mapper)
        {
            _DepartmentRepository = DepartmentRepositoy;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetDepartmentsByOfficeIdRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Department> Department = _DepartmentRepository.Where(x => x.OfficeId == request.OfficeId).OrderByDescending(x => x.DepartmentId);

            var DepartmentDtos = await Task.Run(() => _mapper.Map<List<DepartmentDto>>(Department));

            return DepartmentDtos;
        }
    }
}
