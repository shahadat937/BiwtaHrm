using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SubDepartment;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.SubDepartment.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IHrmRepository<Hrm.Domain.Department> _DepartmentRepository;
        private readonly IMapper _mapper;
        public GetSubDepartmentRequestHandler(IHrmRepository<Hrm.Domain.SubDepartment> SubDepartmentRepository, IMapper mapper, IHrmRepository<Domain.Department> DepartmentRepository)
        {
            _SubDepartmentRepository = SubDepartmentRepository;
            _mapper = mapper;
            _DepartmentRepository = DepartmentRepository;
        }

        public async Task<object> Handle(GetSubDepartmentRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.SubDepartment> SubDepartments = _SubDepartmentRepository.FilterWithInclude(x => true);
            SubDepartments = SubDepartments.OrderByDescending(x => x.SubDepartmentId);

            var SubDepartmentDtos = new List<SubDepartmentDto>();

            foreach (var SubDepartment in SubDepartments)
            {
                var SubDepartmentDto = _mapper.Map<SubDepartmentDto>(SubDepartment);
                var DepartmentName = await GetDepartmentName(SubDepartment.DepartmentId);
                SubDepartmentDto.DepartmentName = DepartmentName;
                SubDepartmentDtos.Add(SubDepartmentDto);
            }

            return SubDepartmentDtos;
        }

        private async Task<string?> GetDepartmentName(int? DepartmentId)
        {
            if (DepartmentId.HasValue)
            {
                var Department = await _DepartmentRepository.Get(DepartmentId.Value);
                return Department?.DepartmentName;
            }
            return null;
        }
    }
}
