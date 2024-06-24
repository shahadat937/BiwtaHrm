import { getStyle } from '@coreui/utils';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Employee } from '../../basic-setup/model/employees';
import { EmployeesService } from '../service/employees.service';
import { EmpModalComponent } from '../emp-modal/emp-modal.component';
import { DesignationService } from '../../basic-setup/service/designation.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DepartmentService } from '../../basic-setup/service/department.service';

@Component({
  selector: 'app-transfer-posting-history',
  templateUrl: './transfer-posting-history.component.html',
  styleUrl: './transfer-posting-history.component.scss'
})
export class TransferPostingHistoryComponent implements OnInit, OnDestroy, AfterViewInit {
  postingOrderInfoId: number | null = null;
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('TransferPostingHistoryForm', { static: true }) TransferPostingHistoryForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'officeOrderNo', 'officeOrderDate','transferSection','releaseType','isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  designations: SelectedModel[] = [];
  departments: SelectedModel[] = [];
  @Input() employeeSelected = new EventEmitter<Employee>();

  constructor(
    private departmentService: DepartmentService,
    public designationService: DesignationService,
    public postingOrderInfoService: PostingOrderInfoService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    public employeeService: EmployeesService,
  ) {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('postingOrderInfoId');
      if (id) {
        this.btnText = 'Update';
        this.postingOrderInfoService.find(+id).subscribe((res) => {
          this.TransferPostingHistoryForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  ngOnInit(): void {
    this.loadDepartment();
    this.loadDesignation();
    this.route.paramMap.subscribe(params => {
      this.postingOrderInfoId = +params.get('postingOrderInfoId')!;
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
  toggleToast() {
    this.visible = !this.visible;
  }
  selectEmployee(employee: Employee) {
    this.employeeSelected.emit(employee);
  }
  openModal(): void {
    const modalRef: BsModalRef = this.modalService.show(EmpModalComponent);
    modalRef.content?.employeeSelected.subscribe((selectedEmployee: Employee) => {
      this.handleEmployeeSelection(selectedEmployee);
      console.log(selectedEmployee)
    });
  }
  handleEmployeeSelection(employee: Employee) {
    this.employeeService.demoEmployee = employee;


  }
  
  loadDepartment() { 
    this.subscription=this.departmentService.getSelectDepartments().subscribe((data) => { 
      this.departments = data;
    });
  }

  onDepartmentSelect(departmentId : number){
    this.designationService.getDesignationByDepartmentId(+departmentId).subscribe((res) => {
      this.designations = res;

    });
  }
  loadDesignation() { 
    this.subscription=this.designationService.selectDesignation().subscribe((data) => { 
      this.designations = data;
    });
  }
  onDesignationSelect(designationId:number){
    this.designationService.getDesignationByDepartmentId(+designationId).subscribe((res)=>{
      this.designations
    })
  }
  // getemployee() {
  //   this.subscription = this.employeeService.getEmployees().subscribe((item) => {
  //     this.dataSource = new MatTableDataSource(item);
  //     this.dataSource.paginator = this.paginator;
  //     this.dataSource.sort = this.matSort;
  //   });
  // }
  resetForm():void{
    this.TransferPostingHistoryForm
  }
  onSubmit(form: NgForm): void {
    this.postingOrderInfoService.cachedData = [];
    const id = form.value.wardId;
    //console.log(form.value)
    const action$ = id
      ? this.postingOrderInfoService.update(id, form.value)
      : this.postingOrderInfoService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? 'Update' : 'Successfully';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      //  this.getemployee();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/ward']);
        }
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
    });
  }
 


}

