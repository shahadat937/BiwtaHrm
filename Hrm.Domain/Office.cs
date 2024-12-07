using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Office:BaseDomainEntity
    {
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string? OfficeNameBangla { get; set; }
        public int? CountryId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? UpazilaId { get; set; }
        public int? ThanaId { get; set; }
        public int? UnionId { get; set; }
        public int? WardId { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? OfficeWebsite { get; set; }
        public string? OfficeCode { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Department> Departments { get; set; }
        public ICollection<Designation> Designations { get; set; }
        public ICollection<Section> Sections { get; set; }
        public virtual ICollection<EmpJobDetail>? EmpJobDetail { get; set; }
        public ICollection<Attendance> Attendances { get; } = new List<Attendance>();
        public virtual ICollection<EmpTransferPosting>? CurrentEmpTransferPosting { get; set; }
        public virtual ICollection<EmpTransferPosting>? TransferEmpTransferPosting { get; set; }
        //public virtual ICollection<EmpWorkHistory>? EmpWorkHistory { get; set; }
        public virtual ICollection<EmpOtherResponsibility>? EmpOtherResponsibility { get; set; }
    }
}
