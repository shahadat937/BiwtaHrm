export class EmpRewardPunishment {
    Id : number = 0;
    EmpId : number | null = null;
    RewardPunishmentTypeId : number | null = null;
    RewardPunishmentPriorityId : number | null = null;
    RewardPunishmentDate : Date | null = null;
    StartDate : Date | null = null;
    EndDate : Date | null = null;
    Description : string = '';
    OrderNo : string = '';
    WithdrawStatus : boolean | null = null;
    WithdrawDate : Date | null = null;
    OrderBy : number | null = null;
    ApplicationBy : number | null = null;
    ApproveById : number | null = null;
    ApproveDate : Date | null = null;
    ApproveStatus : boolean | null = null;
    MenuPosition : number = 0;
    Remark : string = '';
    IsActive : boolean = true;
}
