export class AssignEmployeeModel {
    empId: number | null;
    idCardNo: string | null;
    deviceId: number | null;
    passwd: string | null;
    groupId: number;
    privilage: number;

    constructor () {
        this.empId = null;
        this.idCardNo = null;
        this.deviceId = null;
        this.passwd = "";
        this.groupId = 1;
        this.privilage = 0;
    }
}
