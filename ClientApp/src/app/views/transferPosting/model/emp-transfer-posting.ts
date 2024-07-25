export class EmpTransferPosting {
    id: number | null = null;
    empId: number | null = null;

    idCardNo: number | null = null;
    empName: string | null = null;
    departmentName: string | null = null;
    designationName: string | null = null;
    sectionName: string | null = null;
    
    orderByIdCardNo: number | null = null;
    orderByEmpName: string | null = null;
    orderByDepartmentName: string | null = null;
    orderByDesignationName: string | null = null;
    orderBySectionName: string | null = null;

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
    isTransferApprove: boolean | null = null;
    transferApproveById: number | null = null;
    transferApproveDate: Date | null = null;
    approveRemark: string | null = null;
    transferApproveStatus: boolean | null = null;
    isDepartmentApprove: boolean | null = null;
    deptReleaseTypeId: number | null = null;
    deptReleaseById: number | null = null;
    deptReleaseDate: Date | null = null;
    referenceNo: string | null = null;
    deptClearance: boolean | null = null;
    deptRemark: string | null = null;
    deptApproveStatus: boolean | null = null;
    isJoining: boolean | null = null;
    joiningReportingById: number | null = null;
    joiningDate: Date | null = null;
    joiningRemark: string | null = null;
    joiningStatus: boolean | null = null;
    applicationStatus: boolean | null = null;
    remark: string | null = null;
    menuPosition: number | null = null;
    isActive: boolean | null = null;
}
