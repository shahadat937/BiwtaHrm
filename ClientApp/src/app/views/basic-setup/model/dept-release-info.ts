import { SafeResourceUrl } from "@angular/platform-browser";

export class DeptReleaseInfo {
    depReleaseInfoId: number = 0;
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
