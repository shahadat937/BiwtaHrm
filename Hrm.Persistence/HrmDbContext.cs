using Hrm.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Persistence
{
    public class HrmDbContext:AuditableDbContext
    {
        public HrmDbContext(DbContextOptions<HrmDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<AccountType> AccountType { get; set; } = null!;
    }
}
