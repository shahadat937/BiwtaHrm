using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class EmpChildInfo : BaseDomainEntity
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string? ChildName { get; set; }
        public string? ChildNameBangla { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? BirthRegNo { get; set; }
        public string? NID { get; set; }
        public int? OccupationId { get; set; }
        public int? GenderId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? ChildStatusId { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }

        public virtual EmpBasicInfo? EmpBasicInfo { get; set; }
        public virtual Occupation? Occupation { get; set; }
        public virtual Gender? Gender { get; set; }
        public virtual MaritalStatus? MaritalStatus { get; set; }
        public virtual ChildStatus? ChildStatus { get; set; }
    }
}
