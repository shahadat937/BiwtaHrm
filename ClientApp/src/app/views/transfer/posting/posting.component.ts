import { PostingOrderInfoService } from './../../basic-setup/service/posting-order-info.service';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { PositioningService } from 'ngx-bootstrap/positioning';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-posting',
  templateUrl: './posting.component.html',
  styleUrl: './posting.component.scss'
})
export class PostingComponent implements OnInit, OnDestroy, AfterViewInit {
  //grades: any[] = [];
  editMode: boolean = false;
  //countrys: SelectedModel[] = [];

  btnText: string | undefined;
  loading = false;
  @ViewChild('PostingForm', { static: true }) PostingForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo','officeOrderNo','officeOrderDate', 'orderOfficeBy','transferSection','releaseType','isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public postingOrderInfoService: PostingOrderInfoService,
    //private countryService: CountryService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getAllPostingOrderInfos();
    //this.SelectModelCountry();
    //this.loaddivisions();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('postingOrderInfoId');
      if (id) {
        this.btnText = 'Update';
        this.postingOrderInfoService.find(+id).subscribe((res) => {
          this.PostingForm?.form.patchValue(res);
        });
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
      this.subscription.unsubscribe();
    }
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  initaialPostingOrderInfo(form?: NgForm) {
    if (form != null) form.resetForm();
    this.postingOrderInfoService.postingOrderInfos = {

      postingOrderInfoId:0,
      empId:0,
      departmentId:0,
      subBranchId:0,
      subDepartmentId:0,
      officeBranchId:0,
      officeOrderNo:"",
      officeOrderDate:new Date(),
      orderOfficeBy:"",
      transferSection:"",
      releaseType:"",
      menuPosition: 0,
      isActive: true

     };
  }
  resetForm() {
    if (this.PostingForm?.form != null) {
      this.PostingForm.form.reset();
      this.PostingForm.form.patchValue({
        postingOrderInfoId:0,
        empId:0,
        departmentId:0,
        subBranchId:0,
        subDepartmentId:0,
        officeBranchId:0,
        officeOrderNo:"",
        officeOrderDate:new Date(),
        orderOfficeBy:"",
        transferSection:"",
        releaseType:"",
        menuPosition: 0,
        isActive: true
      });
    }
    this.router.navigate(['/bascisetup/postingOrderInfo']);
  }
  getAllPostingOrderInfos() {
    this.subscription = this.postingOrderInfoService.getAll().subscribe((item) => {
      console.log(item);
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
 
 
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.postingOrderInfoService.cachedData = [];
    const id = form.value.divisionId;
    const action$ = id
      ? this.postingOrderInfoService.update(id, form.value)
      : this.postingOrderInfoService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllPostingOrderInfos();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/postingOrderInfo']);
        }
        this.loading = false;
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
      this.loading = false;
    });
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.postingOrderInfoService.delete(element.postingOrderInfoId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
            },
            (err) => {
              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
              console.log(err);
            }
          );
        }
      });
  }
}
