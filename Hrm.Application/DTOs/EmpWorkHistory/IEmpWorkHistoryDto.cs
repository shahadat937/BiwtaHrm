using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpWorkHistory
{
    public interface IEmpWorkHistoryDto
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? OfficeId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? DesignationId { get; set; }
        public int? DesignationSetupId { get; set; }
        public DateOnly? JoiningDate { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string? Remark { get; set; }
        public bool? IsActive { get; set; }
    }
}
