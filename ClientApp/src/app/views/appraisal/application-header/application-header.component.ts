import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
import { EmpBasicInfoService } from '../../employee/service/emp-basic-info.service';

@Component({
  selector: 'app-application-header',
  templateUrl: './application-header.component.html',
  styleUrl: './application-header.component.scss'
})
export class ApplicationHeaderComponent implements OnInit, OnChanges, OnDestroy {
  companyTitle :string = "Bangladesh Inland Water Transport Authority"
  address = "141-143, Motijheel Commerial Area, Dhaka-1000"

  @Input()
  formName: string; 

  @Input()
  from: string;

  @Input()
  to: string;

  @Input()
  department: string

  @Input()
  empId: number;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  constructor(
    private empService: EmpBasicInfoService
  ) {
    this.companyTitle = environment.companyTitle
    this.address = environment.companyAddress
    this.department = ""
    this.empId = 0
    this.from = "";
    this.to = "";

    this.formName = "Annual Confidential Report Of Officer"
  }

  ngOnInit(): void {
    if(this.empId!=0) {
      this.subscription.push(
        this.empService.findByEmpId(this.empId).subscribe({
        next: response => {
          this.department = response.departmentName
        }
      })
      )
      
    } 
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes['empId']) {
      if(this.empId!=0) {
        this.subscription.push(
          this.empService.findByEmpId(this.empId).subscribe({
          next: response => {
            this.department = response.departmentName
          }
        })
        )
        
      } else {
        this.department = "";
      } 
    }
  }


  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }


  
}
