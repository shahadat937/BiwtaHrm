using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class NotificationReadBy : BaseDomainEntity
    {
        public int Id { get; set; }
        public int? NotificationId { get; set; }
        public int? EmpId { get; set; }


        public virtual Notification? Notification { get; set; }
        public virtual EmpBasicInfo? EmpBasicInfo { get; set; }
    }
}
