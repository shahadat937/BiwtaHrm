using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Reporting
{
    public class PRLReportDto
    {
        public int Id { get; set; }
        public string? IdCardNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FirstNameBangla { get; set; }
        public string? LastNameBangla { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public DateOnly? JoiningDate { get; set; }

        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public string? DesignationName { get; set; }

        public bool PrlGone { get; set; }
        public bool PrlWillGone { get; set; }
        public bool RetirmentGone { get; set; }
        public bool RetirmentWillGone { get; set; }
        public int TotalCount { get; set; }
    }
}
