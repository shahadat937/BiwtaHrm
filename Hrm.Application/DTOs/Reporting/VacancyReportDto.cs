using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Reporting
{
    public class VacancyReportDto
    {
        public int? TotalPost { get; set; }
        public int? TotalInService { get; set; }
        public int? TotalVacant { get; set; }
        public List<VacancyDetailsDto> VacancyDetailsDto { get; set; }
    }
    public class VacancyDetailsDto
    {
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public string? DesignationName { get; set; }
        public int? TotalPost { get; set; }
        public int? TotalInService { get; set; }
        public int? TotalVacantPost { get; set; }

        public int? DepartmentId { get; set; }
        public int? SectionSequence { get; set; }
        public int? DesignationPosition { get; set; }
    }
}
