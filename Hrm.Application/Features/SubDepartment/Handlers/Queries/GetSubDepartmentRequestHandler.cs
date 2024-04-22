using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.SubDepartment;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.SubDepartment.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubDepartment.Handlers.Queries
{
    public class GetSubDepartmentRequestHandler : IRequestHandler<GetSubDepartmentRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.SubDepartment> _SubDepartmentRepository;
        private readonly IMapper _mapper;
        public GetSubDepartmentRequestHandler(IHrmRepository<Hrm.Domain.SubDepartment> SubDepartmentRepository, IMapper mapper)
        {
            _SubDepartmentRepository = SubDepartmentRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetSubDepartmentRequest request, CancellationToken cancellationToken)
        {
           
                // Fetch SubDepartment groups from repository
                IQueryable<Hrm.Domain.SubDepartment> SubDepartments = _SubDepartmentRepository.Where(x => true);

               // Order SubDepartment groups by descending order
               SubDepartments = SubDepartments.OrderByDescending(x => x.SubDepartmentId);

                // Map the SubDepartment blood groups to BloodGroupDto
                var SubDepartmentDtos = _mapper.Map<List<SubDepartmentDto>>(SubDepartments);

                return SubDepartmentDtos;
        }
    }
}
