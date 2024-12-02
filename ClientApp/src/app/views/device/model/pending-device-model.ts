export class PendingDeviceModel {
    id: number;
    sn: string;
    deviceType: string;
    deviceIp: string;
    expireTime: Date | null;

    constructor() {
        this.id = 0;
        this.sn = "";
        this.deviceType = "";
        this.deviceIp = "";
        this.expireTime = null;
    }
}
