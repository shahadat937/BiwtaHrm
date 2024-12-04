export class AttendanceDeviceModel {
    id: number;
    title: string;
    sn: string;
    deviceName: string;
    area: string;
    mac: string;
    localIpAddress: string;
    attStamp: string;
    opStamp: string;
    oem: string;
    pushVersion: string;
    language: string;
    timezone: string | null;
    accDevice: boolean;
    lastOnline: Date | null;
    status: boolean

    constructor() {
        this.id = 0;
        this.title = "";
        this.sn = "";
        this.deviceName = "";
        this.area = "";
        this.mac = "";
        this.localIpAddress = "";
        this.attStamp = "";
        this.opStamp = "";
        this.oem = "";
        this.pushVersion = "";
        this.language = "";
        this.timezone = null;
        this.accDevice = false;
        this.lastOnline = null;
        this.status = true;
    }
}
