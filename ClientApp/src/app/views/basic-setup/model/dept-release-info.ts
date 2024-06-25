import { SafeResourceUrl } from "@angular/platform-browser";

export class DeptReleaseInfo {
    depReleaseInfoId: number = 0;
    postingOrderInfoId:number=0;
    transferApproveInfoId:number=0;
    approveByName:string= "";
    approveBy:number=0;
    approveStatus:  boolean = true;
    empId:number=0;
    officeOrderNo: string = ""; 
    releaseDate:Date= new Date();
    orderOfficeBy:string="";
    referenceNo:string="";
    depClearance:string="";
    releaseType:string="";
    remarks:string="";
    menuPosition: number = 0;
    isActive: boolean = true;
}
