using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SubDepartment;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.SubDepartment.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubDepartment.Handlers.Queries
{
    public class GetSubDepartmentByIdRequestHandler : IRequestHandler<GetSubDepartmentByIdRequest, SubDepartmentDto>
    {

        private readonly IHrmRepository<Hrm.Domain.SubDepartment> _SubDepartmentRepository;
        private readonly IMapper _mapper;
        public GetSubDepartmentByIdRequestHandler(IHrmRepository<Hrm.Domain.SubDepartment> SubDepartmentRepositoy, IMapper mapper)
        {
            _SubDepartmentRepository = SubDepartmentRepositoy;
            _mapper = mapper;
        }

        public async Task<SubDepartmentDto> Handle(GetSubDepartmentByIdRequest request, CancellationToken cancellationToken)
        {
            var SubDepartment = await _SubDepartmentRepository.Get(request.SubDepartmentId);
            return _mapper.Map<SubDepartmentDto>(SubDepartment);
        }
    }
}
