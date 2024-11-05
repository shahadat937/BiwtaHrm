import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-manage-site-visit',
  templateUrl: './manage-site-visit.component.html',
  styleUrl: './manage-site-visit.component.scss'
})
export class ManageSiteVisitComponent implements OnInit {
  IsUser = true;
  filter = {};
  
  constructor(
    private authService: AuthService
  ) {
    this.authService.currentUser.subscribe(user => {
      let empId = user.empId == null? 0: user.empId
      this.filter = {EmpId: empId};
    }) 
  }

  ngOnInit(): void {
    
  }


}
