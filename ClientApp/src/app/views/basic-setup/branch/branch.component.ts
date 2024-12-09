import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { BranchService } from './../service/branch.service';

import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { OfficeService } from '../service/office.service';

@Component({
  selector: 'app-branch',
  templateUrl: './branch.component.html',
  styleUrl: './branch.component.scss',
})
export class BranchComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  BtnText: string | undefined;
  btnText: string | undefined;
  headerText : string | undefined;
  buttonIcon : string | undefined;
  offices: SelectedModel[] = [];
  branches: SelectedModel[] = [];
  upperBranchView = false;
  @ViewChild('BranchForm', { static: true }) BranchForm!: NgForm;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = ['slNo', 'branchName','branchNameBangla', 'isActive', 'Action'];
  loading = false;

  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public branchService: BranchService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public officeService : OfficeService,
  ) {}
  ngOnInit(): void {
    this.getALlBranchs();
    this.handleRouteParams();
    this.loadOffice();
  }
  handleRouteParams() {
    this.subscription.push(
    this.route.paramMap.subscribe((params) => {
      const id = params.get('branchId');
      if (id) {
        this.visible = true;
        this.btnText = 'Update';
        this.headerText = "Update Branch";
        this.BtnText = " Hide Form";
        this.buttonIcon = "cilTrash";
        this.branchService.getById(+id).subscribe((res) => {
          this.onOfficeSelect(res.officeId);
          this.BranchForm?.form.patchValue(res);
        });
      } else {
        this.resetForm();
        this.btnText = 'Submit';
        this.visible = false;
        this.headerText = "Branch List"
        this.buttonIcon = "cilPencil";
        this.BtnText = " Add Branch";
      }
    })
    )
    
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


  UserFormView(){
    if(this.BtnText == " Add Branch"){
      this.BtnText = " Hide Form";
      this.buttonIcon = "cilTrash";
      this.headerText = "Add New Branch";
      this.visible = true;
    }
    else {
      this.BtnText = " Add Branch";
      this.buttonIcon = "cilPencil";
      this.headerText = "Branch List";
      this.visible = false;
    }
  }
  
  toggleCollapse(){
    this.handleRouteParams();
    this.headerText = "Update User";
    this.visible = true;
  }

  cancelUpdate(){
    this.resetForm();
    this.router.navigate(['/bankInfoSetup/officeBranch']);
  }

  initaialBranch(form?: NgForm) {
    if (form != null) form.resetForm();
    this.branchService.branchs = {
      branchId: 0,
      branchName: "",
      branchNameBangla: "",
      branchCode: "",
      officeId: null,
      upperBranchId: null,
      phone: "",
      mobile: "",
      fax: "",
      email: "",
      sequence: null,
      remark: "",
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    if (this.BranchForm?.form != null) {
      this.BranchForm.form.reset();
      this.BranchForm.form.patchValue({
        branchId: 0,
        branchName: "",
        branchNameBangla: "",
        branchCode: "",
        officeId: null,
        upperBranchId: null,
        phone: "",
        mobile: "",
        fax: "",
        email: "",
        sequence: null,
        remark: "",
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bankInfoSetup/officeBranch']);
  }


  loadOffice() { 
    this.subscription.push(
    this.officeService.selectGetoffice().subscribe((data) => { 
      this.offices = data;
    })
    )
    
  }

  onOfficeSelect(officeId:number){
    this.subscription.push(
    this.branchService.getBranchByOfficeId(+officeId).subscribe((res) => {
      this.branches = res;
      if(res.length>0){
        this.upperBranchView = true;
      }
      else{
        this.upperBranchView = false;
      }
    })
    )
    
  }

  getALlBranchs() {
    this.subscription.push(
    this.branchService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }

  
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.branchService.cachedData = [];
    const id = form.value.branchId;
    const action$ = id
      ? this.branchService.update(id, form.value)
      : this.branchService.submit(form.value);

      this.subscription.push(
      action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlBranchs();
        this.resetForm();
        this.router.navigate(['/bankInfoSetup/officeBranch']);
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
          this.subscription.push(
          this.branchService.delete(element.officeBranchId).subscribe(
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
            }
          )
          )
          
        }
      })
    )
    
  }
}
