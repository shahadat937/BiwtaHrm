export class EmpTransferPosting {
    id: number = 0;
    empId: number | null = null;

    empIdCardNo: string | null = null;
    empName: string | null = null;
    departmentName: string | null = null;
    designationName: string | null = null;
    sectionName: string | null = null;
    
    approveByIdCardNo: string | null = null;
    approveByEmpName: string | null = null;
    approveByDepartmentName: string | null = null;
    approveByDesignationName: string | null = null;
    approveBySectionName: string | null = null;
    
    deptReleaseByIdCardNo: string | null = null;
    deptReleaseByEmpName: string | null = null;
    deptReleaseByDepartmentName: string | null = null;
    deptReleaseByDesignationName: string | null = null;
    deptReleaseBySectionName: string | null = null;
    deptReleaseByDepartmentId:  number | null  = null;
    deptReleaseBySectionId:  number | null  = null;
    deptReleaseByResponsibilityTypeId:  number | null  = null;
    deptReleaseByDesignationId:  number | null  = null;
    
    joiningReportingByIdCardNo: string | null = null;
    joiningReportingByEmpName: string | null = null;
    joiningReportingByDepartmentName: string | null = null;
    joiningReportingByDesignationName: string | null = null;
    joiningReportingBySectionName: string | null = null;
    joiningReportingByDepartmentId: number | null  = null;
    joiningReportingBySectionId: number | null  = null;
    joiningReportingByResponsibilityTypeId: number | null  = null;
    joiningReportingByDesignationId: number | null  = null;
  

    releaseTypeName: string | null = null;
    deptReleaseTypeName: string | null = null;

    applicationById: number | null = null;
    applicantDepartmentId:  number | null  = null;
  applicantSectionId:  number | null  = null;
  applicantDesignationId:  number | null  = null;
  applicantJobResponsibilityTypeId:  number | null  = null;
    currentOfficeId: number | null = null;
    currentDeptJoinDate: Date | null = null;
    currentDepartmentId: number | null = null;
    currentDesignationId: number | null = null;
    currentSectionId: number | null = null;

    currentGradeId: number | null = null;
    currentScaleId: number | null = null;
    currentBasicPay: number | null = null;
    updateGradeId: number | null = null;
    updateScaleId: number | null = null;
    updateBasicPay: number | null = null;

    withPromotion: boolean = false;
    currentGradeName: string = '';
    currentScaleName: string = '';
    updateGradeName: string = '';
    updateScaleName: string = '';

    officeOrderNo: string | null = null;
    officeOrderDate: Date | null = null;
    orderOfficeById: number | null = null;
    orderByOffice: string = '';

    releaseTypeId: number | null = null;
    transferOfficeId: number | null = null;
    transferDepartmentId: number | null = null;
    transferDesignationId: number | null = null;
    transferDepartmentName: string | null = null;
    transferDesignationName: string | null = null;
    transferSectionName: string | null = null;
    transferSectionId: number | null = null;
    isTransferApprove: boolean = true;

    provideTransferApproveInfo: boolean = false;
    transferApproveById: number | null = null;
    transferApproveDate: Date | null = null;
    approveRemark: string | null = null;
    transferApproveStatus: boolean | null = null;

    isDepartmentApprove: boolean = true;
    provideDepartmentApproveInfo: boolean = false;
    deptReleaseTypeId: number | null = null;
    deptReleaseById: number | null = null;
    deptReleaseDate: Date | null = null;
    referenceNo: string | null = null;
    deptClearance: boolean = true;
    deptRemark: string | null = null;
    deptApproveStatus: boolean | null = null;

    isJoining: boolean = true;
    provideJoiningInfo: boolean = false;
    joiningReportingById: number | null = null;
    joiningDate: Date | null = null;
    joiningRemark: string | null = null;
    joiningStatus: boolean | null = null;

    applicationStatus: boolean | null = null;
    remark: string | null = null;
    menuPosition: number | null = null;
    isActive: boolean | null = null;
    isAdditionalDesignation : boolean | null = null;
    responsibilityTypeId : number | null = null;
    transferResponsibilityTypeId: number | null = null;
    currentResponsibiltyTypeId: number | null = null;
}
