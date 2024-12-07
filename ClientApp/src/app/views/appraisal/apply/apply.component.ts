import { Component, OnDestroy, OnInit } from '@angular/core';
import { EmpJobDetailsService } from '../../employee/service/emp-job-details.service';
import { GradeService } from '../../basic-setup/service/Grade.service';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/core/service/auth.service';
import { Subscription } from 'rxjs';
import { AppraisalRole } from '../enum/appraisal-role';

@Component({
  selector: 'app-apply',
  templateUrl: './apply.component.html',
  styleUrl: './apply.component.scss'
})
export class ApplyComponent implements OnInit, OnDestroy{
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  currentGrade: number;
  officerGradeType: number;
  staffGradeType: number;
  loading: boolean;
  ActiveSectionForStaff : boolean[] = [true,false,false,false];
  appraisalRole = AppraisalRole;
  constructor(private gradeService: GradeService,
    private authService: AuthService
  ) {
    this.currentGrade = 0;
    this.officerGradeType = environment.officerGradeType;
    this.staffGradeType = environment.staffGradeType; 
    this.loading = false;
  }

  ngOnInit(): void {
    this.loading = true;
    this.subscription.push(
    this.authService.currentUser.subscribe(user => {
      if(user!=null&&user.empId!=null) {
        const empId = parseInt(user.empId);
        this.subscription.push(
          this.gradeService.getByEmpId(empId).subscribe({
          next: response => {
            if(response) {
              this.currentGrade = response.gradeTypeId;
            }
          },
          error: (err) => {
            this.loading = false;
          },
          complete: () => {
            this.loading = false;
          }
        }) 
        )
        
      } else {
        this.loading = false;
      }
    })
    )
    
    this.loading = false;
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }
}
