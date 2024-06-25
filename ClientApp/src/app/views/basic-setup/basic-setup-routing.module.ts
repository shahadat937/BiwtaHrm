import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewAccountTypeComponent } from './accounttype/new-accounttype/new-accounttype.component';
import { BloodGroupComponent } from './blood-group/blood-group.component';
import { ScaleComponent } from './scale/scale.component';
import { DistrictComponent } from './district/district.component';
import { UpazilaComponent } from './upazila/upazila.component';
import { ThanaComponent } from './thana/thana.component';
import { ResultComponent } from './result/result.component';
import { SubjectComponent } from './subject/subject.component';
import { GroupComponent } from './group/group.component';
//import{GroupComponent} from './group/group.component'
import { BranchComponent } from './branch/branch.component';
import { UnionComponent } from './union/union.component';
import { WardComponent } from './ward/ward.component';
import { ShiftComponent } from './shift/shift.component';
import { TrainingComponent } from './training/training.component';
import { DepartmentComponent } from './department/department.component';
import { MaritalStatusComponent } from './marital-status/marital-status.component';
import { EmployeeTypeComponent } from './employee-type/employee-type.component';
import { GenderComponent } from './gender/gender.component';
import { ReligionComponent } from './religion/religion.component';
import { ChildStatusComponent } from './child-status/child-status.component';
import { Designation } from './model/Designation';
import { DesignationComponent } from './designation/designation.component';
import { PunishmentComponent } from './punishment/punishment.component';
import { PromotionTypeComponent } from './promotion-type/promotion-type.component';
import { GradeComponent } from './grade/grade.component';
import { CountryComponent } from './country/country.component';
import { GradeTypeComponent } from './grade-type/grade-type.component';
import { GradeClassComponent } from './grade-class/grade-class.component';
import { DivisionComponent } from './division/division.component';
import { OccupationComponent } from './occupation/occupation.component';
import { LeaveComponent } from './leave/leave.component';
import { OverallEVPromotionComponent } from './overall-ev-promotion/overall-ev-promotion.component';
import { HairColorComponent } from './hair-color/hair-color.component';
import { EyesColorComponent } from './eyes-color/eyes-color.component';
import { RelationComponent } from './relation/relation.component';
import { SubDepartmentComponent } from './sub-department/sub-department.component';
import { PoolComponent } from './pool/pool.component';
import { UserRoleComponent } from './user-role/user-role.component';
import { BankComponent } from './bank/bank.component';
import { BankBranchComponent } from './bank-branch/bank-branch.component';
import { BankAccountTypeComponent } from './bank-account-type/bank-account-type.component';
import { LanguageComponent } from './language/language.component';
import { TrainingNameComponent } from './training-name/training-name.component';
import { InstituteComponent } from './institute/institute.component';
import { OfficeComponent } from './office/office.component';
import { OfficeAddressComponent } from './office-address/office-address.component';
import { CompetenceComponent } from './competence/competence.component';
import { ExamTypeComponent } from './exam-type/exam-type.component';
import { BoardComponent } from './board/board.component';
import { group } from '@angular/animations';

import { SectionComponent } from './section/section.component';

import { SubBranchComponent } from './sub-branch/sub-branch.component';
import { YearSetupComponent } from './year-setup/year-setup.component';
import { HolidayTypeComponent } from './holiday-type/holiday-type.component';
import { OrganogramComponent } from './organogram/organogram.component';


