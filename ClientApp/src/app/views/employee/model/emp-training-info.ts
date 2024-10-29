export class EmpTrainingInfo {
    id: number = 0;
    empId: number | null = null;
    trainingTypeId: number | null = null;
    fromDate: Date | null = null;
    toDate: Date | null = null;
    fileUrl: string | null = null;
    countryId: number | null = null;
    remark: string | null = null;
    menuPosition: number | null = null;
    isActive: boolean = true;

    trainingTypeName: string = "";
    trainingName: string = "";
    instituteName: string = "";
    trainingDuration: string = "";
    countryName: string = "";
}
