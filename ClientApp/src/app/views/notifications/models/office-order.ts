export class OfficeOrder {
    id: number = 0;
    empId: number | null = null;
    orderTypeId: number | null = null;
    officeId: number | null = null;
    departmentId: number | null = null;
    sectionId: number | null = null;
    designationId: number | null = null;
    orderDate: Date = new Date;
    orderNo: string = "";
    orderFile: File | null = null;
    fileUrl: string = "";
    remark: string = "";
    isActive: boolean = true;
    menuPosition: number | null = null;

    orderTypeName: string = "";
    officeName: string = "";
    departmentName: string = "";
    sectionName: string = "";
    designationName: string = "";
}