const routes: Routes = [

  {
    path: '',
    data: {
      title: 'Personal Info. Setup',
    },
    children: [
      {
        path: 'blood-group',
        component: BloodGroupComponent,
        data: {
          title: 'Blood Group',
        },
      },
      {
        path: 'update-bloodgroup/:bloodGroupId',
        component: BloodGroupComponent,
        data: {
          title: 'Update Blood Group',
        },
      }, {
        path: 'marital-status',
        component: MaritalStatusComponent,
        data: {
          title: 'Marital Status',
        },
      },
      {
        path: 'update-marital-status/:maritalStatusId',
        component: MaritalStatusComponent,
        data: {
          title: 'Update Marital Status',
        },
      },
      {
        path: 'employee-type',
        component: EmployeeTypeComponent,
        data: {
          title: 'Employee Type',
        },
      },
      {
        path: 'update-employee-type/:employeeTypeId',
        component: EmployeeTypeComponent,
        data: {
          title: 'Update Employee Type',
        },
      },
      {
        path: 'gender',
        component: GenderComponent,
        data: {
          title: 'Gender',
        },
      },
      {
        path: 'update-gender/:genderId',
        component: GenderComponent,
        data: {
          title: 'Update Gender',
        },
      },
      {
        path: 'religion',
        component: ReligionComponent,
        data: {
          title: 'Religion',
        },
      },
      {
        path: 'update-religion/:religionId',
        component: ReligionComponent,
        data: {
          title: 'Update Religion',
        },
      },
      {
        path: 'child-status',
        component: ChildStatusComponent,
        data: {
          title: 'Child Status',
        },
      },
      {
        path: 'update-child-status/:childStatusId',
        component: ChildStatusComponent,
        data: {
          title: 'Update Child Status',
        },
      },
      {
        path: 'pool',
        component: PoolComponent,
        data: {
          title: 'Pool',
        },
      },
      {
        path: 'update-pool/:poolId',
        component: PoolComponent,
        data: {
          title: 'Update Pool',
        },
      },
      {
        path: 'occupation',
        component: OccupationComponent,
        data: {
          title: 'Occupation',
        },
      },
      {
        path: 'update-occupation/:occupationId',
        component: OccupationComponent,
        data: {
          title: 'Update Occupation',
        },
      },
      {
        path: 'shift',
        component: ShiftComponent,
        data: {
          title: 'Shift',
        },
      },
      {
        path: 'update-shift/:shiftId',
        component: ShiftComponent,
        data: {
          title: 'Update Shift',
        },
      },
      {
        path: 'promotionType',
        component: PromotionTypeComponent,
        data: {
          title: 'Promotion Type',
        },
      },
      {
        path: 'update-promotionType/:promotionTypeId',
        component: PromotionTypeComponent,
        data: {
          title: 'Update Promotion Type',
        },
      },
      {
        path: 'punishment',
        component: PunishmentComponent,
        data: {
          title: 'Punishment',
        },
      },
      {
        path: 'update-punishment/:punishmentId',
        component: PunishmentComponent,
        data: {
          title: 'Update Punishment',
        },
      },
      {
        path: 'scale',
        component: ScaleComponent,
        data: {
          title: 'Scale',
        },
      },
      {
        path: 'update-scale/:scaleId',
        component: ScaleComponent,
        data: {
          title: 'Update Scale',
        },
      },

      {
        path: 'grade',
        component: GradeComponent,
        data: {
          title: 'Grade',
        },
      },
      {
        path: 'update-grade/:gradeId',
        component: GradeComponent,
        data: {
          title: 'Update Grade',
        },
      },
      {
        path: 'grade-type',
        component: GradeTypeComponent,
        data: {
          title: 'Grade Type',
        },
      },
      {
        path: 'update-grade-type/:gradeTypeId',
        component: GradeTypeComponent,
        data: {
          title: 'Update Grade Type',
        },
      },
      {
        path: 'grade-class',
        component: GradeClassComponent,
        data: {
          title: 'Grade Class',
        },
      },
      {
        path: 'update-grade-class/:gradeClassId',
        component: GradeClassComponent,
        data: {
          title: 'Update Grade',
        },
      },
      {
        path: 'leave',
        component: LeaveComponent,
        data: {
          title: 'Leave Type',
        },
      },
      {
        path: 'update-leave/:leaveId',
        component: LeaveComponent,
        data: {
          title: 'Update Leave Type',
        },
      },
      {
        path: 'overall_EV_Promotion',
        component: OverallEVPromotionComponent,
        data: {
          title: 'Evolution & Promotion',
        },
      },
      {
        path: 'update-overall_EV_Promotion/:overallEVPromotionId',
        component: OverallEVPromotionComponent,
        data: {
          title: 'Update Evolution & Promotion',
        },
      },
      {
        path: 'hairColor',
        component: HairColorComponent,
        data: {
          title: 'Hair Color',
        },
      },
      {
        path: 'update-hairColor/:hairColorId',
        component: HairColorComponent,
        data: {
          title: 'Update Hair Color',
        },
      },
      {
        path: 'eyesColor',
        component: EyesColorComponent,
        data: {
          title: 'Eyes Color',
        },
      },
      {
        path: 'update-eyesColor/:eyesColorId',
        component: EyesColorComponent,
        data: {
          title: 'Update Eye Color',
        },
      },
      {
        path: 'relation',
        component: RelationComponent,
        data: {
          title: 'Relation',
        },
      },
      {
        path: 'update-relation/:relationId',
        component: RelationComponent,
        data: {
          title: 'Update Relation',
        },
      },
      {
        path: 'subDepartment',
        component: SubDepartmentComponent,
        data: {
          title: 'Sub Department',
        },
      },
      {
        path: 'update-subDepartment/:subDepartmentId',
        component: SubDepartmentComponent,
        data: {
          title: 'Update Sub Department',
        },
      },
      {
        path: 'userRole',
        component: UserRoleComponent,
        data: {
          title: 'User Role',
        },
      },
      {
        path: 'update-userRole/:userRoleId',
        component: UserRoleComponent,
        data: {
          title: 'Update User Role',
        },
      },
      {
        path: 'section',
        component: SectionComponent,
        data: {
          title: 'Section',
        },
      },
      {
        path: 'update-section/:sectionId',
        component: SectionComponent,
        data: {
          title: 'Update Section',
        },
      },
      {
        path: 'officeBranch',
        component: BranchComponent,
        data: {
          title: 'Branch',
        },
      },
      {
        path: 'update-officeBranch/:branchId',
        component: BranchComponent,
        data: {
          title: 'Update Branch',
        },
      },
      {
        path: 'subBranch',
        component: SubBranchComponent,
        data: {
          title: 'Sub Branch',
        },
      },
      {
        path: 'update-subBranch/:subBranchId',
        component: SubBranchComponent,
        data: {
          title: 'Update Sub Branch',
        },
      },
      {
        path: 'yearsetup',
        component: YearSetupComponent,
        data: {
          title: 'Year',
        },
      },
      {
        path: 'update-year/:yearId',
        component: YearSetupComponent,
        data: {
          title: 'Update Year',
        },
      },
      {
        path: 'holidaytype',
        component: HolidayTypeComponent,
        data: {
          title: 'Holiday Type',
        },
      },
      {
        path: 'update-holidaytype/:holidayTypeId',
        component: HolidayTypeComponent,
        data: {
          title: 'Update Holiday Type',
        },
      },

    ]
  },
  {
    path: '',
    data: {
      title: 'Address Setup',
    },
    children: [

      {
        path: 'country',
        component: CountryComponent,
        data: {
          title: 'Country',
        },
      },
      {
        path: 'update-country/:countryId',
        component: CountryComponent,
        data: {
          title: 'Update Country',
        },
      },
      {
        path: 'division',
        component: DivisionComponent,
        data: {
          title: 'Division',
        },
      },
      {
        path: 'update-division/:divisionId',
        component: DivisionComponent,
        data: {
          title: 'Update Division',
        },
      },
      {
        path: 'district',
        component: DistrictComponent,
        data: {
          title: 'District',
        },
      },
      {
        path: 'update-district/:districtId',
        component: DistrictComponent,
        data: {
          title: 'Update District',
        },
      },
      {
        path: 'upazila',
        component: UpazilaComponent,
        data: {
          title: 'Upazila',
        },
      },
      {
        path: 'update-upazila/:upazilaId',
        component: UpazilaComponent,
        data: {
          title: 'Update Upazila',
        },
      },
      {
        path: 'thana',
        component: ThanaComponent,
        data: {
          title: 'Thana',
        },
      },
      {
        path: 'update-thana/:thanaId',
        component: ThanaComponent,
        data: {
          title: 'Update Thana',
        },
      }, {
        path: 'union',
        component: UnionComponent,
        data: {
          title: 'Union',
        },
      },
      {
        path: 'update-union/:unionId',
        component: UnionComponent,
        data: {
          title: 'Update Union',
        },
      },
      {
        path: 'ward',
        component: WardComponent,
        data: {
          title: 'Ward',
        },
      },
      {
        path: 'update-ward/:wardId',
        component: WardComponent,
        data: {
          title: 'Update Ward',
        },
      },

    ]
  },
  {
    path: '',
    data: {
      title: 'Education Setup',
    },
    children: [
      {
        path: 'examType',
        component: ExamTypeComponent,
        data: {
          title: 'Exam Type',
        },
      },
      {
        path: 'update-examType/:examTypeId',
        component: ExamTypeComponent,
        data: {
          title: 'Update Exam Type',
        },
      },
      {
        path: 'group',
        component: GroupComponent,
        data: {
          title: 'Group',
        },
      },
      {
        path: 'update-group/:groupId',
        component: GroupComponent,
        data: {
          title: 'Update Group',
        },
      },
      {
        path: 'board',
        component: BoardComponent,
        data: {
          title: 'Board',
        },
      },
      {
        path: 'update-board/:boardId',
        component: BoardComponent,
        data: {
          title: 'Update Board',
        },
      },
      {
        path: 'group',
        component: GroupComponent,
        data: {
          title: 'Group',
        },
      },
      {
        path: 'update-group/:groupId',
        component: GroupComponent,
        data: {
          title: 'Update Group',
        },
      },
      {
        path: 'result',
        component: ResultComponent,
        data: {
          title: 'Result',
        },
      },
      {
        path: 'update-result/:resultId',
        component: ResultComponent,
        data: {
          title: 'Update Result',
        },
      },
      {
        path: 'subject',
        component: SubjectComponent,
        data: {
          title: 'Subject',
        },
      },
      {
        path: 'update-subject/:subjectId',
        component: SubjectComponent,
        data: {
          title: 'Update Subject',
        },
      },

    ]
  },
  {
    path: '',
    data: {
      title: 'Training Setup',
    },
    children: [
      {
        path: 'training',
        component: TrainingComponent,
        data: {
          title: 'Training',
        },
      },
      {
        path: 'update-trainingType/:trainingTypeId',
        component: TrainingComponent,
        data: {
          title: 'Update Training',
        },
      },

      {
        path: 'trainingName',
        component: TrainingNameComponent,
        data: {
          title: 'Training Name',
        },
      },
      {
        path: 'update-trainingName/:trainingNameId',
        component: TrainingNameComponent,
        data: {
          title: 'Update Training Name',
        },
      },
      {
        path: 'institute',
        component: InstituteComponent,
        data: {
          title: 'Institute',
        },
      },
      {
        path: 'update-institute/:instituteId',
        component: InstituteComponent,
        data: {
          title: 'Update Institute',
        },
      },

    ]
  },
  {
    path: '',
    data: {
      title: 'Bank Info. Setup',
    },
    children: [
      {
        path: 'bank',
        component: BankComponent,
        data: {
          title: 'Bank',
        },
      },
      {
        path: 'update-bank/:bankId',
        component: BankComponent,
        data: {
          title: 'Update Bank',
        },
      },
      {
        path: 'bankBranch',
        component: BankBranchComponent,
        data: {
          title: 'Bank Branch',
        },
      },
      {
        path: 'update-bankBranch/:bankBranchId',
        component: BankBranchComponent,
        data: {
          title: 'Update Bank Branch',
        },
      },
      {
        path: 'bankAccountType',
        component: BankAccountTypeComponent,
        data: {
          title: 'Bank Account Type',
        },
      },
      {
        path: 'update-bankAccountType/:bankAccountTypeId',
        component: BankAccountTypeComponent,
        data: {
          title: 'Update Bank Account Type',
        },
      },

    ]
  },
  {
    path: '',
    data: {
      title: 'Language Setup',
    },
    children: [
      {
        path: 'competence',
        component: CompetenceComponent,
        data: {
          title: 'Competence',
        },
      },
      {
        path: 'update-competence/:competenceId',
        component: CompetenceComponent,
        data: {
          title: 'Update Competence',
        },
      },
      {
        path: 'language',
        component: LanguageComponent,
        data: {
          title: 'Language',
        },
      },
      {
        path: 'update-language/:languageId',
        component: LanguageComponent,
        data: {
          title: 'Update Language',
        },
      },

    ]
  },
  {
    path: '',
    data: {
      title: 'Office Info. Setup',
    },
    children: [
      {
        path: 'office',
        component: OfficeComponent,
        data: {
          title: 'Office',
        },
      },
      {
        path: 'update-office/:officeId',
        component: OfficeComponent,
        data: {
          title: 'Update Office',
        },
      },
      {
        path: 'department',
        component: DepartmentComponent,
        data: {
          title: 'Department',
        },
      },
      {
        path: 'update-department/:departmentId',
        component: DepartmentComponent,
        data: {
          title: 'Update Department',
        },
      },
      {
        path: 'designation',
        component: DesignationComponent,
        data: {
          title: 'Designation',
        },
      },
      {
        path: 'update-designation/:designationId',
        component: DesignationComponent,
        data: {
          title: 'Update Designation',
        },
      },
      
      {
        path: 'organogram',
        component: OrganogramComponent,
        data: {
          title: 'Organogram',
        },
      },
      {
        path: 'officeAddress',
        component: OfficeAddressComponent,
        data: {
          title: 'Office Address',
        },
      },
      {
        path: 'update-officeAddress/:officeAddressId',
        component: OfficeAddressComponent,
        data: {
          title: 'Update Office Address',
        },
      },

    ]
  },


  {
    path: 'add-accounttype',
    component: NewAccountTypeComponent,
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BasicSetupRoutingModule { }
