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
                .ThenInclude(d => d.EmpJobDetail) // Include EmpJobDetail
                .ThenInclude(ejd => ejd.EmpBasicInfo) // Include EmpBasicInfo
                .ToListAsync();

            var result = offices.Select(o => new OrganogramOfficeNameDto
            {
                Name = o.OfficeName,
                Departments = o.Departments
                    .Where(d => d.UpperDepartmentId == null)
                    .Select(d => MapDepartmentName(d))
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
                SubDepartments = department.SubDepartments.Select(sd => MapDepartmentName(sd)).ToList()
            };

            return departmentNameDto;
        }

    }
}