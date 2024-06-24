using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class EmpPermanentAddress : BaseDomainEntity
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? CountryId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? UpazilaId { get; set; }
        public int? ThanaId { get; set; }
        public int? UnionId { get; set; }
        public int? WardId { get; set; }
        public int? ZipCode { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }

        //public virtual EmpBasicInfo? EmpBasicInfo { get; set; }
        //public virtual Country? Country { get; set; }
        //public virtual Division? Division { get; set; }
        //public virtual District? District { get; set; }
        //public virtual Upazila? Upazila { get; set; }
        //public virtual Thana? Thana { get; set; }
        //public virtual Union? Union { get; set; }
        //public virtual Ward? Ward { get; set; }
    }
}
