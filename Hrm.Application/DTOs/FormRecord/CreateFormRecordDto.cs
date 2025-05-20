using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormRecord
{
    public class CreateFormRecordDto: IFormRecordDto
    {
        public int RecordId { get; set; }
        public int FormId { get; set; }
        public int EmpId { get; set; }
        public int? ReportingOfficerId { get; set; }
        public int? CounterSignatoryId {  get; set; }
        public int? ReceiverId { get; set; }
        public DateTime? ReportFrom { get; set; }
        public DateTime? ReportTo { get; set; }
        public bool IsActive { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }

        public int? ReportingOfficerDepartmentId { get; set; }
        public int? ReportingOfficerSectionId { get; set; }
        public int? ReportingOfficerDesignationId { get; set; }
        public int? ReportingOfficerResponsibilityTypeId { get; set; }

        public int? CounterSignatoryDepartmentId { get; set; }
        public int? CounterSignatorySectionId { get; set; }
        public int? CounterSignatoryDesignationId { get; set; }
        public int? CounterSignatoryResponsibilityTypeId { get; set; }

        public int? ReceiverDepartmentId { get; set; }
        public int? ReceiverSectionId { get; set; }
        public int? ReceiverDesignationId { get; set; }
        public int? ReceiverResponsibilityTypeId { get; set; }


    }
}
