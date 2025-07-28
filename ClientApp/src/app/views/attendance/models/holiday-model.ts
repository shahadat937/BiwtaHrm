export class HolidayModel {
    holidayId: number| null;
    holidayName: string;
    holidayDate: Date| null;
    holidayFrom: any;
    holidayTo: any;
    holidayTypeId: number| null;
    yearId: number | null;
    isActive: boolean = true;
    yearName: string;
    holidayTypeName: string;
    remark: string;
    groupId: number;

    constructor() {
        this.holidayId=0;
        this.holidayName="";
        this.holidayDate=null;
        this.holidayFrom = null;
        this.holidayTo = null;
        this.holidayTypeId=null;
        this.yearId=null;
        this.isActive=true;
        this.yearName="";
        this.holidayTypeName="";
        this.remark="";
        this.groupId = 0;
    }
}
