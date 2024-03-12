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
            modelBuilder.Entity<Thana>(entity => {

                entity.HasKey(e => e.ThanaId)
                    .HasName("PK__Thana__438130B46C389C43");

            });
            modelBuilder.Entity<Upazila>(entity => {

                entity.HasKey(e => e.UpazilaId)
                    .HasName("PK__Upazila__FE787458879EB561");

            });
            modelBuilder.Entity<Union>(entity =>
            {
                entity.HasKey(e => e.UnionId)
                .HasName("PK__Union__E3A71494908469B4");

            });
            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.DistrictId)
                .HasName("PK_DivisionId");
            });
            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasKey(e => e.ResultId)
                .HasName("PK__Result__97690208E73A8F36");
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
        public virtual DbSet<Thana> Thana { get; set; } = null!;
        public virtual DbSet<Upazila> Upazila { get; set; } = null!;
        public virtual DbSet<Union> Union { get; set; }= null!;
        public virtual DbSet<District> District { get; set; }=null!;
        public virtual DbSet<Result> Result { get; set; } = null!;

    }
}
