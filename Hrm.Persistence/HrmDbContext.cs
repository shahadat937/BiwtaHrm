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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        
        modelBuilder.Entity<BloodGroup>(entity => {

                entity.HasKey(e => e.BloodGroupId)
                    .HasName("PK_BloodGroupId");
                
            });
            modelBuilder.Entity<EmployeeType>(entity => {

                entity.HasKey(e => e.EmployeeTypeId)
                    .HasName("PK_EmployeeTypeId");

            });
            modelBuilder.Entity<Gender>(entity => {

                entity.HasKey(e => e.GenderId)
                    .HasName("PK_GenderId");

            });
        }
        public virtual DbSet<AccountType> AccountType { get; set; } = null!;
        public virtual DbSet<BloodGroup> BloodGroup { get; set; } = null!;
        public virtual DbSet<MaritalStatus> MaritalStatus { get; set; } = null!;
        public virtual DbSet<EmployeeType> EmployeeType { get; set; } = null!;
        public virtual DbSet<Gender> Gender { get; set; } = null!;

    }
}
