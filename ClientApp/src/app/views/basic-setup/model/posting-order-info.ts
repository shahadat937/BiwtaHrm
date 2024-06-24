export class PostingOrderInfo {
    postingOrderInfoId: number = 0;
    empId: number = 0;
    departmentId:any=null;
    subBranchId:number=0;
    subDepartmentId:number=0;
    designationId:any=null;
    designationName:string="";
    officeName:string="";
    departmentName:string="";
    officeId: any=null;
    officeOrderNo:string="";
    officeOrderDate:Date= new Date();
    orderOfficeBy:string="";
    transferSection:string="";
    releaseType:string="";
    menuPosition: number = 0;
    isActive: boolean = true;
    status:string=""
}
interface PostingOrderInfoWithStatus extends PostingOrderInfo {
    status: 'Pending' | 'Approved' | 'Rejected';
  }
  

// export interface PostingOrderInfo {
//     postingOrderInfoId: number;
//     empId: number;
//     departmentId: any;
//     subBranchId: number;
//     subDepartmentId: number;
//     designationId: any;
//     officeId: any;
//     designationName: string;
//     officeName: string;
//     departmentName: string;
//     officeOrderNo: string;
//     officeOrderDate: Date;
//     orderOfficeBy: string;
//     transferSection: string;
//     releaseType: string;
//     menuPosition: number;
//     isActive: boolean;
//     status: 'Pending' | 'Approved' | 'Rejected'; // Add this field
//   }
  

