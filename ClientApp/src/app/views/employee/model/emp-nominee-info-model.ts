export class EmpNomineeInfoModel {
    id: number = 0;
    empId: any = null;
    pNo: string = "";
    nomineeName : string = "";
    dateOfBirth : Date | null = null;
    birthRegNo : string = "";
    nid : string = "";
    relationId : any = null;
    relationName : any = null;
    percentage : number = 0;
    address : string = "";
    photoUrl: string = "";
    signatureUrl: string = "";
    photoFile: File | null = null;
    signatureFile: File | null = null;
    uniqueIdentity: string = "";
    remark: string = "";
    menuPosition: number = 0;
    isActive: boolean = true
}
