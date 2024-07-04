using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class AttendanceType: BaseDomainEntity
    {
        public int AttendanceTypeId { get; set; }
        public string AttendanceTypeName {  get; set; }
        public bool IsActive { get; set; }

        public int? MenuPosition { get; set; }
        public string? Remark { get; set; }

        public ICollection<Attendance> Attendances {  get; } = new List<Attendance>();
    }
}
