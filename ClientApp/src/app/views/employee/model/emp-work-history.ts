export class EmpWorkHistory {
    id : number = 0;
    empId : number | null = null;
    joiningDate : Date | null = null;
    releaseDate : Date | null = null;
    remark : string = '';
    isActive : boolean = true;
    isCurrentJob : boolean = false;

    departmentName : string = '';
    sectionName : string = '';
    designationName : string = '';
    departmentNameBangla : string = '';
    sectionNameBangla : string = '';
    designationNameBangla : string = '';
    workPlace : string = '';
    workPlaceBangla : string = '';
}
