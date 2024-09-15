export class EmpRewardPunishment {
    id : number = 0;
    empId : number | null = null;
    rewardPunishmentTypeId : number | null = null;
    rewardPunishmentPriorityId : number | null = null;
    rewardPunishmentDate : Date | null = null;
    startDate : Date | null = null;
    endDate : Date | null = null;
    description : string = '';
    orderNo : string = '';
    orderDate : Date | null = null;
    withdrawStatus : boolean = false;
    withdrawDate : Date | null = null;
    orderBy : number | null = null;
    applicationBy : number | null = null;
    approveById : number | null = null;
    approveDate : Date | null = null;
    approveStatus : boolean | null = null;
    menuPosition : number = 0;
    remark : string = '';
    isActive : boolean = true;

    empIdCardNo : string = '';
    empName : string = '';
    departmentName : string = '';
    designationName : string = '';
    rewardPunishmentTypeName : string = '';
    rewardPunishmentPriorityName : string = '';
}
