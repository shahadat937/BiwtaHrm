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
    name:'Add Employee',
    url: '/base',
    iconComponent: { name: 'cil-puzzle' },

  },
  {
    name:'Manage Employee',
    url: '/base',
    iconComponent: { name: 'cil-puzzle' },

  },
  {
    title: true,
    name: 'Attendance Setup'
  },
  {
    name:'Attendance',
    url: '/base',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Manage Shift',
        url: '/base/accordion'
      },
      {
        name: 'Workday Setting',
        url: '/base/breadcrumbs'
      },
      {
        name: 'Attendance Record',
        url: '/base/cards'
      },
      {
        name: 'Manual Attendance',
        url: '/base/carousel'
      },
      {
        name: 'Site Visit',
        url: '/base/collapse'
      },
      {
        name: 'Attendance Report',
        url: '/base/list-group'
      }

    ]
  },
  {
    title: true,
    name: 'Leave Management'
  },
  {
    name:'Leave',
    url: '/base',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Add leave',
        url: '/base/accordion'
      },
      {
        name: 'Manage leave',
        url: '/base/breadcrumbs'
      },
      {
        name: 'leave',
        url: '/bascisetup/leave'
      },

      {
        name: 'Overall_EV_Promotion',
        url: '/bascisetup/overall_EV_Promotion'
      }
    ]
  },
  {
    title: true,
    name: 'Transfer Management'
  },
  {
    name:'Transfer',
    url: '/base',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Transfer posting Form',
        url: '/base/accordion'
      },
      {
        name: 'Department Release',
        url: '/base/breadcrumbs'
      }
      ,
      {
        name: 'Transfer posting History',
        url: '/base/breadcrumbs'
      }


    ]
  },
  {
    title: true,
    name: 'Promotion Management'
  },
  {
    name:'Promotion',
    url: '/base',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Increment & Promotion',
        url: '/base/accordion'
      },
      {
        name: 'Incre.& Promo. Approval',
        url: '/base/breadcrumbs'
      }
      ,
      {
        name: 'Incr.& Promo. History',
        url: '/base/breadcrumbs'
      }


    ]
  },
  {
    name:'PromotionType',
    url: '/bascisetup',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'PromotionType',
        url: '/bascisetup/promotionType'
      },
    ]
  },
  {
    title: true,
    name: 'Appraisal Management'
  },
  {
    name:'Appraisal',
    url: '/base',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Staff Form',
        url: '/base/accordion'
      },
      {
        name: 'Manage Form',
        url: '/base/breadcrumbs'
      }
      ,
      {
        name: 'Officer Form',
        url: '/base/breadcrumbs'
      }


    ]
  },
  {
    title: true,
    name: 'User Management'
  },
  {
    name:'Role',
    url: '/base',
    iconComponent: { name: 'cil-puzzle' },

  },
  {
    name:'User',
    url: '/base',
    iconComponent: { name: 'cil-puzzle' },

  },
  {
    title: true,
    name: 'Basic Setup'
  },
  {
    name:'Personal Info. Setup',
    url: '/bascisetup',
    iconComponent: { name: 'cil-puzzle' },
    children: [
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
        name: 'User Role',
        url: '/base/breadcrumbs'
      },
      {
        name: 'Pool',
        url: '/base/cards'
      },
      {
        name: 'Department',
        url: '/bascisetup/department'
      },
      {
        name: 'Sub Department',
        url: '/bascisetup/subdepartment'
      },
      {
        name: 'Branch',
        url: '/bascisetup/branch'
      },
      {
        name: 'Sub Branch',
        url: '/bascisetup/subbranch'
      },
      {
        name: 'Section',
        url: '/base/pagination'
      },
      {
        name: 'Designation',
        url: '/bascisetup/designation'
      },
      {
        name: 'GradeType',
        url: '/bascisetup/grade-type'
      },
      {
        name: 'GradeClass',
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
        name: 'HairColor',
        url: '/bascisetup/hairColor'
      },
      {
        name: 'Eyes Color',
        url: '/bascisetup/eyesColor'
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
        name: 'Punishment/reward',
        url: '/bascisetup/punishment'
      }
    ]
  },
  {
    name:'Address Setup',
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
    name:'Education Setup',
    url: '/bascisetup',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Exam Type',
        url: '/base/accordion'
      },
      {
        name: 'Board',
        url: '/base/breadcrumbs'
      },
      {
        name: 'Group',
        url: '/base/cards'
      },
      {
        name: 'Result',
        url: '/bascisetup/result'
      },
      {
        name: 'Subject',
        url: '/bascisetup/subject'
      },
      {
        name: 'Subject Group',
        url: '/bascisetup/group'
      }

    ]
  },
  {
    name:'Training Setup',
    url: '/bascisetup',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Training Type',
        url: '/bascisetup/training'
      },
      {
        name: 'Training Name',
        url: '/base/breadcrumbs'
      },
      {
        name: 'Institute Name',
        url: '/base/cards'
      }

    ]
  },
  {
    name:'Bank Info. Setup',
    url: '/base',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Bank Name',
        url: '/base/accordion'
      },
      {
        name: 'Branch Name',
        url: '/base/breadcrumbs'
      },
      {
        name: 'Account Type',
        url: '/base/cards'
      }

    ]
  },
  {
    name:'Language Name Setup',
    url: '/base',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Language Name',
        url: '/base/accordion'
      },
      {
        name: 'Competence',
        url: '/base/breadcrumbs'
      }

    ]
  },
  {
    name:'Office Info. Setup',
    url: '/base',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Office Name',
        url: '/base/accordion'
      },
      {
        name: 'Address',
        url: '/base/breadcrumbs'
      }

    ]
  },
];
