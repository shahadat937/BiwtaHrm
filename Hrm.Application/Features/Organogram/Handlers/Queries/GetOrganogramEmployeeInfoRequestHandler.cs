using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpBasicInfo;
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
    public class GetOrganogramEmployeeInfoRequestHandler : IRequestHandler<GetOrganogramEmployeeInfoRequest, List<OrganogramEmployeeInfo>>
    {
  
        private readonly IHrmRepository<Hrm.Domain.EmpJobDetail> _EmpJobDetaildsRepository;
        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IHrmRepository<Hrm.Domain.EmpOtherResponsibility> _EmpOtherResponsibilityRepository;
        private readonly IMapper _mapper;
        public GetOrganogramEmployeeInfoRequestHandler( IMapper mapper, IHrmRepository<Hrm.Domain.EmpJobDetail> EmpJobDetaildsRepository, IHrmRepository<Domain.Designation> designationRepository, IHrmRepository<Domain.EmpOtherResponsibility> empOtherResponsibilityRepository)
        {

            _mapper = mapper;
            _EmpJobDetaildsRepository = EmpJobDetaildsRepository;
            _DesignationRepository = designationRepository;
            _EmpOtherResponsibilityRepository = empOtherResponsibilityRepository;
        }



        //public async Task<List<OrganogramEmployeeInfo>> Handle(GetOrganogramEmployeeInfoRequest request, CancellationToken cancellationToken)
        //{
        //    var designations = await _DesignationRepository
        //        .Where(d => d.DepartmentId == request.DepartmentId)
        //        .Include(d => d.DesignationSetup) // Ensure DesignationSetup is available
        //        .ToListAsync(cancellationToken);

        //    var result = new List<OrganogramEmployeeInfo>();

        //    foreach (var d in designations)
        //    {
        //        var emp = await _EmpJobDetaildsRepository
        //            .Where(e => e.DesignationId == d.DesignationId
        //                        && e.DepartmentId == request.DepartmentId
        //                        && e.ServiceStatus == true)
        //            .Include(e => e.EmpBasicInfo)
        //            .Select(e => new EmployeeInfo
        //            {
        //                EmpId = e.Id,
        //                EmployeeName = e.EmpBasicInfo.FirstName + " " + e.EmpBasicInfo.LastName
        //            })
        //            .FirstOrDefaultAsync(cancellationToken);

        //        result.Add(new OrganogramEmployeeInfo
        //        {
        //            Name = d.DesignationSetup?.Name,
        //            EmployeeInfo = emp
        //        });
        //    }

        //    return result;
        //}


        public async Task<List<OrganogramEmployeeInfo>> Handle(GetOrganogramEmployeeInfoRequest request, CancellationToken cancellationToken)
        {
            var designations = await _DesignationRepository
                             .Where(d => d.DepartmentId == request.DepartmentId
                              && (request.SectionId == 0 ? d.SectionId == null : d.SectionId == request.SectionId) && d.DesignationSetup.IsActive == true)
                             .Include(d => d.DesignationSetup)
                             .OrderBy(d => d.MenuPosition ?? int.MaxValue)
                             .ToListAsync(cancellationToken);

            var result = new List<OrganogramEmployeeInfo>();

            foreach (var d in designations)
            {
                bool added = false;

                // Primary employee
                var primaryEmployee = await _EmpJobDetaildsRepository
                    .Where(e => e.DesignationId == d.DesignationId
                                && e.DepartmentId == request.DepartmentId
                                && e.ServiceStatus == true
                                && (request.SectionId == 0 ? e.SectionId == null : e.SectionId == request.SectionId)
                                && e.Designation.DesignationSetup.IsActive == true
                                )
                    .Include(e => e.EmpBasicInfo)
                    .Select(e => new EmployeeInfo
                    {
                        EmpId = e.EmpBasicInfo.Id,
                        EmployeeName = e.EmpBasicInfo.FirstName + " " + e.EmpBasicInfo.LastName
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (primaryEmployee != null)
                {
                    result.Add(new OrganogramEmployeeInfo
                    {
                        Name = d.DesignationSetup?.Name,
                        EmployeeInfo = primaryEmployee
                    });
                    added = true;
                }

                // Secondary employee
                var secondaryEmployee = await _EmpOtherResponsibilityRepository
                    .Where(e => e.DesignationId == d.DesignationId
                                && e.DepartmentId == request.DepartmentId
                                && e.ServiceStatus == true)
                    .Include(e => e.EmpBasicInfo)
                    .Select(e => new EmployeeInfo
                    {
                        EmpId = e.EmpBasicInfo.Id,
                        EmployeeName = e.EmpBasicInfo.FirstName + " " + e.EmpBasicInfo.LastName +"("+e.ResponsibilityType.Name +")"
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (secondaryEmployee != null)
                {
                    result.Add(new OrganogramEmployeeInfo
                    {
                        Name = d.DesignationSetup?.Name,
                        EmployeeInfo = secondaryEmployee
                    });
                    added = true;
                }

                // No employee at all — add designation with null
                if (!added)
                {
                    result.Add(new OrganogramEmployeeInfo
                    {
                        Name = d.DesignationSetup?.Name,
                        EmployeeInfo = null
                    });
                }
            }

            return result;
        }




    }
}
