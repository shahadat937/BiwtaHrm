import { Role } from './role';

export class User {
  id: number;
  empId: string
  img: string;
  username: string;
  password: string;
  firstName: string;
  lastName: string;
  branchId: string;
  traineeId: string;
  role: Role;
  token?: string;
  departmentId: string;
  sectionId : string;
  designationId: string;
  responsibilityTypeId: string
  constructor() {
    this.id = 0;
    this.img = '';
    this.username = '';
    this.password = '';
    this.firstName = '';
    this.lastName = '';
    this.branchId = '';
    this.empId = '';
    this.traineeId = '';
    this.role = Role.All; // You need to replace 'Role.Default' with the default role value
    // this.token = ''; // Optionally initialize token if it's not always provided
    this.departmentId = '';
    this.sectionId = '';
    this.designationId = '';
    this.responsibilityTypeId = ''
  }
}