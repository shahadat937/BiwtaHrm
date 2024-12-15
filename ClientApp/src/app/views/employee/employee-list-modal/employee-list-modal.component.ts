import { Component, ElementRef, EventEmitter, HostListener, OnDestroy, OnInit, Output, Renderer2, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Table } from 'primeng/table';
import { Subscription } from 'rxjs';
import { BasicInfoModule } from '../model/basic-info.module';
import { UserModule } from '../../usermanagement/model/user.module';
import { EmpBasicInfoService } from '../service/emp-basic-info.service';
import { EmpPhotoSignService } from '../service/emp-photo-sign.service';
import { PaginatorModel } from 'src/app/core/models/paginator-model';

@Component({
  selector: 'app-employee-list-modal',
  templateUrl: './employee-list-modal.component.html',
  styleUrl: './employee-list-modal.component.scss'
})
export class EmployeeListModalComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  modalOpened: boolean = false;
  @Output() employeeSelected = new EventEmitter<string>();
  employees: BasicInfoModule[] = [];
  departments: any[] = [];
  sections!: any[];
  imageLinkUrl: any;
  defaultImage: any;
  maleImage: any;
  femaleImage: any;
  loading: boolean = true;
  totalRecords: number = 0;
  dataSource = new MatTableDataSource<any>();
  @ViewChild('dt') dt: Table | undefined;
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  userStatus : string = '';
  loadingMap: { [key: number]: boolean } = {};
  userForm : UserModule;
  selectedEmpId!: number;
  pagination: PaginatorModel = new PaginatorModel();

  constructor(
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2,
    public empBasicInfoService: EmpBasicInfoService,
    public empPhotoSign: EmpPhotoSignService,
  ) {
    this.userForm = new UserModule;
  }

  ngOnInit(): void {
    this.getAllEmpBasicInfo(this.pagination);
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  getAllEmpBasicInfo(queryParams: any) {
    // this.subscription = 
    this.subscription.push(
      this.empBasicInfoService.getAllPagination(queryParams).subscribe((employees: any) => {
      this.totalRecords = employees.totalItemsCount;
      this.employees = employees.items;
      this.loading = false;

      // this.departments = [...new Set(employees
      //   .map(emp => emp.departmentName)
      //   .filter(departmentName => departmentName !== null && departmentName.trim() !== '')
      // )].map(department => ({ name: department }));
      
      // this.sections = [...new Set(employees
      //   .map(emp => emp.sectionName)
      //   .filter(sectionName => sectionName !== null && sectionName.trim() !== '')
      // )].map(section => ({ name: section }));
    })
    )
    

    this.imageLinkUrl = this.empPhotoSign.imageUrl + '/EmpPhoto';
    this.defaultImage = this.empPhotoSign.imageUrl + '/EmpPhoto/default.jpg';
    this.maleImage = this.empPhotoSign.imageUrl + '/EmpPhoto/default_Male.jpg';
    this.femaleImage = this.empPhotoSign.imageUrl + '/EmpPhoto/default_Female.jpg';
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.toLowerCase();
    this.pagination.pageIndex = 1;
    this.pagination.searchText = filterValue;
    this.getAllEmpBasicInfo(this.pagination);
  }

  onPageChange(event: any){
    this.pagination.pageSize = event.rows;
    this.pagination.pageIndex = (event.first / event.rows) + 1;
    this.getAllEmpBasicInfo(this.pagination);
  }

  onGlobalFilter(event: Event) {
    const inputValue = (event.target as HTMLInputElement).value;
    this.dt!.filterGlobal(inputValue, 'contains');
  }
  onSelect(selectedValue: string | null) {
    if (!selectedValue) {
      this.dt!.filterGlobal('', 'contains');
    } else {
      this.dt!.filterGlobal(selectedValue, 'contains');
    }
  }

  onSelectEmployee(idCardNo: string) {
    this.employeeSelected.emit(idCardNo);
    this.closeModal();
  }

  closeModal(): void {
    this.bsModalRef.hide();
  }
  
  @HostListener('document:click', ['$event'])
  onClickOutside(event: MouseEvent): void {
    if (this.modalOpened) {
      const modalElement = this.el.nativeElement.querySelector('.modal-content');
      if (modalElement && !modalElement.contains(event.target as Node)) {
        this.shakeModal();
      }
    }
  }

  shakeModal(): void {
    const modalElement = this.el.nativeElement.querySelector('.modal-content');
    if (modalElement) {
      this.renderer.addClass(modalElement, 'shake');
      setTimeout(() => {
        this.renderer.removeClass(modalElement, 'shake');
      }, 500);
    }
  }
}
