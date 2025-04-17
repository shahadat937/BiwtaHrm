using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Organograms;
using Hrm.Application.Features.Organogram.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Organogram.Handlers.Queries
{
    public class GetCountOfDepartmentDesignationSectionRequestHandler : IRequestHandler<GetCountOfDepartmentDesignationSectionRequest, OrganogramDesignationDepartmentAndSectionCount>
    {
        private readonly IHrmRepository<Hrm.Domain.Department> _DepartmentRepository;
        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IHrmRepository<Hrm.Domain.Section> _SectionRepository;

        private readonly IMapper _mapper;
        public GetCountOfDepartmentDesignationSectionRequestHandler(IHrmRepository<Hrm.Domain.Department> DepartmentRepository, IMapper mapper, IHrmRepository<Hrm.Domain.Designation> DesignationRepository, IHrmRepository<Domain.Section> sectionRepository)
        {
            _DepartmentRepository = DepartmentRepository;
            _mapper = mapper;
            _DesignationRepository = DesignationRepository;
            _SectionRepository = sectionRepository;
        }
        public async Task<OrganogramDesignationDepartmentAndSectionCount> Handle(GetCountOfDepartmentDesignationSectionRequest request, CancellationToken cancellationToken)
        {
            var departmentCount = await _DepartmentRepository.Where(x => x.UpperDepartmentId == request.DepartmentId).CountAsync();
            var designationCount = await _DesignationRepository.Where(x => x.DepartmentId == request.DepartmentId && (request.SectionId == 0 ? x.SectionId == null : x.SectionId == request.SectionId)).CountAsync();
            var sectionCount = await _SectionRepository.Where(x => x.DepartmentId == request.DepartmentId && (request.SectionId == 0 ? x.UpperSectionId == null : x.UpperSectionId == request.SectionId)).CountAsync();

            var count = new OrganogramDesignationDepartmentAndSectionCount
            {
                DepartmentCount = departmentCount,
                DesignationCount = designationCount,
                SectionCount = sectionCount
            };
            return count;
        }
    }
}
