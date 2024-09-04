using Hrm.Application;
using Hrm.Application.DTOs.Department;
using Hrm.Application.DTOs.Designation;
using Hrm.Application.DTOs.Office;
using Hrm.Application.DTOs.Organograms;
using Hrm.Domain;
using Hrm.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Organogram)]
    [ApiController]
    public class OrganogramController : ControllerBase
    {
        private readonly HrmDbContext _context;
        public OrganogramController(HrmDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-organogram")]
        public async Task<ActionResult<IEnumerable<OrganogramOfficeDto>>> GetOffices()
        {
            var offices = await _context.Office
                .Include(o => o.Departments)
                    .ThenInclude(d => d.SubDepartments)
                .Include(o => o.Departments)
                    .ThenInclude(d => d.Designations)
                .ToListAsync();

            var result = offices.Select(o => new OrganogramOfficeDto
            {
                OfficeId = o.OfficeId,
                OfficeName = o.OfficeNameBangla,
                Departments = o.Departments.Where(d => d.UpperDepartmentId == null).Select(d => MapDepartment(d)).ToList()
            });

            return Ok(result);
        }

        private OrganogramDepartmentDto MapDepartment(Department department)
        {
            return new OrganogramDepartmentDto
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentNameBangla,
                OfficeId = department.OfficeId,
                UpperDepartmentId = department.UpperDepartmentId,
                Designations = department.Designations.Select(de => new OrganogramDesignationDto
                {
                    DesignationId = de.DesignationId,
                    DesignationName = de.DesignationNameBangla,
                    OfficeId = de.OfficeId,
                    DepartmentId = de.DepartmentId
                }).ToList(),
                SubDepartments = department.SubDepartments.Select(sd => MapDepartment(sd)).ToList(),
            };
        }


        [HttpGet]
        [Route("get-organogramNamesOnly")]
        public async Task<ActionResult<IEnumerable<OrganogramOfficeNameDto>>> GetOfficeNames()
        {
            var offices = await _context.Office
                .Include(o => o.Departments)
                .ThenInclude(d => d.SubDepartments)
                .Include(o => o.Departments)
                    .ThenInclude(d => d.Designations)
                .Include(o => o.Designations) // Include direct designations
                    .ThenInclude(d => d.EmpJobDetail)
                    .ThenInclude(ejd => ejd.EmpBasicInfo)
                .Include(o => o.Sections) // Include Sections under Office
                    .ThenInclude(s => s.SubSections) // Include SubSections under Sections
                .Include(o => o.Sections)
                    .ThenInclude(s => s.Designations) // Include Designations under Sections
                .ToListAsync();

            var result = offices.Select(o => new OrganogramOfficeNameDto
            {
                Name = o.OfficeName,
                Departments = o.Departments
                    .Where(d => d.UpperDepartmentId == null)
                    .Select(d => MapDepartmentName(d))
                    .ToList(),
                Sections = o.Sections
                    .Where(s => s.DepartmentId == null && s.UpperSectionId == null)
                    .Select(s => MapSectionName(s))
                    .ToList(),
                DirectDesignations = o.Designations
                    .Where(d => d.DepartmentId == null)
                    .Select(d => new OrganogramDesignationNameDto
                    {
                        Name = d.DesignationName,
                        EmployeeName = d.EmpJobDetail?.FirstOrDefault()?.EmpBasicInfo != null ? $"{d.EmpJobDetail.FirstOrDefault().EmpBasicInfo.FirstName} {d.EmpJobDetail.FirstOrDefault().EmpBasicInfo.LastName}" : null
                    })
                    .ToList()
            }).ToList();

            return Ok(result);
        }

        private OrganogramDepartmentNameDto MapDepartmentName(Department department)
        {
            var departmentNameDto = new OrganogramDepartmentNameDto
            {
                Name = department.DepartmentName,
                Designations = department.Designations.Select(de => new OrganogramDesignationNameDto
                {
                    Name = de.DesignationName,
                    EmployeeName = de.EmpJobDetail?.FirstOrDefault()?.EmpBasicInfo != null ? $"{de.EmpJobDetail.FirstOrDefault().EmpBasicInfo.FirstName} {de.EmpJobDetail.FirstOrDefault().EmpBasicInfo.LastName}" : null
                }).ToList(),
                Sections = department.Section != null ? department.Section
                    .Where(s => s.UpperSectionId == null)
                    .Select(s => MapSectionName(s)).ToList() : new List<OrganogramSectionNameDto>(),
                SubDepartments = department.SubDepartments?.Select(sd => MapDepartmentName(sd)).ToList() ?? new List<OrganogramDepartmentNameDto>()
            };

            return departmentNameDto;
        }
        private OrganogramSectionNameDto MapSectionName(Domain.Section section)
        {
            var sectionNameDto = new OrganogramSectionNameDto
            {
                Name = section.SectionName,
                Designations = section.Designations.Select(se => new OrganogramDesignationNameDto
                {
                    Name = se.DesignationName,
                    EmployeeName = se.EmpJobDetail?.FirstOrDefault()?.EmpBasicInfo != null ? $"{se.EmpJobDetail.FirstOrDefault().EmpBasicInfo.FirstName} {se.EmpJobDetail.FirstOrDefault().EmpBasicInfo.LastName}" : null
                }).ToList(),
                SubSections = section.SubSections.Select(ss => MapSectionName(ss)).ToList()
            };

            return sectionNameDto;
        }

    }
}