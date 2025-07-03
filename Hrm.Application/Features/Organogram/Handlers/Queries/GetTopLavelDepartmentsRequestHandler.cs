using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Department;
using Hrm.Application.DTOs.Organograms;
using Hrm.Application.Features.Organogram.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IMapper _mapper;
        public GetTopLavelDepartmentsRequestHandler(IHrmRepository<Hrm.Domain.Department> DepartmentRepository, IMapper mapper, IHrmRepository<Domain.Designation> designationRepository)
        {
            _DepartmentRepository = DepartmentRepository;
            _mapper = mapper;
            _DesignationRepository = designationRepository;
        }

        //  public async Task<List<DepartmentDto>> Handle(GetTopLavelDepartmentsRequest request, CancellationToken cancellationToken)
        //  {
        //      var departments = request.DepartmentId == 0
        //? _DepartmentRepository.FilterWithInclude(x => x.UpperDepartmentId == null)
        //: _DepartmentRepository.FilterWithInclude(x => x.UpperDepartmentId == request.DepartmentId);

        //      var departmentList = await departments.ToListAsync(cancellationToken);

        //      foreach (var dept in departmentList)
        //      {
        //          // Count designations for each department
        //          int count = await _DesignationRepository.CountAsync(x => x.DepartmentId == dept.DepartmentId);

        //          dept.DepartmentName = $"{dept.DepartmentName} ({count})";
        //      }
        //      var departmentDtos = _mapper.Map<List<DepartmentDto>>(departmentList);
        //      return departmentDtos;
        //  }

        public async Task<List<DepartmentDto>> Handle(GetTopLavelDepartmentsRequest request, CancellationToken cancellationToken)
        {
            var departments = request.DepartmentId == 0
                ? _DepartmentRepository.FilterWithInclude(x => x.UpperDepartmentId == null)
                : _DepartmentRepository.FilterWithInclude(x => x.UpperDepartmentId == request.DepartmentId);

            var departmentList = await departments.ToListAsync(cancellationToken);

            // Load all departments once (to traverse tree)
            var allDepartments =  _DepartmentRepository.FilterWithInclude(x=>true).ToList(); // or FilterWithInclude(x => true)

            foreach (var dept in departmentList)
            {
                // Get all descendant department IDs (including current one)
                var allDeptIds = GetAllChildDepartmentIds(allDepartments, dept.DepartmentId).ToList();
                allDeptIds.Add(dept.DepartmentId); // include self

                // Count designations for all relevant departments
                int count = await _DesignationRepository.CountAsync(x => allDeptIds.Contains(x.DepartmentId?? 0));

                dept.DepartmentName = $"{dept.DepartmentName} ({count})";
            }

            var departmentDtos = _mapper.Map<List<DepartmentDto>>(departmentList);
            return departmentDtos;
        }


        private List<int> GetAllChildDepartmentIds(List<Hrm.Domain.Department> allDepartments, int parentId)
        {
            var result = new List<int>();
            var children = allDepartments.Where(d => d.UpperDepartmentId == parentId).ToList();

            foreach (var child in children)
            {
                result.Add(child.DepartmentId);
                result.AddRange(GetAllChildDepartmentIds(allDepartments, child.DepartmentId));
            }

            return result;
        }


    }
}
