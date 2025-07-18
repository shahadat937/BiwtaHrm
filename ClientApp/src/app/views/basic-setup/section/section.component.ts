import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { SectionService } from '../service/section.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DepartmentService } from '../service/department.service';
import { OfficeService } from '../service/office.service';

@Component({
  selector: 'app-section',
  templateUrl:'./section.component.html',
  styleUrl: './section.component.scss'
})
export class SectionComponent implements OnInit, OnDestroy, AfterViewInit {
  // position = 'top-end';
  visible = false;
  // percentage = 0;
  BtnText: string | undefined;
  btnText: string | undefined;
  headerText: string | undefined;
  buttonIcon: string | undefined;
  offices: SelectedModel[] = [];
  departments: SelectedModel[] = [];
  upperSections: SelectedModel[] = [];
  departmentView = false;
  upperSectionView = false;
  loading = false;
  @ViewChild('SectionForm', { static: true }) SectionForm!: NgForm;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = [
    'slNo', 
    // 'office',
    'department',
    'section',
    'upperSection',
    'sequence',
    'isActive', 
    'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public departmentService: DepartmentService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public officeService : OfficeService,
    public sectionService : SectionService,
  ) {
  }
  ngOnInit(): void {
    this.initaialSectionForm();
    this.getAllSection();
    this.handleRouteParams();
    this.loadOffice();
    this.getAllSelectedDepartments();
  }

  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('sectionId');
      if (id) {
        this.visible = true;
        this.btnText = 'Update';
        this.headerText = "Update Section";
        this.BtnText = " Hide Form";
        this.buttonIcon = "cilTrash";
        this.sectionService.find(+id).subscribe((res) => {
          this.onOfficeAndDepartmentSelect(res.departmentId);
          this.SectionForm?.form.patchValue(res);
        });
      } else {
        this.resetForm();
        this.btnText = 'Submit';
        this.visible = false;
        this.headerText = "Section List"
        this.buttonIcon = "cilPencil";
        this.BtnText = " Add Section";
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
  
  UserFormView() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('sectionId');
      if(id){
        if (this.BtnText == " View Form") {
          this.BtnText = " Hide Form";
          this.buttonIcon = "cilTrash";
          this.headerText = "Update Section";
          this.visible = true;
        }
        else {
          this.BtnText = " View Form";
          this.buttonIcon = "cilPencil";
          this.headerText = "Section List";
          this.visible = false;
        }
      }
      else {
        if (this.BtnText == " Add Section") {
          this.BtnText = " Hide Form";
          this.buttonIcon = "cilTrash";
          this.headerText = "Add New Section";
          this.visible = true;
        }
        else {
          this.BtnText = " Add Section";
          this.buttonIcon = "cilPencil";
          this.headerText = "Section List";
          this.visible = false;
        }
      }
    });
  }

  toggleCollapse() {
    this.handleRouteParams();
    this.visible = true;
  }

  cancelUpdate() {
    this.resetForm();
    this.router.navigate(['/officeSetup/section']);
  }

  initaialSectionForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.sectionService.sections = {
      sectionId: 0,
      sectionName: "",
      sectionNameBangla: "", 
      sectionCode: "", 
      officeId: null,
      departmentId: null,
      upperSectionId: null,
      phone: "", 
      mobile: "", 
      fax: "", 
      email: "", 
      sequence: null,
      remark: "", 
      menuPosition: 0,
      isActive: true,
      officeName: "", 
      departmentName: "", 
      upperSectionName: "",
      showAllDesignation: false,
    };
  }
  resetForm() {
    if (this.SectionForm?.form != null) {
      this.SectionForm.form.reset();
      this.SectionForm.form.patchValue({
        sectionId: 0,
        sectionName: "",
        sectionNameBangla: "", 
        sectionCode: "", 
        officeId: null,
        departmentId: null,
        upperSectionId: null,
        phone: "", 
        mobile: "", 
        fax: "", 
        email: "", 
        sequence: null,
        remark: "", 
        menuPosition: 0,
        isActive: true,
        officeName: "", 
        departmentName: "", 
        upperSectionName: "", 
      });
    }
    this.router.navigate(['/officeSetup/section']);
  }

  getAllSection() {
    this.subscription.push(
    this.sectionService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }
  
  loadOffice() { 
    // this.subscription=
    this.subscription.push(
    this.officeService.selectGetoffice().subscribe((data) => { 
      this.offices = data;
    })
    )
    
  }

  onOfficeSelect(officeId : number){
    this.departmentService.departments.upperDepartmentId = null;
    this.subscription.push(
    this.departmentService.getSelectedDepartmentByOfficeId(+officeId).subscribe((res) => {
      this.departments = res;
      if(res.length>0){
        this.departmentView = true;
      }
      else{
        this.departmentView = false;
      }
    })
    )
    
  }

  
  getAllSelectedDepartments(){
    this.subscription.push(
       this.departmentService.getSelectedAllDepartment().subscribe((res) => {
          this.departments = res;
    })
    )
    
  }
  
  onOfficeAndDepartmentSelect(departmentId : number){
    this.sectionService.sections.upperSectionId = null;
    this.subscription.push(
    this.sectionService.getSectionByOfficeDepartment(+departmentId).subscribe((res) => {
      this.upperSections = res;
      if(res.length>0){
        this.upperSectionView = true;
      }
      else{
        this.upperSectionView = false;
      }
    })
    )
    
  }

  onOfficeSelectGetDepartment(officeId : number){
    if(officeId == null){
      this.getAllSection();
    }
    else {
      this.subscription.push(
      this.departmentService.onOfficeSelectGetDepartment(+officeId).subscribe((res) => {
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.matSort;
      })
      )
      
    }
  }

  
  getSectionPosition(departmentId : any, sectionId : any){
    if(this.btnText == 'Submit'){
      this.sectionService.getSectionPosition(+departmentId, +sectionId).subscribe((res) => {
        this.sectionService.sections.sequence = res;
      })
    }
  }

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.sectionService.cachedData = [];
    const id = form.value.sectionId;
    const action$ = id
      ? this.sectionService.update(id, form.value)
      : this.sectionService.submit(form.value);

    this.subscription.push(
    action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllSection();
        this.resetForm();
        this.router.navigate(['/officeSetup/section']);
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
          this.sectionService.delete(element.sectionId).subscribe(
            (res : any) => {
              if(res.success){
                const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
                this.dataSource.paginator = this.paginator;
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
            },
          );
        }
      })
    )
    
  }
}

