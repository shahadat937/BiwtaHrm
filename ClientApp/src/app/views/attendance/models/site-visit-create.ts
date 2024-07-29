import { from } from "rxjs";

export class SiteVisitCreate {
    siteVisitId: number|null;
    empId: number| null;
    fromDate: Date | null;
    toDate: Date | null;
    visitPlace: string | null;
    visitPurpose: string | null;
    remark: string | null;
    status: string | null;

    constructor() {
        this.siteVisitId = null;
        this.empId = null;
        this.fromDate = null;
        this.toDate = null;
        this.visitPlace = null;
        this.visitPlace = null;
        this.visitPurpose = null;
        this.remark = null;
        this.status = null;
    }
}
