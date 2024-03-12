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
            modelBuilder.Entity<Religion>(entity => {

                entity.HasKey(e => e.ReligionId)
                    .HasName("PK_ReligionId");

            });
            modelBuilder.Entity<ChildStatus>(entity => {

                entity.HasKey(e => e.ChildStatusId)
                    .HasName("PK_ChildStatusId");

            });
            modelBuilder.Entity<Division>(entity => {

                entity.HasKey(e => e.DivisionId)
                    .HasName("PK_DivisionId");

            });
            modelBuilder.Entity<Thana_Upazila>(entity => {

                entity.HasKey(e => e.Thana_UpazilaId)
                    .HasName("PK__Thana_Up__438130B40AC9D830");

            });
        }
        public virtual DbSet<AccountType> AccountType { get; set; } = null!;
        public virtual DbSet<BloodGroup> BloodGroup { get; set; } = null!;
        public virtual DbSet<MaritalStatus> MaritalStatus { get; set; } = null!;
        public virtual DbSet<EmployeeType> EmployeeType { get; set; } = null!;
        public virtual DbSet<Gender> Gender { get; set; } = null!;
        public virtual DbSet<Religion> Religion { get; set; } = null!;
        public virtual DbSet<TrainingType> TrainingType { get; set; } = null!;
        public virtual DbSet<ChildStatus> ChildStatus { get; set; } = null!;
        public virtual DbSet<Division> Division { get; set; } = null!;
        public virtual DbSet<Thana_Upazila> Thana_Upazila { get; set; } = null!;
        public virtual DbSet<PromotionType> PromotionType { get; set; } = null!;

    }
}
