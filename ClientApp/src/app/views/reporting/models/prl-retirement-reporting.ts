export class PrlRetirementReporting {
    Id : number = 0;
    IdCardNo : string = "";
    FirstName : string = "";
    LastName : string = "";
    FirstNameBangla : string = "";
    LastNameBangla : string = "";
    DateOfBirth : Date | null = null;
    JoiningDate : Date | null = null;

    DepartmentName : string = "";
    SectionName : string = "";
    DesignationName : string = "";

    PrlGone : boolean = false;
    PrlWillGone : boolean = false;
    RetirmentGone : boolean = false;
    RetirmentWillGone : boolean = false;
    TotalCount : number = 0;
}
