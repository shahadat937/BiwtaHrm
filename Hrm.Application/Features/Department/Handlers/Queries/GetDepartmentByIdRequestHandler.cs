using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Department;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Department.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Department.Handlers.Queries
{
    public class GetDepartmentByIdRequestHandler : IRequestHandler<GetDepartmentByIdRequest, DepartmentDto>
    {

        private readonly IHrmRepository<Hrm.Domain.Department> _DepartmentRepository;
        private readonly IMapper _mapper;
        public GetDepartmentByIdRequestHandler(IHrmRepository<Hrm.Domain.Department> DepartmentRepositoy, IMapper mapper)
        {
            _DepartmentRepository = DepartmentRepositoy;
            _mapper = mapper;
        }

        public async Task<DepartmentDto> Handle(GetDepartmentByIdRequest request, CancellationToken cancellationToken)
        {
            var Department = await _DepartmentRepository.Get(request.DepartmentId);
            return _mapper.Map<DepartmentDto>(Department);
        }
    }
}
