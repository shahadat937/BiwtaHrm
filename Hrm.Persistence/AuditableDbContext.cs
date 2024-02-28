using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Persistence
{
    public abstract class AuditableDbContext:DbContext
    {
        public AuditableDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
