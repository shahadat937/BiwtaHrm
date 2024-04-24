using Hrm.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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


            modelBuilder.Entity<BloodGroup>(entity =>
            {

                entity.HasKey(e => e.BloodGroupId)
                    .HasName("PK_BloodGroupId");

            });
            modelBuilder.Entity<EmployeeType>(entity =>
            {

                entity.HasKey(e => e.EmployeeTypeId)
                    .HasName("PK_EmployeeTypeId");

            });
            modelBuilder.Entity<Gender>(entity =>
            {

                entity.HasKey(e => e.GenderId)
                    .HasName("PK_GenderId");

            });
            modelBuilder.Entity<Religion>(entity =>
            {

                entity.HasKey(e => e.ReligionId)
                    .HasName("PK_ReligionId");

            });
            modelBuilder.Entity<ChildStatus>(entity =>
            {

                entity.HasKey(e => e.ChildStatusId)
                    .HasName("PK_ChildStatusId");

            });
            modelBuilder.Entity<Division>(entity =>
            {

                entity.HasOne(d => d.Country)
                  .WithMany(p => p.Divisions)
                  .HasForeignKey(d => d.CountryId)
                  .HasConstraintName("FK__Division__Countr__0D7A0286");
                entity.HasKey(e => e.DivisionId)
                    .HasName("PK_DivisionId");

            });
            modelBuilder.Entity<Thana>(entity =>
            {

                entity.HasKey(e => e.ThanaId)
                    .HasName("PK__Thana__438130B46C389C43");

            });
            modelBuilder.Entity<Upazila>(entity =>
            {

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
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.BranchId)
                .HasName("[PK__Branch__A1682FC588459E1D]");
            });
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentId)
                .HasName("[[PK__Departme__B2079BED028213CD]]");
            });
            modelBuilder.Entity<Designation>(entity =>
            {
                entity.HasKey(e => e.DesignationId)
                .HasName("[[PK__Designat__BABD60DE3D706100]]");
            });
            modelBuilder.Entity<Shift>(entity =>
            {
                entity.HasKey(e => e.ShiftId)
                .HasName("[[PK_ShiftId]]");
            });
            modelBuilder.Entity<Leave>(entity =>
            {
                entity.HasKey(e => e.LeaveId)
                .HasName("[[PK_LeaveId]]");
            });
            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.SubjectId)
                .HasName("[[PK__Subject__AC1BA3A81467B256]]");
            });
            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                .HasName("[[PK__Group__149AF36A7B245A3B]]");
            });
            modelBuilder.Entity<Punishment>(entity =>
            {
                entity.HasKey(e => e.PunishmentId)
                .HasName("[PK_PunishmentId]");
            });
            modelBuilder.Entity<Reward>(entity =>
            {
                entity.HasKey(e => e.RewardId)
                .HasName("[[PK_RewardId]]");
            });
            modelBuilder.Entity<HolidayType>(entity =>
            {
                entity.HasKey(e => e.HolidayTypeId)
                .HasName("[[PK_HolidayTypeId]]");
            });
            modelBuilder.Entity<Weekend>(entity =>
            {
                entity.HasKey(e => e.WeekendId)
                .HasName("[[PK_WeekendId]]");
            });
            modelBuilder.Entity<Scale>(entity =>
            {
                entity.HasKey(e => e.ScaleId)
                .HasName("[[PK_ScaleId]]");
            });
            modelBuilder.Entity<ScaleGradeView>(entity =>
            {
                entity.HasKey(e => e.ScaleId)
                .HasName("[[PK_ScaleId]]");
            });
            modelBuilder.Entity<Grade_cls_type_Vw>(entity =>
            {
                entity.HasKey(e => e.GradeId)
                .HasName("[[PK_GradeId]]");
            });

            modelBuilder.Entity<SubBranch>(entity =>
            {
                entity.HasKey(e => e.SubBranchId)
                .HasName("[[PK_SubBranchId]]");
            });


            modelBuilder.Entity<TrainingName>(entity =>
            {
                entity.HasKey(e => e.TrainingNameId)
                .HasName("[[PK_TrainingNameId]]");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.HasKey(e => e.OfficeId)
                .HasName("[[PK_Office]]");
            });

            modelBuilder.Entity<Competence>(entity =>
            {
                entity.HasKey(e => e.CompetenceId)
                .HasName("[[PK_Competence]]");
            });
            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.LanguageId)
                .HasName("[[PK_Language]]");
            });
            modelBuilder.Entity<BankAccountType>(entity =>
            {
                entity.HasKey(e => e.BankAccountTypeId)
                .HasName("[[PK_BankAccountType]]");
            });
            modelBuilder.Entity<BankBranch>(entity =>
            {
                entity.HasKey(e => e.BankBranchId)
                .HasName("[[PK_BankBranch]]");
            });
            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasKey(e => e.BankId)
                .HasName("[[PK_Bank]]");
            });
            modelBuilder.Entity<Institute>(entity =>
            {
                entity.HasKey(e => e.InstituteId)
                .HasName("[[PK_Institute]]");
            });


            modelBuilder.Entity<Relation>(entity =>
            {
                entity.HasKey(e => e.RelationId)
                .HasName("[[PK_RelationId]]");
            });
            modelBuilder.Entity<Occupation>(entity =>
            {
                entity.HasKey(e => e.OccupationId)
                .HasName("[[PK_OccupationId]]");
            });

            modelBuilder.Entity<OfficeAddress>(entity =>
            {
                entity.HasKey(e => e.OfficeAddressId)
                .HasName("[[PK_OfficeAddressId]]");
            });
            modelBuilder.Entity<HairColor>(entity =>
                {
                    entity.HasKey(e => e.HairColorId)
                    .HasName("[[PK_HairColorId]]");
                });
            modelBuilder.Entity<EyesColor>(entity =>
                {
                    entity.HasKey(e => e.EyesColorId)
                    .HasName("[[PK_EyesColorId]]");
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
        public virtual DbSet<PromotionType> PromotionType { get; set; } = null!;
        public virtual DbSet<Thana> Thana { get; set; } = null!;
        public virtual DbSet<Upazila> Upazila { get; set; } = null!;
        public virtual DbSet<Union> Union { get; set; }= null!;
        public virtual DbSet<District> District { get; set; }=null!;
        public virtual DbSet<Result> Result { get; set; } = null!;
        public virtual DbSet<Ward> Ward { get; set; } = null!;
        public virtual DbSet<Branch> Branch { get; set; } = null!;
        public virtual DbSet<Department> Department { get; set; } = null!;
        public virtual DbSet<SubBranch> SubBranch { get; set; } = null!;
        public virtual DbSet<Country> Country { get; set; } = null!;
        public virtual DbSet<Designation> Designation { get; set; } = null!;
        public virtual DbSet<Shift> Shift { get; set; } = null!;
        public virtual DbSet<Leave> Leave { get; set; } = null!;
        public virtual DbSet<Subject> Subject { get; set; } = null!;
        public virtual DbSet<GradeType> GradeType { get; set; } = null!;
        public virtual DbSet<GradeClass> GradeClass { get; set; } = null!;
        public virtual DbSet<Grade> Grade { get; set; } = null!;
        public virtual DbSet<Group> Group { get; set; } = null!;
        public virtual DbSet<Punishment> Punishment { get; set; } = null!;
        public virtual DbSet<Reward> Reward { get; set; }= null!;
        public virtual DbSet<HolidayType> HolidayType { get; set; } = null!;
        public virtual DbSet<Weekend> Weekend { get; set; } = null!;
        public virtual DbSet<Overall_EV_Promotion> Overall_EV_Promotion { get; set; } = null!;
        public virtual DbSet<Domain.Module> Module { get; set; } = null!;
        public virtual DbSet<Feature> Feature { get; set; } = null!;
        public virtual DbSet<Scale> Scale { get; set; } = null!;
        public virtual DbSet<ScaleGradeView> ScaleGradeView { get; set; } = null!;
        public virtual DbSet<Grade_cls_type_Vw> Grade_cls_type_Vw { get; set; } = null!;
        public virtual DbSet<Relation> Relation { get; set; } = null!;
        public virtual DbSet<Occupation> Occupation { get; set; } = null!;
        public virtual DbSet<HairColor> HairColor { get; set; } = null!;

        public virtual DbSet<TrainingName> TrainingName { get; set; } = null!;

        public virtual DbSet<Office> Office { get; set; } = null!;
        public virtual DbSet<Competence> Competence { get; set; } = null!;
        public virtual DbSet<Language> Language { get; set; } = null!;
        public virtual DbSet<BankAccountType> BankAccountType { get; set; } = null!;
        public virtual DbSet<BankBranch> BankBranch { get; set; } = null!;
        public virtual DbSet<Bank> Bank { get; set; } = null!;
        public virtual DbSet<Institute> Institute { get; set; } = null!;
        public virtual DbSet<OfficeAddress> OfficeAddress { get; set; } = null!;
        public virtual DbSet<EyesColor> EyesColor { get; set; } = null!;



    }
}
