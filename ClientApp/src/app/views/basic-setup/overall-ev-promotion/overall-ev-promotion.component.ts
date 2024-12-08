import { Overall_EV_PromotionService } from './../service/Overall_EV_Promotion.service';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-overall-ev-promotion',
  templateUrl: './overall-ev-promotion.component.html',
  styleUrl: './overall-ev-promotion.component.scss'
})
export class OverallEVPromotionComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  loading = false;
  @ViewChild('Overall_EV_PromotionForm', { static: true }) Overall_EV_PromotionForm!: NgForm;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = ['slNo', 'overallEVPromotionName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public overall_EV_PromotionService: Overall_EV_PromotionService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    //  const id = this.route.snapshot.paramMap.get('bloodGroupId');
  }
  ngOnInit(): void {
    this.getAllOverall_EV_Promotions();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('overallEVPromotionId');
      if (id) {
        this.btnText = 'Update';
        this.subscription.push(
          this.overall_EV_PromotionService.find(+id).subscribe((res) => {
          this.Overall_EV_PromotionForm?.form.patchValue(res);
        })
        )
        
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;
  }
  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  initaialOverall_EV_Promotion(form?: NgForm) {
    if (form != null) form.resetForm();
    this.overall_EV_PromotionService.overall_EV_Promotions = {
      overallEVPromotionId: 0,
      overallEVPromotionName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.Overall_EV_PromotionForm?.form != null) {
      this.Overall_EV_PromotionForm.form.reset();
      this.Overall_EV_PromotionForm.form.patchValue({
        overallEVPromotionId: 0,
        overallEVPromotionName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bascisetup/overall_EV_Promotion']);

  }

  getAllOverall_EV_Promotions() {
    this.subscription.push(
    this.overall_EV_PromotionService.getAll().subscribe((item) => {
      //console.log(item);
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.overall_EV_PromotionService.cachedData = [];
    const id = form.value.overallEVPromotionId;
    const action$ = id
      ? this.overall_EV_PromotionService.update(id, form.value)
      : this.overall_EV_PromotionService.submit(form.value);

      this.subscription.push(
      action$.subscribe((response: any) => {
      if (response.success) {
         //const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllOverall_EV_Promotions();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/overall_EV_Promotion']);
        }
        this.loading = false;
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
      this.loading = false;
    })
      )
   
  }
  delete(element: any) {
    this.subscription.push(
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.overall_EV_PromotionService.delete(element.overallEVPromotionId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
              this.toastr.success('Delete sucessfully ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            },
            (err) => {
              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
              console.log(err);
            }
          );
        }
      })
    )
    
  }
}