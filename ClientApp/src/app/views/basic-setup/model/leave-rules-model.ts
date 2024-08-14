export class LeaveRulesModel {
    ruleId: number;
    leaveTypeId: number | null;
    ruleName: string | null;
    ruleValue: number | null;
    remark: string;
    isActive: boolean|null;

    constructor() {
        this.ruleId = 0;
        this.leaveTypeId = null;
        this.ruleName = null;
        this.ruleValue = null;
        this.remark = "";
        this.isActive = null;
    }
}
