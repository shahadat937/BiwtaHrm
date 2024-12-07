using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class EmpOtherResponsibility : BaseDomainEntity
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string? OrderNo { get; set; }
        public DateOnly? OrderDate { get; set; }
        public int? ResponsibilityTypeId { get; set; }
        public int? OfficeId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? DesignationId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool ServiceStatus { get; set; }
        public string? Remark { get; set; }
        public bool? IsActive { get; set; }

        public virtual EmpBasicInfo? EmpBasicInfo { get; set; }
        public virtual ResponsibilityType? ResponsibilityType { get; set; }
        public virtual Office? Office { get; set; }
        public virtual Department? Department { get; set; }
        public virtual Section? Section { get; set; }
        public virtual Designation? Designation { get; set; }
    }

}
