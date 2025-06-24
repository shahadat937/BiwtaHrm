using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class FormRecord : BaseDomainEntity
    {
        public int RecordId { get; set; }
        public int FormId { get; set; }
        public int EmpId { get; set; }
        public int? ReportingOfficerId { get; set; }
        public int? CounterSignatoryId { get; set; }
        public int? ReceiverId { get; set; }
        public bool ReportingOfficerApproval { get; set; }
        public bool CounterSignatoryApproval { get; set; }
        public bool ReceiverApproval { get; set; }
        public DateTime? ReportFrom {  get; set; }
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


        public Form Form { get; set; }
        public EmpBasicInfo Employee {  get; set; }
        public virtual ICollection<FieldRecord> FieldRecords { get; } = new List<FieldRecord>();
    }
}
