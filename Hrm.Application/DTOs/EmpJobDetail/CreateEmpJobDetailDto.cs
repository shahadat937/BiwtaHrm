using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpJobDetail
{
    public class CreateEmpJobDetailDto : IEmpJobDetailDto
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? OfficeId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public int? SectionId { get; set; }
        public int? PresentGradeId { get; set; }
        public int? PresentScaleId { get; set; }
        public int? BasicPay { get; set; }
        public DateOnly? JoiningDate { get; set; }
        public int? FirstGradeId { get; set; }
        public int? FirstScaleId { get; set; }
        public int? FirstDepartmentId { get; set; }
        public int? FirstSectionId { get; set; }
        public int? FirstDesignationId { get; set; }
        public DateOnly? PRLDate { get; set; }
        public DateOnly? RetirementDate { get; set; }
        public bool? ServiceStatus { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }
    }
}
