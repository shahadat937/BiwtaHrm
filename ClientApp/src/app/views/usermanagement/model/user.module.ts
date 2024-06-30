export class UserModule {
  id: string;
  userName: string;
  password: string;
  rePassword: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber : string;
  pNo : string;
  empId : number;
  menuPosition: number;
  isActive : boolean;
  
  constructor() {
    this.id = '';
    this.userName = '';
    this.password = '';
    this.rePassword = '';
    this.firstName = '';
    this.lastName = '';
    this.email = '';
    this.phoneNumber = '';
    this.pNo = '';
    this.empId = 0;
    this.menuPosition = 0;
    this.isActive = true;
  }
}
