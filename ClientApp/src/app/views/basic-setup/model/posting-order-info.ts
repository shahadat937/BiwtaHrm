export class PostingOrderInfo {
    postingOrderInfoId: number = 0;
    empId: number = 0;
    departmentId:number=0;
    subBranchId:number=0;
    subDepartmentId:number=0;
    officeBranchId:number=0;
    officeOrderNo:string="";
    officeOrderDate:Date= new Date();
    orderOfficeBy:string="";
    transferSection:string="";
    releaseType:string="";
    menuPosition: number = 0;
    isActive: boolean = true
}
