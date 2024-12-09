export class EmpOtherResponsibility {
    id: number = 0;
    empId: number | null = null;
    orderNo: string = "";
    orderDate: Date | null = null;
    responsibilityTypeId: number | null = null;
    officeId: number | null = null;
    departmentId: any = null;
    sectionId: any = null;
    designationId: any = null;
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
