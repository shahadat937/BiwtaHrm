import { SubBranch } from './../../basic-setup/model/sub-branch';
import { DepartmentService } from './../../basic-setup/service/department.service';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { SubDepartmentService } from '../../basic-setup/service/sub-department.service';
import { BranchService } from '../../basic-setup/service/branch.service';
import { SubBranchService } from '../../basic-setup/service/sub-branch.service';


@Component({
  selector: 'app-posting',
  templateUrl: './posting.component.html',
  styleUrl: './posting.component.scss'
})
export class PostingComponent implements OnInit, OnDestroy, AfterViewInit {
  //grades: any[] = [];
  editMode: boolean = false;
  departments: SelectedModel[] = [];
  subDepartments:SelectedModel[]=[];
  officeBranchs:SelectedModel[]=[];
  subBranchs:SelectedModel[]=[];

  btnText: string | undefined;
  loading = false;
  @ViewChild('PostingAndTrnsForm', { static: true }) PostingAndTrnsForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo','officeOrderNo','isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public postingOrderInfoService: PostingOrderInfoService,
    private branchServices: BranchService,
    private departmentService:DepartmentService,
    private subDepartmentService:SubDepartmentService,
    private subBranchService:SubBranchService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getAllPostingOrderInfos();
    this.SelectDepartments();
    this.SelectsubDepartments();
    this.SelectBranchs();
    this.SelectSubBranchs();
    this.handleRouteParams();
  }

  SelectDepartments() {
    this.departmentService.getSelectDepartments().subscribe((data) => {
      console.log(data);
      this.departments = data;
    });
  }
  SelectsubDepartments() {
    this.subDepartmentService.getSelectSubDepartment().subscribe((res) => {
      console.log(res);
      this.subDepartments = res;
    });
  }
  SelectBranchs() {
    this.branchServices.getSelectBranch().subscribe((res) => {
      console.log(res);
      this.officeBranchs = res;
    });
  }
  SelectSubBranchs() {
    this.subBranchService.getSelectSubBranchs().subscribe((res) => {
      console.log(res);
      this.subBranchs = res;
    });
  }

  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('postingOrderInfoId');
      if (id) {
        this.btnText = 'Update';
        this.postingOrderInfoService.find(+id).subscribe((res) => {
          this.PostingAndTrnsForm?.form.patchValue(res);
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
    if (this.PostingAndTrnsForm?.form != null) {
      this.PostingAndTrnsForm.form.reset();
      this.PostingAndTrnsForm.form.patchValue({
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
    this.router.navigate(['/transfer/postingOrderInfo']);
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
          this.router.navigate(['/transfer/postingOrderInfo']);
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
