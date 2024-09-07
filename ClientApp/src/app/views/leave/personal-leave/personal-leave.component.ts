import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-personal-leave',
  templateUrl: './personal-leave.component.html',
  styleUrl: './personal-leave.component.scss'
})
export class PersonalLeaveComponent implements OnInit {
  filterLeave: any;
  Role: string = "User";
  CanApprove : boolean = false;

  constructor(
    private authService: AuthService
  ) {
  }

  ngOnInit(): void {
    this.filterLeave = {empId:this.authService.currentUserValue.empId}
  }
}
