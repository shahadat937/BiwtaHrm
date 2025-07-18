import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { DesignationService } from '../service/designation.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { OfficeService } from '../service/office.service';
import { DepartmentService } from '../service/department.service';
import { SectionService } from '../service/section.service';
@Component({
  selector: 'app-designation',
  templateUrl: './designation.component.html',
  styleUrl: './designation.component.scss',
})
export class DesignationComponent implements OnInit, OnDestroy, AfterViewInit {
  
  visible = false;
  percentage = 0;
  BtnText: string | undefined;
  btnText: string | undefined;
  headerText: string | undefined;
  buttonIcon: string | undefined;
  offices: SelectedModel[] = [];
  departments: SelectedModel[] = [];
  sections: SelectedModel[] = [];
  designations: SelectedModel[] = [];
  upperDepartmentView = false;
  sectionView = false;
  loading = false;
  @ViewChild('DesignationForm', { static: true }) DesignationForm!: NgForm;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = [
    'slNo',
    // 'officeName',
    'departmentName',
    'sectionName',
    'designationName',
    // 'designationNameBangla',
    'menuPosition',
    'Action',
  ];

  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public designationService: DesignationService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public officeService:OfficeService,
    public departmentService:DepartmentService,
    public sectionService : SectionService,
  ) {
    //  const id = this.route.snapshot.paramMap.get('bloodGroupId');
  }
  ngOnInit(): void {
    this.initaialDesignation();
    this.getALlDesignations();
    this.handleRouteParams();
    this.loadOffice();
    this.getAllSelectedDepartments();
    this.getSelectDesignationSetupName();
  }
  handleRouteParams() {
    this.subscription.push(
    this.route.paramMap.subscribe((params) => {
      const id = params.get('designationId');
      if (id) {
        this.visible = true;
        this.btnText = 'Update';
        this.headerText = "Update Designation";
        this.BtnText = " Hide Form";
        this.buttonIcon = "cilTrash";
        this.subscription.push(
        this.designationService.find(+id).subscribe((res) => {
          // this.onOfficeSelect(res.officeId);
          this.onOfficeAndDepartmentSelect(res.departmentId);
          this.DesignationForm?.form.patchValue(res);
        })
        )
        
      } else {
        this.resetForm();
        this.btnText = 'Submit';
        this.visible = false;
        this.headerText = "Designation List"
        this.buttonIcon = "cilPencil";
        this.BtnText = " Add Designation";
        this.designationService.designation.officeId = null;
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
  
  UserFormView() {
    this.subscription.push(
    this.route.paramMap.subscribe((params) => {
      const id = params.get('designationId');
      if(id){
        if (this.BtnText == " View Form") {
          this.BtnText = " Hide Form";
          this.buttonIcon = "cilTrash";
          this.headerText = "Update Designation";
          this.visible = true;
        }
        else {
          this.BtnText = " View Form";
          this.buttonIcon = "cilPencil";
          this.headerText = "Designation List";
          this.visible = false;
        }
      }
      else {
        if (this.BtnText == " Add Designation") {
          this.BtnText = " Hide Form";
          this.buttonIcon = "cilTrash";
          this.headerText = "Add New Designation";
          this.visible = true;
        }
        else {
          this.BtnText = " Add Designation";
          this.buttonIcon = "cilPencil";
          this.headerText = "Designation List";
          this.visible = false;
        }
      }
    })
    )
    
  }
  toggleCollapse() {
    this.handleRouteParams();
    this.visible = true;
  }
  cancelUpdate() {
    this.resetForm();
    this.router.navigate(['/officeSetup/designation']);
  }
  initaialDesignation(form?: NgForm) {
    if (form != null) form.resetForm();
    this.designationService.designation = {
      designationId: 0,
      designationName: '',
      designationSetupId: null,
      officeId: null,
      departmentId: null,
      sectionId: null,
      remark: '',
      createCount: 1,
      menuPosition: 0,
      isActive: true,
      officeName: "",
      departmentName: "",
      sectionName: "",
    };
  }
  resetForm() {
    if (this.DesignationForm?.form != null) {
      this.DesignationForm.form.reset();
      this.DesignationForm.form.patchValue({
        designationId: 0,
        designationName: '',
        designationSetupId: null,
        officeId: null,
        departmentId: null,
        sectionId: null,
        remark: '',
        menuPosition: 0,
        createCount: 1,
        isActive: true,
        officeName: "",
        departmentName: "",
        sectionName: "",
      });
    }
  }
  getALlDesignations() {
    // this.subscription = 
    this.subscription.push(
    this.designationService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }

  loadOffice() { 
    this.subscription.push(
    this.officeService.selectGetoffice().subscribe((data) => { 
      this.offices = data;
    })
    )
   
  }
  
  onOfficeSelect(officeId : number){
    this.designationService.designation.departmentId = null;
    this.subscription.push(
    this.departmentService.getSelectedDepartmentByOfficeId(+officeId).subscribe((res) => {
      this.departments = res;
      if(res.length>0){
        this.upperDepartmentView = true;
      }
      else{
        this.upperDepartmentView = false;
      }
    })
    )
    
  }

  getSelectDesignationSetupName() { 
    this.subscription.push(
    this.designationService.getSelectDesignationSetupName().subscribe((data) => { 
      this.designations = data;
    })
    )
    
  }
  
  onOfficeSelectGetDesignation(officeId : number){
    if(officeId == null){
      this.getALlDesignations();
    }
    else {
      this.subscription.push(
      this.designationService.getDesignationsByOfficeId(+officeId).subscribe((item) => {
        this.dataSource = new MatTableDataSource(item);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.matSort;
      })
      )
      
    }
  }
  
  getAllSelectedDepartments(){
    this.subscription.push(
    this.departmentService.getSelectedAllDepartment().subscribe((res) => {
        this.departments = res;
    })
    )
   
  }
  
  onDepartmentSelectGetDesignation(officeId : number, departmentId : number){
    if(departmentId == null){
      this.onOfficeSelectGetDesignation(officeId);
    }
    else {
      this.subscription.push(
      this.designationService.getDesignationsByOfficeIdAndDepartmentId(officeId,departmentId).subscribe((item) => {
        this.dataSource = new MatTableDataSource(item);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.matSort;
      })
      )
      
    }
  }

  
  onOfficeAndDepartmentSelect(departmentId : number){
    this.designationService.designation.sectionId = null;
    this.subscription.push(
    this.sectionService.getSectionByOfficeDepartment(+departmentId).subscribe((res) => {
      this.sections = res;
      if(res.length>0){
        this.sectionView = true;
      }
      else{
        this.sectionView = false;
      }
    })
    )
    
  }

  getDesignationPosition(departmentId : any, sectionId : any){
    if(this.btnText == 'Submit'){
      this.subscription.push(
        this.designationService.getDesignationPosition(+departmentId, +sectionId).subscribe((res) => {
        this.designationService.designation.menuPosition = res;
      })
      )
      
    }
  }


  onSubmit(form: NgForm): void {
    this.loading = true;
    this.designationService.cachedData = [];
    const id = form.value.designationId;
    const action$ = id
      ? this.designationService.update(id, form.value)
      : this.designationService.submit(form.value);
      this.subscription.push(
      action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlDesignations();
        this.resetForm();
        this.router.navigate(['/officeSetup/designation']);
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
          this.designationService.delete(element.designationId).subscribe(
            (res : any) => {
              if(res.success){
                const index = this.dataSource.data.indexOf(element);
                if (index !== -1) {
                  this.dataSource.data.splice(index, 1);
                  this.dataSource = new MatTableDataSource(this.dataSource.data);
                  this.dataSource.paginator = this.paginator
                }
                this.toastr.success('Delete sucessfully ! ', ` `, {
                  positionClass: 'toast-top-right',
                });
              }
              else {
                this.toastr.warning('', `${res.message}`, {
                positionClass: 'toast-top-right',
              });
              }
            }
          )
          )
          
        }
      })
    )
    
  }
}