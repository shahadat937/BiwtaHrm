using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Domain.Common
{
    public abstract class BaseDomainEntity
    {
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
