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
    public class HrmDbContext : AuditableDbContext
    {
        public HrmDbContext(DbContextOptions<HrmDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Office>()
            .HasMany(o => o.Departments)
            .WithOne(d => d.Office)
            .HasForeignKey(d => d.OfficeId);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.UpperDepartment)
                .WithMany(d => d.SubDepartments)
                .HasForeignKey(d => d.UpperDepartmentId);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Designations)
                .WithOne(d => d.Department)
                .HasForeignKey(d => d.DepartmentId);

            modelBuilder.Entity<Office>()
            .HasMany(o => o.Designations)
            .WithOne(d => d.Office)
            .HasForeignKey(d => d.OfficeId);


            modelBuilder.Entity<Designation>()
                .HasOne(d => d.Office)
                .WithMany(o => o.Designations)
                .HasForeignKey(d => d.OfficeId);

            modelBuilder.Entity<Designation>()
                .HasOne(d => d.Department)
                .WithMany(dp => dp.Designations)
                .HasForeignKey(d => d.DepartmentId);


            modelBuilder.Entity<OfficeBranch>(entity => {

                entity.HasKey(e => e.BranchId)
                    .HasName("PK_OfficeBranch");

            });
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
                    .HasName("PK__Division__20EFC6A8D5104B78");

            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                .HasName("PK__Country__10D1609FC64BEAA1");
            });

            modelBuilder.Entity<Thana>(entity => {
                entity.HasOne(d => d.Upazila)
                .WithMany(p => p.Thanas)
                .HasForeignKey(d => d.UpazilaId)
                .HasConstraintName("FK__Thana__UpazilaId__160F4887");

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
            //modelBuilder.Entity<OfficeBranch>(entity =>
            //{
            //    entity.HasKey(e => e.OfficeBranchId)
            //    .HasName("[PK__Branch__A1682FC588459E1D]");
            //});
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
            modelBuilder.Entity<SubGroup>(entity =>
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
            modelBuilder.Entity<WeekDay>(entity =>
            {
                entity.HasKey(e => e.WeekDayId)
                .HasName("[[PK_WeekDayId]]");
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

            modelBuilder.Entity<OfficeAddress>(entity =>
            {
                entity.HasKey(e => e.OfficeAddressId)
                .HasName("[[PK_OfficeAddress]]");

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
                .HasName("[PK_BankBranch]");
            });
            //modelBuilder.Entity<Division>(entity => {
            //    entity.HasKey(e => e.DivisionId)
            //        .HasName("PK__Division__20EFC6A8D5104B78");

            //});
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
            modelBuilder.Entity<Pool>(entity =>
            {
                entity.HasKey(e => e.PoolId)
                .HasName("[PK_PoolId]");
            });

            modelBuilder.Entity<SubDepartment>(entity =>
            {
                entity.HasKey(e => e.SubDepartmentId)
                .HasName("[SubDepartmentId]");
            });

            modelBuilder.Entity<ExamType>(entity =>
            {
                entity.HasKey(e => e.ExamTypeId)
                .HasName("[ExamTypeId]");
            });
            modelBuilder.Entity<Board>(entity =>
            {
                entity.HasKey(e => e.BoardId)
                .HasName("[BoardId]");
            });
            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(e => e.SectionId)
                .HasName("[SectionId]");
            });
            modelBuilder.Entity<PostingOrderInfo>(entity =>
            {
                entity.HasKey(e => e.PostingOrderInfoId)
                .HasName("[PostingOrderInfoId]");
            });
            modelBuilder.Entity<TransferApproveInfo>(entity =>
            {
                entity.HasKey(e => e.TransferApproveInfoId)
                .HasName("[TransferApproveInfoId]");
            });
            modelBuilder.Entity<DepReleaseInfo>(entity =>
            {
                entity.HasKey(e => e.DepReleaseInfoId)
                .HasName("[DepReleaseInfoId]");
            });
            modelBuilder.Entity<EmpTnsferPostingJoin>(entity =>
            {
                entity.HasKey(e => e.EmpTnsferPostingJoinId)
                .HasName("[EmpTnsferPostingJoinId]");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                .HasName("[PK_EmpId]");
            });

            /*modelBuilder.Entity<Workday>()
            .HasOne<Year>(d => d.year)
            .WithMany(sc => sc.Workday)
            .HasForeignKey(sc => sc.YearId);

            modelBuilder.Entity<Workday>()
            .HasOne<WeekDay>(d => d.weekDay)
            .WithMany(sc => sc.Workday)
            .HasForeignKey(sc => sc.WeekDayId);*/

            modelBuilder.Entity<Workday>()
            .HasKey(w => w.WorkdayId);

            modelBuilder.Entity<Workday>()
                .HasOne(w => w.year)
                .WithMany(y => y.Workday)
                .HasForeignKey(w => w.YearId);

            modelBuilder.Entity<Workday>()
                .HasOne(w => w.weekDay)
                .WithMany(wd => wd.Workday)
                .HasForeignKey(w => w.WeekDayId);

            // Optionally, configure Year and WeekDay entities as needed
            modelBuilder.Entity<Year>()
                .HasKey(y => y.YearId);


            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasMany( s => s.SiteVisits)
                .WithOne(e=>e.Employees)
                .HasForeignKey(e=>e.EmpId);
            });

            modelBuilder.Entity<Holidays>(entity =>
            {
                entity.HasKey(e => e.HolidayId)
                .HasName("[[PK_Holidays]]");
            });


            modelBuilder.Entity<DayType>(entity =>
            {
                entity.HasKey(k => k.DayTypeId)
                .HasName("[[PK_DayType]]");
            });

            modelBuilder.Entity<AttendanceType>(entity =>
            {
                entity.HasKey(k => k.AttendanceTypeId)
                .HasName("[[PK_AttendanceType]]");
            });

            modelBuilder.Entity<AttendanceStatus>(entity =>
            {
                entity.HasKey(s => s.AttendanceStatusId)
                .HasName("[[PK_AttendanceStatus]]");
            });

            modelBuilder.Entity<EmpPersonalInfo>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("[[PK_EmpPersonalInfo]]");

                entity.HasOne(e => e.EmpBasicInfo)
                    .WithMany(eb => eb.EmpPersonalInfo)
                    .HasForeignKey(e => e.EmpId);

                entity.HasOne(e => e.Gender)
                    .WithMany(eb => eb.EmpPersonalInfo)
                    .HasForeignKey(e => e.GenderId);

                entity.HasOne(e => e.MaritalStatus)
                    .WithMany(eb => eb.EmpPersonalInfo)
                    .HasForeignKey(e => e.MaritalStatusId);

                entity.HasOne(e => e.BloodGroup)
                    .WithMany(eb => eb.EmpPersonalInfo)
                    .HasForeignKey(e => e.BloodGroupId);

                entity.HasOne(e => e.Religion)
                    .WithMany(eb => eb.EmpPersonalInfo)
                    .HasForeignKey(e => e.ReligionId);

                entity.HasOne(e => e.HairColor)
                    .WithMany(eb => eb.EmpPersonalInfo)
                    .HasForeignKey(e => e.HairColorId);

                entity.HasOne(e => e.EyesColor)
                    .WithMany(eb => eb.EmpPersonalInfo)
                    .HasForeignKey(e => e.EyesColorId);

                entity.HasOne(e => e.Country)
                    .WithMany(eb => eb.EmpPersonalInfo)
                    .HasForeignKey(e => e.NationalityId);
            });


            modelBuilder.Entity<EmpBasicInfo>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("[[PK_EmpBasicInfo]]");

                entity.HasOne(e => e.EmployeeType)
                    .WithMany(eb => eb.EmpBasicInfo)
                    .HasForeignKey(e => e.EmployeeTypeId);
            });


            modelBuilder.Entity<EmpPresentAddress>(entity =>
            {
                entity.HasKey(e => e.Id);


                entity.HasOne(e => e.EmpBasicInfo)
                    .WithMany(eb => eb.EmpPresentAddress)
                    .HasForeignKey(e => e.EmpId);

                entity.HasOne(e => e.Country)
                    .WithMany(eb => eb.EmpPresentAddress)
                    .HasForeignKey(e => e.CountryId);

                entity.HasOne(e => e.Division)
                    .WithMany(eb => eb.EmpPresentAddress)
                    .HasForeignKey(e => e.DivisionId);

                entity.HasOne(e => e.District)
                    .WithMany(eb => eb.EmpPresentAddress)
                    .HasForeignKey(e => e.DistrictId);

                entity.HasOne(e => e.Upazila)
                    .WithMany(eb => eb.EmpPresentAddress)
                    .HasForeignKey(e => e.UpazilaId);

                entity.HasOne(e => e.Thana)
                    .WithMany(eb => eb.EmpPresentAddress)
                    .HasForeignKey(e => e.ThanaId);

                entity.HasOne(e => e.Union)
                    .WithMany(eb => eb.EmpPresentAddress)
                    .HasForeignKey(e => e.UnionId);

                entity.HasOne(e => e.Ward)
                    .WithMany(eb => eb.EmpPresentAddress)
                    .HasForeignKey(e => e.WardId);

            });

            modelBuilder.Entity<EmpPermanentAddress>(entity =>
            {
                entity.HasKey(e => e.Id);


                entity.HasOne(e => e.EmpBasicInfo)
                    .WithMany(eb => eb.EmpPermanentAddress)
                    .HasForeignKey(e => e.EmpId);

                entity.HasOne(e => e.Country)
                    .WithMany(eb => eb.EmpPermanentAddress)
                    .HasForeignKey(e => e.CountryId);

                entity.HasOne(e => e.Division)
                    .WithMany(eb => eb.EmpPermanentAddress)
                    .HasForeignKey(e => e.DivisionId);

                entity.HasOne(e => e.District)
                    .WithMany(eb => eb.EmpPermanentAddress)
                    .HasForeignKey(e => e.DistrictId);

                entity.HasOne(e => e.Upazila)
                    .WithMany(eb => eb.EmpPermanentAddress)
                    .HasForeignKey(e => e.UpazilaId);

                entity.HasOne(e => e.Thana)
                    .WithMany(eb => eb.EmpPermanentAddress)
                    .HasForeignKey(e => e.ThanaId);

                entity.HasOne(e => e.Union)
                    .WithMany(eb => eb.EmpPermanentAddress)
                    .HasForeignKey(e => e.UnionId);

                entity.HasOne(e => e.Ward)
                    .WithMany(eb => eb.EmpPermanentAddress)
                    .HasForeignKey(e => e.WardId);

            });

            modelBuilder.Entity<EmpJobDetail>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.EmpBasicInfo)
                    .WithMany(eb => eb.EmpJobDetail)
                    .HasForeignKey(e => e.EmpId);

                entity.HasOne(e => e.Office)
                    .WithMany(eb => eb.EmpJobDetail)
                    .HasForeignKey(e => e.OfficeId);

                entity.HasOne(e => e.Department)
                    .WithMany(eb => eb.EmpJobDetail)
                    .HasForeignKey(e => e.DepartmentId);

                entity.HasOne(e => e.Designation)
                    .WithMany(eb => eb.EmpJobDetail)
                    .HasForeignKey(e => e.DesignationId);

                entity.HasOne(e => e.Section)
                    .WithMany(eb => eb.EmpJobDetail)
                    .HasForeignKey(e => e.SectionId);

                entity.HasOne(e => e.PresentGrade)
                    .WithMany(eb => eb.EmpJobDetail)
                    .HasForeignKey(e => e.PresentGradeId);

                entity.HasOne(e => e.PresentScale)
                    .WithMany(eb => eb.EmpJobDetail)
                    .HasForeignKey(e => e.PresentScaleId);

            });

            modelBuilder.Entity<EmpSpouseInfo>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.EmpBasicInfo)
                    .WithMany(eb => eb.EmpSpouseInfo)
                    .HasForeignKey(e => e.EmpId);

                entity.HasOne(e => e.Occupation)
                    .WithMany(eb => eb.EmpSpouseInfo)
                    .HasForeignKey(e => e.OccupationId);
            });

            modelBuilder.Entity<EmpChildInfo>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.EmpBasicInfo)
                    .WithMany(eb => eb.EmpChildInfo)
                    .HasForeignKey(e => e.EmpId);

                entity.HasOne(e => e.Occupation)
                    .WithMany(eb => eb.EmpChildInfo)
                    .HasForeignKey(e => e.OccupationId);

                entity.HasOne(e => e.Gender)
                    .WithMany(eb => eb.EmpChildInfo)
                    .HasForeignKey(e => e.GenderId);

                entity.HasOne(e => e.MaritalStatus)
                    .WithMany(eb => eb.EmpChildInfo)
                    .HasForeignKey(e => e.MaritalStatusId);

                entity.HasOne(e => e.ChildStatus)
                    .WithMany(eb => eb.EmpChildInfo)
                    .HasForeignKey(e => e.ChildStatusId);
            });            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(at => at.AttendanceId)
                .HasName("[[PK_Attendance]]");
            });

            modelBuilder.Entity<AttendanceType>(entity =>
            {
                entity.HasMany(at => at.Attendances)
                .WithOne(ad => ad.AttendanceType)
                .HasForeignKey(ad => ad.AttendanceTypeId);
            });

            modelBuilder.Entity<EmpBasicInfo>(entity =>
            {
                entity.HasMany(em => em.Attendances)
                .WithOne(ad => ad.EmpBasicInfo)
                .HasForeignKey(ad => ad.EmpId);
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.HasMany(ofc => ofc.Attendances)
                .WithOne(ad => ad.Office)
                .HasForeignKey(ad => ad.OfficeId);
            });

            modelBuilder.Entity<OfficeBranch>(entity =>
            {
                entity.HasMany(ofb => ofb.Attendances)
                .WithOne(ad => ad.OfficeBranch)
                .HasForeignKey(ad => ad.OfficeBranchId);
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.HasMany(sf => sf.Attendances)
                .WithOne(ad => ad.Shift)
                .HasForeignKey(ad => ad.ShiftId);
            });

            modelBuilder.Entity<DayType>(entity =>
            {
                entity.HasMany(dt => dt.Attendances)
                .WithOne(ad => ad.DayType)
                .HasForeignKey(ad => ad.DayTypeId);
            });

            modelBuilder.Entity<AttendanceStatus>(entity =>
            {
                entity.HasMany(ats => ats.Attendances)
                .WithOne(ad => ad.AttendanceStatus)
                .HasForeignKey(ad => ad.AttendanceStatusId);
            });

            modelBuilder.Entity<EmpEducationInfo>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.EmpBasicInfo)
                    .WithMany(eb => eb.EmpEducationInfo)
                    .HasForeignKey(e => e.EmpId);

                entity.HasOne(e => e.ExamType)
                    .WithMany(eb => eb.EmpEducationInfo)
                    .HasForeignKey(e => e.ExamTypeId);

                entity.HasOne(e => e.Board)
                    .WithMany(eb => eb.EmpEducationInfo)
                    .HasForeignKey(e => e.BoardId);

                entity.HasOne(e => e.SubGroup)
                    .WithMany(eb => eb.EmpEducationInfo)
                    .HasForeignKey(e => e.SubGroupId);
            });

            modelBuilder.Entity<EmpPsiTrainingInfo>(entity =>
            {
                entity.HasKey(e => e.Id);


                entity.HasOne(e => e.EmpBasicInfo)
                    .WithMany(eb => eb.EmpPsiTrainingInfo)
                    .HasForeignKey(e => e.EmpId);

                entity.HasOne(e => e.TrainingName)
                    .WithMany(eb => eb.EmpPsiTrainingInfo)
                    .HasForeignKey(e => e.TrainingNameId);
            });

            modelBuilder.Entity<EmpBankInfo>(entity =>
            {
                entity.HasKey(e => e.Id);


                entity.HasOne(e => e.EmpBasicInfo)
                    .WithMany(eb => eb.EmpBankInfo)
                    .HasForeignKey(e => e.EmpId);

                entity.HasOne(e => e.AccountType)
                    .WithMany(eb => eb.EmpBankInfo)
                    .HasForeignKey(e => e.AccountTypeId);

                entity.HasOne(e => e.Bank)
                    .WithMany(eb => eb.EmpBankInfo)
                    .HasForeignKey(e => e.BankId);

                entity.HasOne(e => e.BankBranch)
                    .WithMany(eb => eb.EmpBankInfo)
                    .HasForeignKey(e => e.BranchId);
            });

            modelBuilder.Entity<EmpLanguageInfo>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.EmpBasicInfo)
                    .WithMany(eb => eb.EmpLanguageInfo)
                    .HasForeignKey(e => e.EmpId);

                entity.HasOne(e => e.Language)
                    .WithMany(eb => eb.EmpLanguageInfo)
                    .HasForeignKey(e => e.LanguageId);

                entity.HasOne(e => e.Competence)
                    .WithMany(eb => eb.EmpLanguageInfo)
                    .HasForeignKey(e => e.CompetenceId);
            });


            modelBuilder.Entity<EmpForeignTourInfo>(entity =>
            {
                entity.HasKey(e => e.Id);


                entity.HasOne(e => e.EmpBasicInfo)
                    .WithMany(eb => eb.EmpForeignTourInfo)
                    .HasForeignKey(e => e.EmpId);

                entity.HasOne(e => e.Country)
                    .WithMany(eb => eb.EmpForeignTourInfo)
                    .HasForeignKey(e => e.CountryId);
            });

            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<UserRole> UserRole { get; set; } = null!;

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
        public virtual DbSet<Union> Union { get; set; } = null!;
        public virtual DbSet<District> District { get; set; } = null!;
        public virtual DbSet<Result> Result { get; set; } = null!;
        public virtual DbSet<Ward> Ward { get; set; } = null!;
        public virtual DbSet<OfficeBranch> OfficeBranch { get; set; } = null!;
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
        public virtual DbSet<SubGroup> SubGroup { get; set; } = null!;
        public virtual DbSet<Punishment> Punishment { get; set; } = null!;
        public virtual DbSet<Reward> Reward { get; set; } = null!;
        public virtual DbSet<HolidayType> HolidayType { get; set; } = null!;
        public virtual DbSet<WeekDay> WeekDay { get; set; } = null!;
        public virtual DbSet<OverallEVPromotion> OverallEVPromotion { get; set; } = null!;
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
        public virtual DbSet<OfficeAddress> OfficeAddress { get; set; } = null!;
        public virtual DbSet<Competence> Competence { get; set; } = null!;
        public virtual DbSet<Language> Language { get; set; } = null!;
        public virtual DbSet<BankAccountType> BankAccountType { get; set; } = null!;
        public virtual DbSet<BankBranch> BankBranch { get; set; } = null!;
        public virtual DbSet<Bank> Bank { get; set; } = null!;
        public virtual DbSet<Institute> Institute { get; set; } = null!;
        public virtual DbSet<EyesColor> EyesColor { get; set; } = null!;
        public virtual DbSet<Pool> Pool { get; set; } = null!;
        public virtual DbSet<SubDepartment> SubDepartment { get; set; } = null!;
        public virtual DbSet<ExamType> ExamType { get; set; } = null!;
        public virtual DbSet<Board> Board { get; set; } = null!;
        public virtual DbSet<ReleaseType> ReleaseType { get; set; } = null!;

        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; } = null!;
        public virtual DbSet<Section> Section { get; set; } = null!;
        public virtual DbSet<PostingOrderInfo> PostingOrderInfo { get; set; } = null!;
        public virtual DbSet<TransferApproveInfo> TransferApproveInfo { get; set; } = null!;
        public virtual DbSet<DepReleaseInfo> DepReleaseInfo { get; set; } = null!;
        public virtual DbSet<EmpTnsferPostingJoin> EmpTnsferPostingJoin { get; set; } = null!;

        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; } = null!;
        public virtual DbSet<Year> Year { get; set; } = null!;
        public virtual DbSet<Employees> Employees { get; set; } = null!;


        //*****************Employees Information Table**************
        public virtual DbSet<EmpBasicInfo> EmpBasicInfo { get; set; } = null!;
        public virtual DbSet<EmpPersonalInfo> EmpPersonalInfo { get; set; } = null!;
        public virtual DbSet<EmpPresentAddress> EmpPresentAddress { get; set; } = null!;
        public virtual DbSet<EmpPermanentAddress> EmpPermanentAddress { get; set; } = null!;
        public virtual DbSet<EmpJobDetail> EmpJobDetail { get; set; } = null!;
        public virtual DbSet<EmpSpouseInfo> EmpSpouseInfo { get; set; } = null!;
        public virtual DbSet<EmpChildInfo> EmpChildInfo { get; set; } = null!;
        public virtual DbSet<EmpEducationInfo> EmpEducationInfo { get; set; } = null!;
        public virtual DbSet<EmpPsiTrainingInfo> EmpPsiTrainingInfo { get; set; } = null!;
        public virtual DbSet<EmpBankInfo> EmpBankInfo { get; set; } = null!;
        public virtual DbSet<EmpLanguageInfo> EmpLanguageInfo { get; set; } = null!;
        public virtual DbSet<EmpForeignTourInfo> EmpForeignTourInfo { get; set; } = null!;
        public virtual DbSet<EmpPhotoSign> EmpPhotoSign { get; set; } = null!;
        public virtual DbSet<EmpNomineeInfo> EmpNomineeInfo { get; set; } = null!;
        public virtual DbSet<EmpTrainingInfo> EmpTrainingInfo { get; set; } = null!;

        public virtual DbSet<EmpTransferPosting> EmpTransferPosting { get; set; } = null!;


        public virtual DbSet<SiteVisit> SiteVisit { get; set; } = null!;
        public virtual DbSet<Workday> Workday { get; set; } = null!;
        public virtual DbSet<Holidays> Holidays { get; set; } = null!;
        public virtual DbSet<DayType> DayType { get; set; } = null!;
        public virtual DbSet<AttendanceType> AttendanceType { get; set; } = null!;
        public virtual DbSet<AttendanceStatus> AttendanceStatus { get; set; } = null!;
        public virtual DbSet<Attendance> Attendance { get; set; } = null!;

    }
}