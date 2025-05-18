using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Form
{
    public class FormDataDto
    {
        public int FormId { get; set; }
        public int? RecordId { get; set; }
        public string FormName { get; set; }
        public string Description { get; set; }
        public DateTime? ReportFrom { get; set; }
        public DateTime? ReportTo { get; set; }
        public int? EmpId { get; set; }
        public int? ReportingOfficerId { get; set; }
        public int? CounterSignatoryId { get; set; }
        public int? ReceiverId { get; set; }

        public int? ReportingOfficerDepartmentId { get; set; }
        public int? ReportingOfficerSectionId { get; set; }
        public int? ReportingOfficerDesignationId { get; set; }
        public int? ReportingOfficerResponsibilityTypeId { get; set; }

        public int? CounterSignatoryDepartmentId { get; set; }
        public int? CounterSignatorySectionId { get; set; }
        public int? CounterSignatoryDesignationId { get; set; }
        public int? CounterSignatoryResponsibilityTypeId { get; set; }

        public List<FormSectionDto> Sections { get; set; } = new List<FormSectionDto>();
    }
}
