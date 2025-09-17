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
        //public int? OfficeId { get; set; }
        //public int? DepartmentId { get; set; }
        //public int? DesignationSetupId { get; set; }
        //public int? SectionId { get; set; }
        //public int? DesignationId { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentNameBangla { get; set; }
        public string? SectionName { get; set; }
        public string? SectionNameBangla { get; set; }
        public string? DesignationName { get; set; }
        public string? DesignationNameBangla { get; set; }
        public string? WorkPlace { get; set; }
        public string? WorkPlaceBangla { get; set; }
        public DateOnly? JoiningDate { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string? OrderNo { get; set; }
        public DateOnly? OrderDate { get; set; }
        public string? Remark { get; set; }
        public bool? IsActive { get; set; }
    }
}
