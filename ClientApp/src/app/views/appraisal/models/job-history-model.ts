export class JobHistoryModel {
    empId:number|null;
    startDate: string;
    endDate: string;
    departmentId: number | null;
    departmentName: string; 
    sectionId: number | null;
    sectionName: string;
    designationId: number | null;
    designationName: string;
    joiningDate: string ;
    releaseDate: string ;

    constructor() {
        this.empId = null;
        this.startDate = "";
        this.endDate = "";
        this.joiningDate  = "";
        this.releaseDate = "";
        this.departmentId = null;
        this.departmentName = "";
        this.sectionId = null;
        this.sectionName = "";
        this.designationId = null;
        this.designationName = "";
    }
}
