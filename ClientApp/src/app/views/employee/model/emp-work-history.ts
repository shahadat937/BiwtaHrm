export class EmpWorkHistory {
    id : number = 0;
    empId : number | null = null;
    officeId : number | null = null;
    departmentId : number | null = null;
    sectionId : number | null = null;
    designationId : number | null = null;
    joiningDate : Date | null = null;
    releaseDate : Date | null = null;
    remark : string = '';
    IsActive : boolean = true;

    officeName : string = '';
    departmentName : string = '';
    sectionName : string = '';
    designationName : string = '';
}
