export class FormRecordModel {
    recordId:number|null;
    formId: number|null;
    empId: number|null;
    idCardNo: string;
    fullName: string;
    empFirstName: string;
    empLastName: string;
    department: string;
    isActive: boolean|null;
    remark: string;
    menuPosition: number|null;

    constructor() {
       this.recordId=null;
       this.formId=null;
       this.empId=null;
       this.idCardNo="";
       this.fullName = "";
       this.empFirstName="";
       this.empLastName="";
       this.department="";
       this.isActive=null;
       this.remark="";
       this.menuPosition=null; 
    }
}
