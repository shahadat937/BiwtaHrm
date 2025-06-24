using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormRecord
{
    public class FormRecordFilterDto
    {
        public string? Keywords {  get; set; }
        public int? RecordId { get; set; }
        public int? EmpId {  get; set; }
        public int? FormId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? ReporterId { get; set; }
        public int? CounterSignatureId { get; set; }
        public int? ReceiverId { get; set; }
        public bool? ReportingOfficerApproval { get; set; }
        public bool? CounterSignatoryApproval { get; set; }
        public bool? ReceiverApproval {  get; set; }
        public DateOnly? ReportFrom {  get; set; }
        public DateOnly? ReportTo { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string? SearchText { get; set; }
        public bool? IsActive { get; set; }
        public int? ReportingOfficerDepartmentId { get; set; }
        public int? ReportingOfficerSectionId { get; set; }
        public int? ReportingOfficerDesignationId { get; set; }
        public int? ReportingOfficerResponsibilityTypeId { get; set; }

        public int? CounterSignatoryDepartmentId { get; set; }
        public int? CounterSignatorySectionId { get; set; }
        public int? CounterSignatoryDesignationId { get; set; }
        public int? CounterSignatoryResponsibilityTypeId { get; set; }

        public int? ReporterDepartmentId { get; set; }
        public int? CounterSignatureDepatmentId { get; set; }

    }
}
