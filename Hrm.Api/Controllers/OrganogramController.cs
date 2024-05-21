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
                OfficeName = o.OfficeName,
                Departments = o.Departments.Where(d => d.UpperDepartmentId == null).Select(d => MapDepartment(d)).ToList()
            });

            return Ok(result);
        }

        private OrganogramDepartmentDto MapDepartment(Department department)
        {
            return new OrganogramDepartmentDto
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                OfficeId = department.OfficeId,
                UpperDepartmentId = department.UpperDepartmentId,
                SubDepartments = department.SubDepartments.Select(sd => MapDepartment(sd)).ToList(),
                Designations = department.Designations.Select(de => new OrganogramDesignationDto
                {
                    DesignationId = de.DesignationId,
                    DesignationName = de.DesignationName,
                    OfficeId = de.OfficeId,
                    DepartmentId = de.DepartmentId
                }).ToList()
            };
        }
    }
}
