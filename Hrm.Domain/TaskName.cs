using Hrm.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class TaskName : BaseDomainEntity
    {
        public int TaskNameId { get; set; }
        public string? TaskNames { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
