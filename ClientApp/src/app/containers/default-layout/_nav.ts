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
  // {
  //   title: true,
  //   name: 'Theme'
  // },
  // {
  //   name: 'Colors',
  //   url: '/theme/colors',
  //   iconComponent: { name: 'cil-drop' }
  // },
  // {
  //   name: 'Typography',
  //   url: '/theme/typography',
  //   linkProps: { fragment: 'someAnchor' },
  //   iconComponent: { name: 'cil-pencil' }
  // },
  // {
  //   name: 'Components',
  //   title: true
  // },
  // {
  //   name: 'Base',
  //   url: '/base',
  //   iconComponent: { name: 'cil-puzzle' },
  //   children: [
  //     {
  //       name: 'Accordion',
  //       url: '/base/accordion'
  //     },
  //     {
  //       name: 'Breadcrumbs',
  //       url: '/base/breadcrumbs'
  //     },
  //     {
  //       name: 'Cards',
  //       url: '/base/cards'
  //     },
  //     {
  //       name: 'Carousel',
  //       url: '/base/carousel'
  //     },
  //     {
  //       name: 'Collapse',
  //       url: '/base/collapse'
  //     },
  //     {
  //       name: 'List Group',
  //       url: '/base/list-group'
  //     },
  //     {
  //       name: 'Navs & Tabs',
  //       url: '/base/navs'
  //     },
  //     {
  //       name: 'Pagination',
  //       url: '/base/pagination'
  //     },
  //     {
  //       name: 'Placeholder',
  //       url: '/base/placeholder'
  //     },
  //     {
  //       name: 'Popovers',
  //       url: '/base/popovers'
  //     },
  //     {
  //       name: 'Progress',
  //       url: '/base/progress'
  //     },
  //     {
  //       name: 'Spinners',
  //       url: '/base/spinners'
  //     },
  //     {
  //       name: 'Tables',
  //       url: '/base/tables'
  //     },
  //     {
  //       name: 'Tabs',
  //       url: '/base/tabs'
  //     },
  //     {
  //       name: 'Tooltips',
  //       url: '/base/tooltips'
  //     }
  //   ]
  // },
  // {
  //   name: 'Buttons',
  //   url: '/buttons',
  //   iconComponent: { name: 'cil-cursor' },
  //   children: [
  //     {
  //       name: 'Buttons',
  //       url: '/buttons/buttons'
  //     },
  //     {
  //       name: 'Button groups',
  //       url: '/buttons/button-groups'
  //     },
  //     {
  //       name: 'Dropdowns',
  //       url: '/buttons/dropdowns'
  //     }
  //   ]
  // },
  // {
  //   name: 'Forms',
  //   url: '/forms',
  //   iconComponent: { name: 'cil-notes' },
  //   children: [
  //     {
  //       name: 'Form Control',
  //       url: '/forms/form-control'
  //     },
  //     {
  //       name: 'Select',
  //       url: '/forms/select'
  //     },
  //     {
  //       name: 'Checks & Radios',
  //       url: '/forms/checks-radios'
  //     },
  //     {
  //       name: 'Range',
  //       url: '/forms/range'
  //     },
  //     {
  //       name: 'Input Group',
  //       url: '/forms/input-group'
  //     },
  //     {
  //       name: 'Floating Labels',
  //       url: '/forms/floating-labels'
  //     },
  //     {
  //       name: 'Layout',
  //       url: '/forms/layout'
  //     },
  //     {
  //       name: 'Validation',
  //       url: '/forms/validation'
  //     }
  //   ]
  // },
  // {
  //   name: 'Charts',
  //   url: '/charts',
  //   iconComponent: { name: 'cil-chart-pie' }
  // },
  // {
  //   name: 'Icons',
  //   iconComponent: { name: 'cil-star' },
  //   url: '/icons',
  //   children: [
  //     {
  //       name: 'CoreUI Free',
  //       url: '/icons/coreui-icons',
  //       badge: {
  //         color: 'success',
  //         text: 'FREE'
  //       }
  //     },
  //     {
  //       name: 'CoreUI Flags',
  //       url: '/icons/flags'
  //     },
  //     {
  //       name: 'CoreUI Brands',
  //       url: '/icons/brands'
  //     }
  //   ]
  // },
  // {
  //   name: 'Notifications',
  //   url: '/notifications',
  //   iconComponent: { name: 'cil-bell' },
  //   children: [
  //     {
  //       name: 'Alerts',
  //       url: '/notifications/alerts'
  //     },
  //     {
  //       name: 'Badges',
  //       url: '/notifications/badges'
  //     },
  //     {
  //       name: 'Modal',
  //       url: '/notifications/modal'
  //     },
  //     {
  //       name: 'Toast',
  //       url: '/notifications/toasts'
  //     }
  //   ]
  // },
  // {
  //   name: 'Widgets',
  //   url: '/widgets',
  //   iconComponent: { name: 'cil-calculator' },
  //   badge: {
  //     color: 'info',
  //     text: 'NEW'
  //   }
  // },
  // {
  //   title: true,
  //   name: 'Extras'
  // },
  // {
  //   name: 'Pages',
  //   url: '/login',
  //   iconComponent: { name: 'cil-star' },
  //   children: [
  //     {
  //       name: 'Login',
  //       url: '/login'
  //     },
  //     {
  //       name: 'Register',
  //       url: '/register'
  //     },
  //     {
  //       name: 'Error 404',
  //       url: '/404'
  //     },
  //     {
  //       name: 'Error 500',
  //       url: '/500'
  //     }
  //   ]
  // },
  // {
  //   title: true,
  //   name: 'Links',
  //   class: 'py-0'
  // },
  // {
  //   name: 'Docs',
  //   url: 'https://coreui.io/angular/docs/templates/installation',
  //   iconComponent: { name: 'cil-description' },
  //   attributes: { target: '_blank', class: '-text-dark' },
  //   class: 'mt-auto'
  // },
  // {
  //   name: 'Try CoreUI PRO',
  //   url: 'https://coreui.io/product/angular-dashboard-template/',
  //   iconComponent: { name: 'cil-layers' },
  //   attributes: { target: '_blank' }
  // },
  {
    title: true,
    name: 'Employee Management'
  },
  {
    name: 'Add Employee',
    url: '/employee/addEmployee',
    iconComponent: { name: 'cil-puzzle' },

  },
  {
    name: 'Manage Employee',
    url: '/employee/manageEmployee',
    iconComponent: { name: 'cil-puzzle' },

  },
  {
    title: true,
    name: 'Attendance Setup'
  },
  {
    name: 'Attendance',
    url: '/attendance',
    iconComponent: { name: 'cil-puzzle' },
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
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Add leave',
        url: '/leave/addleave'
      },
      {
        name: 'Manage leave',
        url: '/leave/manageleave'
      },
    ]
  },
  {
    title: true,
    name: 'Transfer'
  },
  {
    name: 'Transfer',
    url: '/transfer',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Transfer&Posting Management',
        url: '/transfer/postingOrderInfo'
      },
      {
        name: 'Transfer Order List',
        url: '/transfer/TransferOrderList'
      },
      {
        name: 'Transfer Approved List',
        url: '/transfer/transferApproveInfoList'
      },

      {
        name: 'Departmetn Release List',
        url: '/transfer/departmetnReleaseList'
      },
      {
        name: 'Employee Transfer Join',
        url: '/transfer/employePostingJoinList'
      },
      
      {
        name: 'Transfer posting History',
        url: '/transfer/'
      }
    ]
  },
  {
    title: true,
    name: 'Promotion Management'
  },
  {
    name: 'Promotion',
    url: '/promotion',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Increment & Promotion',
        url: '/promotion/incrementAndPromotion'
      },
      {
        name: 'Incre.& Promo. Approval',
        url: '/promotion/incrementAndPromotionApproval'
      }
      ,
      {
        name: 'Incr.& Promo. History',
        url: '/promotion/incrementAndPromotionHistory'
      }
      ,
      {
         name:'Manage Promotion',
         url:'/promotion/managePromotion'
      }
    ]
  },
  {
    name: 'PromotionType',
    url: '/bascisetup',
    iconComponent: { name: 'cil-puzzle' },
    children: [

    ]
  },
  {
    title: true,
    name: 'Appraisal Management'
  },
  {
    name: 'Appraisal',
    url: '/appraisal',
    iconComponent: { name: 'cil-puzzle' },
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
    name: 'Role',
    url: '/base',
    iconComponent: { name: 'cil-puzzle' },

  },
  {
    name: 'User',
    url: '/usermanagement/user',
    iconComponent: { name: 'cil-puzzle' },

  },
  {
    title: true,
    name: 'Basic Setup'
  },
  {
    name: 'Personal Info. Setup',
    url: '/bascisetup',
    iconComponent: { name: 'cil-puzzle' },
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
    ]
  },
  {
    name: 'Address Setup',
    url: '/bascisetup',
    iconComponent: { name: 'cil-puzzle' },
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
    iconComponent: { name: 'cil-puzzle' },
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
    iconComponent: { name: 'cil-puzzle' },
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
    iconComponent: { name: 'cil-puzzle' },
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
    iconComponent: { name: 'cil-puzzle' },
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
    iconComponent: { name: 'cil-puzzle' },
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
];
