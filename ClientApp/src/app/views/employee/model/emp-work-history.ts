export class EmpWorkHistory {
    id : number = 0;
    empId : number | null = null;
    joiningDate : Date | null = null;
    releaseDate : Date | null = null;
    orderDate : Date | null = null;
    remark : string = '';
    isActive : boolean = true;
    isCurrentJob : boolean = false;

    orderNo : string = '';
    departmentName : string = '';
    sectionName : string = '';
    designationName : string = '';
    departmentNameBangla : string = '';
    sectionNameBangla : string = '';
    designationNameBangla : string = '';
    workPlace : string = '';
    workPlaceBangla : string = '';
}
