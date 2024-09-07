import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    iconComponent: { name: 'cil-speedometer' },
    badge: {
      color: 'info',
      text: 'NEW'
    }
  },
  
  {
    title: true,
    name: 'Employee Management'
  },
  {
    name: 'Add Employee',
    url: '/employee/employeeList',
    iconComponent: { name: 'cil-user' },

  },
  {
    name: 'Manage Employee',
    url: '/employee/manageEmployee',
    iconComponent: { name: 'cil-people' },

  },
  {
    title: true,
    name: 'Attendance Setup'
  },
  {
    name: 'Attendance',
    url: '/attendance',
    iconComponent: { name: 'cil-list' },
    children: [
      {
        name: 'Manage Shift',
        url: '/attendance/manageShift'
      },
      {
        name: 'Workday Setting',
        url: '/attendance/workdaySetting'
      },
      {
        name: 'Attendance Record',
        url: '/attendance/attendanceRecord'
      },
      {
        name: 'Manual Attendance',
        url: '/attendance/manualAttendance'
      },
      {
        name: 'Site Visit',
        url: '/attendance/siteVisit'
      },
      {
        name: 'Attendance Report',
        url: '/attendance/attendanceReport'
      },
      {
        name: "Attendance Summary",
        url: '/attendance/attendanceSummary'
      }

    ]
  },
  {
    title: true,
    name: 'Leave Management'
  },
  {
    name: 'Leave',
    url: '/leave',
    iconComponent: { name: 'cil-user-unfollow' },
    children: [
      {
        name: 'Add leave',
        url: '/leave/addleave'
      },
      {
        name: 'Manage Leave',
        url: '/leave/manageleave'
      },
      {
        name: 'Review Leave',
        url: '/leave/reviewleave'
      }
    ]
  },
  // {
  //   title: true,
  //   name: 'Transfer'
  // },
  // {
  //   name: 'Transfer',
  //   url: '/transfer',
  //   iconComponent: { name: 'cil-paper-plane' },
  //   children: [
  //     {
  //       name: 'Transfer&Posting Management',
  //       url: '/transfer/postingOrderInfo'
  //     },
  //     {
  //       name: 'Transfer Order List',
  //       url: '/transfer/TransferOrderList'
  //     },
  //     {
  //       name: 'Transfer Approved List',
  //       url: '/transfer/transferApproveInfoList'
  //     },

  //     {
  //       name: 'Departmetn Release List',
  //       url: '/transfer/departmetnReleaseList'
  //     },
  //     {
  //       name: 'Employee Transfer Join',
  //       url: '/transfer/employePostingJoinList'
  //     },
      
  //     {
  //       name: 'Transfer posting History',
  //       url: '/transfer/'
  //     }
  //   ]
  // },

  
  {
    title: true,
    name: 'Transfer and Posting'
  },
  {
    name: 'Transfer',
    url: '/transferPosting',
    iconComponent: { name: 'cil-paper-plane' },
    children: [
      {
        name: 'New Order Application',
        url: '/transferPosting/transferPostingApplication/'
      },
      {
        name: 'Transfer Posting List',
        url: '/transferPosting/transferPostingList/'
      },
      {
        name: 'Approval List',
        url: '/transferPosting/transferPostingApprovalList/'
      },
      {
        name: 'Dept Approval List',
        url: '/transferPosting/departmentApprovalList/'
      },
      {
        name: 'Joining  List',
        url: '/transferPosting/joiningReportingList/'
      },
    ]
  },
  
  {
    title: true,
    name: 'Promotion Management'
  },
  {
    name: 'Promotion',
    url: '/promotion',
    iconComponent: { name: 'cil-arrow-top' },
    children: [
      {
        name: 'Increment & Promotion',
        url: '/promotion/incrementAndPromotion'
      },
      {
         name:'Manage Promotion',
         url:'/promotion/manage-incrementAndPromotion'
      },
      {
        name: 'Approval List',
        url: '/promotion/incrementAndPromotionApproval'
      },
    ]
  },
  {
    title: true,
    name: 'Appraisal Management'
  },
  {
    name: 'Appraisal',
    url: '/appraisal',
    iconComponent: { name: 'cil-description' },
    children: [
      {
        name: 'Staff Form',
        url: '/appraisal/staffForm'
      },
      {
        name: 'Manage Form',
        url: '/appraisal/manageForm'
      }
      ,
      {
        name: 'Officer Form',
        url: '/appraisal/officerForm'
      }


    ]
  },
  {
    title: true,
    name: 'User Management'
  },
  {
    name: 'Role Permission',
    url: '/usermanagement/rolePermission',
    iconComponent: { name: 'cil-user-follow' },

  },
  {
    name: 'User',
    url: '/usermanagement/user',
    iconComponent: { name: 'cil-user' },

  },
  {
    title: true,
    name: 'Basic Setup'
  },
  {
    name: 'Personal Info. Setup',
    url: '/bascisetup',
    iconComponent: { name: 'cil-people' },
    children: [
      {
        name: 'User Role',
        url: '/bascisetup/userRole'
      },
      {
        name: 'Employee Type',
        url: '/bascisetup/employee-type'
      },
      {
        name: 'Blood Group',
        url: '/bascisetup/blood-group'
      },
      {
        name: 'Marital Status',
        url: '/bascisetup/marital-status'
      },
      {
        name: 'Child Status',
        url: '/bascisetup/child-status'
      },
      {
        name: 'Pool',
        url: '/bascisetup/pool'
      },
      {
        name: 'Sub Department',
        url: '/bascisetup/subDepartment'
      },
      // {
      //   name: 'Branch',
      //   url: '/bascisetup/officeBranch'
      // },
      // {
      //   name: 'Sub Branch',
      //   url: '/bascisetup/subBranch'
      // },
      {
        name: 'Section',
        url: '/bascisetup/section'
      },
      {
        name: 'Grade Type',
        url: '/bascisetup/grade-type'
      },
      {
        name: 'Grade Class',
        url: '/bascisetup/grade-class'
      },
      {
        name: 'Grade',
        url: '/bascisetup/grade'
      },
      {
        name: 'Scale',
        url: '/bascisetup/scale'
      },
      {
        name: 'Religion',
        url: '/bascisetup/religion'
      },
      {
        name: 'Hair Color',
        url: '/bascisetup/hairColor'
      },
      {
        name: 'Eyes Color',
        url: '/bascisetup/eyesColor'
      },
      {
        name: 'Gender',
        url: '/bascisetup/gender'
      },
      {
        name: 'Relation',
        url: '/bascisetup/relation'
      },
      {
        name: 'Occupation',
        url: '/bascisetup/occupation'
      },
      {
        name: 'Punishment or Reward',
        url: '/bascisetup/punishment'
      },
      {
        name: 'leave',
        url: '/bascisetup/leave'
      },

      {
        name: 'Overall Evelation and Promotion',
        url: '/bascisetup/overall_EV_Promotion'
      },
      {
        name: 'Promotion Type',
        url: '/bascisetup/promotionType'
      },
      {
        name: 'Year Setup',
        url: '/bascisetup/yearsetup'
      },
      {
        name: 'Holiday Type',
        url: '/bascisetup/holidaytype'
      },
      {
        name: 'Release Type',
        url: '/bascisetup/releaseType'
      },
    ]
  },
  {
    name: 'Address Setup',
    url: '/bascisetup',
    iconComponent: { name: 'cil-location-pin' },
    children: [
      {
        name: 'Country',
        url: '/bascisetup/country'
      },
      {
        name: 'Division',
        url: '/bascisetup/division'
      },
      {
        name: 'District',
        url: '/bascisetup/district'
      },
      {
        name: 'Upazila',
        url: '/bascisetup/upazila'
      },
      {
        name: 'Thana',
        url: '/bascisetup/thana'
      },
      {
        name: 'Union',
        url: '/bascisetup/union'
      },
      {
        name: 'Ward',
        url: '/bascisetup/ward'
      }

    ]
  },
  {
    name: 'Education Setup',
    url: '/bascisetup',
    iconComponent: { name: 'cil-pencil' },
    children: [
      {
        name: 'Exam Type',
        url: '/bascisetup/examType'
      },
      {
        name: 'Board',
        url: '/bascisetup/board'
      },
      {
        name: 'Sub Group',
        url: '/bascisetup/group'
      },
      {
        name: 'Result',
        url: '/bascisetup/result'
      },
      {
        name: 'Subject',
        url: '/bascisetup/subject'
      },
    ]
  },
  {
    name: 'Training Setup',
    url: '/bascisetup',
    iconComponent: { name: 'cil-notes' },
    children: [
      {
        name: 'Training Type',
        url: '/bascisetup/training'
      },
      {
        name: 'Training Name',
        url: '/bascisetup/trainingName'
      },
      {
        name: 'Institute Name',
        url: '/bascisetup/institute'
      }

    ]
  },
  {
    name: 'Bank Info. Setup',
    url: '/base',
    iconComponent: { name: 'cil-credit-card' },
    children: [
      {
        name: 'Bank Name',
        url: '/bascisetup/bank'
      },
      {
        name: 'Branch Name',
        url: '/bascisetup/bankBranch'
      },
      {
        name: 'Account Type',
        url: '/bascisetup/bankAccountType'
      },
    ]
  },
  {
    name: 'Language Name Setup',
    url: '/base',
    iconComponent: { name: 'cil-speech' },
    children: [
      {
        name: 'Language Name',
        url: '/bascisetup/language'
      },
      {
        name: 'Competence',
        url: '/bascisetup/competence'
      }

    ]
  },
  {
    name: 'Office Info. Setup',
    url: '/bascisetup',
    iconComponent: { name: 'cil-home' },
    children: [
      {
        name: 'Office',
        url: '/bascisetup/office'
      },
      {
        name: 'Department',
        url: '/bascisetup/department'
      },
      {
        name: 'Designation',
        url: '/bascisetup/designation'
      },
      {
        name: 'Organogram',
        url: '/bascisetup/organogram'
      },
      {
        name: 'Office Address',
        url: '/bascisetup/officeAddress'
      }
    ]
  },
  
  {
    name: 'Navbar Setup',
    url: '/navbarsetup',
    iconComponent: { name: 'cil-menu' },
    children: [
      {
        name: 'Module',
        url: '/navbarsetup/module'
      },
      {
        name: 'Feature',
        url: '/navbarsetup/feature'
      },
      {
        name: 'Role Feature',
        url: '/navbarsetup/roleFeature'
      },
    ]
  },
];
