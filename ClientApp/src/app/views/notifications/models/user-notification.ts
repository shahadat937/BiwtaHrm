export class UserNotification {
    id : number = 0;
    fromEmpId : number = 0;
    toEmpId : number = 0;
    toDeptId : number = 0;
    featureId : number = 0;
    isNotice : boolean = false;
    forAllUsers : boolean = false;
    title : string = '';
    message : string = '';
    nevigateLink : string = '';
    readStatus : boolean = false;
}
