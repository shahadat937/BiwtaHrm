using Hrm.Application;
using Hrm.Application.DTOs.Department;
using Hrm.Application.DTOs.Designation;
using Hrm.Application.DTOs.Office;
using Hrm.Application.DTOs.Organograms;
using Hrm.Application.Features.Organogram.Requests.Queries;
using Hrm.Domain;
using Hrm.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Organogram)]
    [ApiController]
    [Authorize]
    public class OrganogramController : ControllerBase
    {
        private readonly HrmDbContext _context;
        private readonly IMediator _mediator;
        public OrganogramController(HrmDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }


        //[HttpGet]
        //[Route("get-organogramNamesOnly")]
        //public async Task<ActionResult<IEnumerable<OrganogramOfficeNameDto>>> GetOfficeNames()
        //{
        //    var offices = await _context.Office
        //        .Include(o => o.Departments)
        //        .ThenInclude(d => d.SubDepartments)
        //        .Include(o => o.Departments)
        //            .ThenInclude(d => d.Designations)
        //        .Include(o => o.Designations) // Include direct designations
        //            .ThenInclude(d => d.EmpJobDetail)
        //            .ThenInclude(ejd => ejd.EmpBasicInfo)
        //        .Include(o => o.Sections) // Include Sections under Office
        //            .ThenInclude(s => s.SubSections) // Include SubSections under Sections
        //        .Include(o => o.Sections)
        //            .ThenInclude(s => s.Designations) // Include Designations under Sections
        //        .ToListAsync();

        //    var result = offices.Select(o => new OrganogramOfficeNameDto
        //    {
        //        Name = o.OfficeName,
        //        Departments = o.Departments
        //            .Where(d => d.UpperDepartmentId == null)
        //            .Select(d => MapDepartmentName(d))
        //            .ToList(),
        //        Sections = o.Sections
        //            .Where(s => s.DepartmentId == null && s.UpperSectionId == null)
        //            .Select(s => MapSectionName(s))
        //            .ToList(),
        //        DirectDesignations = o.Designations
        //            .Where(d => d.DepartmentId == null)
        //            .Select(d => new OrganogramDesignationNameDto
        //            {
        //                Name = d.DesignationName,
        //                EmployeeName = d.EmpJobDetail?.FirstOrDefault()?.EmpBasicInfo != null ? $"{d.EmpJobDetail.FirstOrDefault().EmpBasicInfo.FirstName} {d.EmpJobDetail.FirstOrDefault().EmpBasicInfo.LastName}" : null
        //            })
        //            .ToList()
        //    }).ToList();

        //    return Ok(result);
        //}

        //private OrganogramDepartmentNameDto MapDepartmentName(Department department)
        //{
        //    var departmentNameDto = new OrganogramDepartmentNameDto
        //    {
        //        Name = department.DepartmentName,
        //        Designations = department.Designations.Select(de => new OrganogramDesignationNameDto
        //        {
        //            Name = de.DesignationName,
        //            EmployeeName = de.EmpJobDetail?.FirstOrDefault()?.EmpBasicInfo != null ? $"{de.EmpJobDetail.FirstOrDefault().EmpBasicInfo.FirstName} {de.EmpJobDetail.FirstOrDefault().EmpBasicInfo.LastName}" : null
        //        }).ToList(),
        //        Sections = department.Section != null ? department.Section
        //            .Where(s => s.UpperSectionId == null)
        //            .Select(s => MapSectionName(s)).ToList() : new List<OrganogramSectionNameDto>(),
        //        SubDepartments = department.SubDepartments?.Select(sd => MapDepartmentName(sd)).ToList() ?? new List<OrganogramDepartmentNameDto>()
        //    };

        //    return departmentNameDto;
        //}
        //private OrganogramSectionNameDto MapSectionName(Domain.Section section)
        //{
        //    var sectionNameDto = new OrganogramSectionNameDto
        //    {
        //        Name = section.SectionName,
        //        Designations = section.Designations.Select(se => new OrganogramDesignationNameDto
        //        {
        //            Name = se.DesignationName,
        //            EmployeeName = se.EmpJobDetail?.FirstOrDefault()?.EmpBasicInfo != null ? $"{se.EmpJobDetail.FirstOrDefault().EmpBasicInfo.FirstName} {se.EmpJobDetail.FirstOrDefault().EmpBasicInfo.LastName}" : null
        //        }).ToList(),
        //        SubSections = section.SubSections.Select(ss => MapSectionName(ss)).ToList()
        //    };

        //    return sectionNameDto;
        //}

        [HttpGet]
        [Route("get-organogramNamesOnly")]
        public async Task<ActionResult<IEnumerable<OrganogramDepartmentNameDto>>> GetDepartmentNames()
        {
            var departments = await _context.Department
                .Include(d => d.SubDepartments) // Include SubDepartments under Departments
                .Include(d => d.Designations) // Include Designations under Departments
                    .ThenInclude(de => de.DesignationSetup) // Include DesignationSetup for Designations
                .Include(d => d.Designations)
                    .ThenInclude(de => de.EmpJobDetail)
                    .ThenInclude(ejd => ejd.EmpBasicInfo)
                .Include(d => d.Designations) // Include EmpOtherResponsibility
                    .ThenInclude(de => de.EmpOtherResponsibility)
                    .ThenInclude(eor => eor.EmpBasicInfo)
                .Include(d => d.Section) // Include Sections under Departments
                    .ThenInclude(s => s.SubSections) // Include SubSections under Sections
                .Include(d => d.Section)
                    .ThenInclude(s => s.Designations) // Include Designations under Sections
                        .ThenInclude(de => de.DesignationSetup) // Include DesignationSetup for Section Designations
                .ToListAsync();

            var result = departments
                .Where(d => d.UpperDepartmentId == null) // Only top-level departments
                .Select(d => MapDepartmentName(d))
                .ToList();

            return Ok(result);
        }

        private OrganogramDepartmentNameDto MapDepartmentName(Department department)
        {
            var departmentNameDto = new OrganogramDepartmentNameDto
            {
                Name = department.DepartmentName,
                Designations = department.Designations
                    .Where(x => x.SectionId == null)
                    .OrderBy(x => x.MenuPosition)
                    .Select(de => new OrganogramDesignationNameDto
                {
                    Name = de.DesignationSetup.Name,
                    EmployeeInfo = GetEmployeeInformation(de)
                    }).ToList(),
                Sections = department.Section != null
                    ? department.Section
                        .Where(s => s.UpperSectionId == null)
                        .OrderBy(s => s.Sequence)
                        .Select(s => MapSectionName(s))
                        .ToList()
                    : new List<OrganogramSectionNameDto>(),
                SubDepartments = department.SubDepartments?.Select(sd => MapDepartmentName(sd)).ToList()
                    ?? new List<OrganogramDepartmentNameDto>()
            };

            return departmentNameDto;
        }

        private OrganogramSectionNameDto MapSectionName(Domain.Section section)
        {
            var sectionNameDto = new OrganogramSectionNameDto
            {
                Name = section.SectionName,
                Designations = section.Designations
                    .OrderBy(x => x.MenuPosition)
                    .Select(se => new OrganogramDesignationNameDto
                    {
                        Name = se.DesignationSetup.Name,
                        EmployeeInfo = GetEmployeeInformation(se)
                    }).ToList(),
                SubSections = section.SubSections
                    .OrderBy(ss => ss.Sequence)
                    .Select(ss => MapSectionName(ss)).ToList()
            };

            return sectionNameDto;
        }
        private OrganogramEmployeeInfoDto GetEmployeeInformation(Designation designation)
        {
            var empJobDetail = designation.EmpJobDetail?.FirstOrDefault(x => x.ServiceStatus == true)?.EmpBasicInfo;
            if (empJobDetail != null)
            {
                var empInfo = new OrganogramEmployeeInfoDto
                {
                    EmpId = empJobDetail.Id,
                    EmployeeName = $"{empJobDetail.FirstName} {empJobDetail.LastName}"
                };

                return empInfo;
            }

            var empOtherResponsibility = designation.EmpOtherResponsibility?.FirstOrDefault(x => x.ServiceStatus == true);
            if (empOtherResponsibility != null)
            {
                var empBasicInfo = empOtherResponsibility.EmpBasicInfo;
                var responsibilityTypeName = _context.ResponsibilityType.FirstOrDefault(x => x.Id == empOtherResponsibility.ResponsibilityTypeId).Name;

                if (empBasicInfo != null)
                {
                    var empInfo = new OrganogramEmployeeInfoDto
                    {
                        EmpId = empBasicInfo.Id,
                        EmployeeName = responsibilityTypeName != null
                        ? $"{empBasicInfo.FirstName} {empBasicInfo.LastName} ({responsibilityTypeName})"
                        : $"{empBasicInfo.FirstName} {empBasicInfo.LastName}"
                    };

                    return empInfo;
                }
            }

            return null;
        }


        [HttpGet]
        [Route("get-topLavelDept")]
        public async Task<ActionResult<List<DepartmentDto>>> GetTopLavelDept(int departmentId)
        {
            var result = await _mediator.Send(new GetTopLavelDepartmentsRequest { DepartmentId = departmentId });
            return result;

        }

        [HttpGet]
        [Route("get-deparmentDetails")]
        public async Task<ActionResult<IEnumerable<OrganogramDepartmentNameDto>>> GetDeparmentDetails(int id)
        {
            var departments = await _context.Department
                .Include(d => d.SubDepartments) // Include SubDepartments under Departments
                .Include(d => d.Designations) // Include Designations under Departments
                    .ThenInclude(de => de.DesignationSetup) // Include DesignationSetup for Designations
                .Include(d => d.Designations)
                    .ThenInclude(de => de.EmpJobDetail)
                    .ThenInclude(ejd => ejd.EmpBasicInfo)
                .Include(d => d.Designations) // Include EmpOtherResponsibility
                    .ThenInclude(de => de.EmpOtherResponsibility)
                    .ThenInclude(eor => eor.EmpBasicInfo)
                .Include(d => d.Section) // Include Sections under Departments
                    .ThenInclude(s => s.SubSections) // Include SubSections under Sections
                .Include(d => d.Section)
                    .ThenInclude(s => s.Designations) // Include Designations under Sections
                        .ThenInclude(de => de.DesignationSetup) // Include DesignationSetup for Section Designations
                .ToListAsync();

            var result = departments
                .Where(d => d.UpperDepartmentId == id) // Only top-level departments
                .Select(d => MapDepartment(d))
                .ToList();

            return Ok(result);
        } 


        private OrganogramDepartmentNameDto MapDepartment(Department department)
        {
            var departmentNameDto = new OrganogramDepartmentNameDto
            {
                Name = department.DepartmentName,
                Designations = department.Designations
                    .Where(x => x.SectionId == null)
                    .OrderBy(x => x.MenuPosition)
                    .Select(de => new OrganogramDesignationNameDto
                    {
                        Name = de.DesignationSetup.Name,
                        EmployeeInfo = GetEmployeeInformation(de)
                    }).ToList(),
                Sections = department.Section != null
                    ? department.Section
                        .Where(s => s.UpperSectionId == null)
                        .OrderBy(s => s.Sequence)
                        .Select(s => MapSectionName(s))
                        .ToList()
                    : new List<OrganogramSectionNameDto>(),
                SubDepartments = department.SubDepartments?.Select(sd => MapDepartmentName(sd)).ToList()
                    ?? new List<OrganogramDepartmentNameDto>()
            };

            return departmentNameDto;
        }

        [HttpGet]
        [Route("get-countDeparmentDesignationSection")]
        public async Task<ActionResult<OrganogramDesignationDepartmentAndSectionCount>>  CountDeparmentDesignationSection(int departmentId)
        {
           var result = await _mediator.Send( new GetCountOfDepartmentDesignationSectionRequest{ DepartmentId = departmentId });
           return result;
        }

        public async Task<ActionResult<OrganogramEmployeeInfoDto>>  GetEmployee(Designation designation)
        {
            var empJobDetail = designation.EmpJobDetail?.FirstOrDefault(x => x.ServiceStatus == true)?.EmpBasicInfo;
            if (empJobDetail != null)
            {
                var empInfo = new OrganogramEmployeeInfoDto
                {
                    EmpId = empJobDetail.Id,
                    EmployeeName = $"{empJobDetail.FirstName} {empJobDetail.LastName}"
                };

                return empInfo;
            }

            var empOtherResponsibility = designation.EmpOtherResponsibility?.FirstOrDefault(x => x.ServiceStatus == true);
            if (empOtherResponsibility != null)
            {
                var empBasicInfo = empOtherResponsibility.EmpBasicInfo;
                var responsibilityTypeName = _context.ResponsibilityType.FirstOrDefault(x => x.Id == empOtherResponsibility.ResponsibilityTypeId).Name;

                if (empBasicInfo != null)
                {
                    var empInfo = new OrganogramEmployeeInfoDto
                    {
                        EmpId = empBasicInfo.Id,
                        EmployeeName = responsibilityTypeName != null
                        ? $"{empBasicInfo.FirstName} {empBasicInfo.LastName} ({responsibilityTypeName})"
                        : $"{empBasicInfo.FirstName} {empBasicInfo.LastName}"
                    };

                    return empInfo;
                }
            }

            return null;
        }






    }
}