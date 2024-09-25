export class EmpOtherResponsibility {
    id: number = 0;
    empId: number | null = null;
    empOtherResponsibilityId: number | null = null;
    officeId: number | null = null;
    departmentId: number | null = null;
    sectionId: number | null = null;
    designationId: number | null = null;
    startDate: Date | null = null;
    endDate: Date | null = null;
    serviceStatus: boolean = true;
    remark: string = "";
    isActive: boolean = true;

    responsibilityName: string = "";
    officeName: string = "";
    departmentName: string = "";
    sectionName: string = "";
    designationName: string = "";
}
