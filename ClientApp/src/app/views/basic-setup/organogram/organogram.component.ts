import { AfterViewInit, Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { OrganogramService } from '../service/organogram.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-organogram',
  templateUrl: './organogram.component.html',
  styleUrl: './organogram.component.scss'
})
export class OrganogramComponent implements OnInit, OnDestroy  {

  subscription: Subscription = new Subscription();
  organograms:any[] = [];

  constructor(
    public organogramService: OrganogramService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
  ) {
  }
  ngOnInit(): void {
    this.getOrganogram();
  }
  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getOrganogram(){
    this.subscription=this.organogramService.getOrganogramNamesOnly().subscribe((data) => { 
      this.organograms = data;
      console.log(this.organograms)
    });
  }
}

// export class DynamicFlatNode {
//   constructor(
//     public item: string,
//     public level = 1,
//     public expandable = false,
//     public isLoading = false
//   ) {}
// }
// @Injectable({ providedIn: 'root' })
// export class DynamicDatabase {

//   subscription: Subscription = new Subscription();
//   organograms:any[] = [];

//   constructor(
//     public organogramService: OrganogramService,
//   ) {
//   }

//   getingOrganogram(){
//     this.subscription=this.organogramService.getOrganogram().subscribe((data) => { 
//       this.organograms = data;
      
//       const dataMap = new Map<any, any[]>(
//         [
//         ['Fruits', ['Apple', 'Orange', 'Banana']],
//         ['Vegetables', ['Tomato', 'Potato', 'Onion']],
//         ['Apple', ['Fuji', 'Macintosh']],
//         ['Onion', ['Yellow', 'White', 'Purple']],
//         ['Onion', ['Yellow', 'White', 'Purple']],
//       ]);
//     });
//   }

// }
