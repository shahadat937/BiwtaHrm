export class SiteVisitModel {
    siteVisitId: number;
    empId: number | null;
    firstName: string;
    lastName: string;
    fromDate: string;
    toDate: string; 
    visitPlace: string;
    visitPurpose: string;
    status: string;
    remark: string;

    constructor() {
        this.siteVisitId=0;
        this.empId = null;
        this.firstName="";
        this.lastName="";
        this.fromDate="";
        this.toDate="";
        this.visitPlace="";
        this.visitPurpose="";
        this.status = "";
        this.remark = "";
    }
}
