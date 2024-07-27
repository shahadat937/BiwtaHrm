export class EmpTransferPosting {
    id: number | null = null;
    empId: number | null = null;

    idCardNo: string | null = null;
    empName: string | null = null;
    departmentName: string | null = null;
    designationName: string | null = null;
    sectionName: string | null = null;
    
    orderByIdCardNo: string | null = null;
    orderByEmpName: string | null = null;
    orderByDepartmentName: string | null = null;
    orderByDesignationName: string | null = null;
    orderBySectionName: string | null = null;

    approveByIdCardNo: string | null = null;
    approveByEmpName: string | null = null;
    approveByDepartmentName: string | null = null;
    approveByDesignationName: string | null = null;
    approveBySectionName: string | null = null;

    applicationById: number | null = null;
    currentOfficeId: number | null = null;
    currentDepartmentId: number | null = null;
    currentDesignationId: number | null = null;
    currentSectionId: number | null = null;
    officeOrderNo: string | null = null;
    officeOrderDate: Date | null = null;
    orderOfficeById: number | null = null;
    releaseTypeId: number | null = null;
    transferOfficeId: number | null = null;
    transferDepartmentId: number | null = null;
    transferDesignationId: number | null = null;
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
}
