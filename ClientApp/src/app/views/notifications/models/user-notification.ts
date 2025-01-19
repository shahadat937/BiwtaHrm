export class UserNotification {
    id : number = 0;
    fromEmpId : any;
    toEmpId : any;
    empIdCard: any;
    toDeptId : any;
    featureId : any;
    featurePath : string = '';
    UnreadCount : number = 0;
    isNotice : boolean = false;
    forAllUsers : boolean = false;
    title : string = '';
    message : string = '';
    nevigateLink : string = '';
    forEntryId : any;
    readStatus : boolean = false;
    fromEmpName : string = '';
    dateCreated : string = '';
    isActive : boolean = true;
}
