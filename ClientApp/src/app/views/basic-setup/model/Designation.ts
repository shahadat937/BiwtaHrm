export class Designation {
    designationId: number=0;
    designationSetupId: number | null = null;
    officeId: any = null;
    departmentId: any = null;
    sectionId: any = null;
    remark: string="";
    createCount: number = 1;
    menuPosition: number=0;
    isActive:boolean= true;
    officeName: string = "";
    departmentName: string = "";
    sectionName: string = "";
    designationName: string="";
}