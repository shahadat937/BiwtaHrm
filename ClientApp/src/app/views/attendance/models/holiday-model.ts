export class HolidayModel {
    holidayId: number| null;
    holidayName: string;
    holidayDate: Date| null;
    holidayTypeId: number| null;
    yearId: number | null;
    isActive: boolean| null;
    yearName: string;
    holidayTypeName: string;
    remark: string;

    constructor() {
        this.holidayId=0;
        this.holidayName="";
        this.holidayDate=null;
        this.holidayTypeId=null;
        this.yearId=null;
        this.isActive=true;
        this.yearName="";
        this.holidayTypeName="";
        this.remark="";
    }
}
