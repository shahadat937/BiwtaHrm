import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { ReportingService } from '../service/reporting.service';
import { DepartmentService } from 'src/app/views/basic-setup/service/department.service';
import { SectionService } from 'src/app/views/basic-setup/service/section.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { PaginatorModel } from 'src/app/core/models/paginator-model';
import { EmployeeListReporting } from '../models/employee-list-reporting';
import { EmpPhotoSignService } from '../../employee/service/emp-photo-sign.service';
import { LeaveReportingService } from '../service/leave-reporting.service';
import { DesignationService } from '../../basic-setup/service/designation.service';
import { DesignationSetupService } from '../../basic-setup/service/designation-setup.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EmployeeListModalComponent } from '../../employee/employee-list-modal/employee-list-modal.component';
import { ToastrService } from 'ngx-toastr';
import { EmpTransferPostingService } from '../../transferPosting/service/emp-transfer-posting.service';
import { cilSearch } from '@coreui/icons';

@Component({
  selector: 'app-leave-reporting',
  templateUrl: './leave-reporting.component.html',
  styleUrl: './leave-reporting.component.scss'
})
export class LeaveReportingComponent implements OnInit, OnDestroy {

  subscription: Subscription[] = [];
  departments: SelectedModel[] = [];
  sections: SelectedModel[] = [];
  designations: SelectedModel[] = [];
  leaveTypes: SelectedModel[] = [];

  displayedColumns: string[] = [
    // 'slNo',
    'employee',
    'department/section',
    'designation',
    'leaveType',
    'fromDate',
    'toDate',
  ];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  pagination: PaginatorModel = new PaginatorModel();
  departmentId: number = 0;
  sectionId: number = 0;
  designationId: number = 0;
  leaveTypeId: number = 0;
  employeeId: number = 0;
  employeeIdCard: any;
  empName: any;
  departmentName: string = "";
  sectionName: string = "";
  designationName: string = "";
  leaveTypeName: string = "";
  totalEmployee: number = 0;
  fromDate: any = null;
  toDate: any = null;
  biwtaLogo: string = `${this.empPhotoSignService.imageUrl}TempleteImage/biwta-logo.png`;
  employees!: EmployeeListReporting[];
  constructor(
    public leaveReportingService: LeaveReportingService,
    public departmentService: DepartmentService,
    public sectionService: SectionService,
    public empPhotoSignService: EmpPhotoSignService,
    public designationService: DesignationService,
    public designationSetupService: DesignationSetupService,
    private modalService: BsModalService,
    public empTransferPostingService: EmpTransferPostingService,
    private toastr: ToastrService,
  ) {

  }

  icons = { cilSearch };

  ngOnInit(): void {
    this.getAllSelectedDepartments();
    this.getSelectDesignationSetupName();
    this.getSelectLeaveType();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs => subs.unsubscribe());
    }
  }
  getAllSelectedDepartments() {
    this.subscription.push(
      this.departmentService.getSelectedAllDepartment().subscribe((res) => {
        this.departments = res;
      })
    )
  }
  onDepartmentSelect(departmentId: number) {
    this.departmentName = "";
    this.sectionName = "";
    if (this.paginator) {
      this.paginator.firstPage();
    }
    this.sectionId = 0;
    this.sectionService.getSectionByOfficeDepartment(+departmentId).subscribe((res) => {
      this.sections = res;
    });
    this.departmentService.getById(+departmentId).subscribe((res) => {
      if (res) {
        this.departmentName = res.departmentName;
      }
    });
    this.getLeaveReportingResult(this.pagination);
  }

  onSectionSelect() {
    if (this.paginator) {
      this.paginator.firstPage();
    }
    this.sectionName = "";
    this.sectionService.find(this.sectionId).subscribe((res) => {
      if (res) {
        this.sectionName = res.sectionName;
      }
    });
    this.getLeaveReportingResult(this.pagination);
  }

  onDesignationSelect() {
    if (this.paginator) {
      this.paginator.firstPage();
    }
    this.designationName = "";
    this.designationSetupService.find(this.designationId).subscribe((res) => {
      if (res) {
        this.designationName = res.name;
      }
    });
    this.getLeaveReportingResult(this.pagination);
  }

  onLeaveTypeSelect() {
    if (this.paginator) {
      this.paginator.firstPage();
    }
    this.leaveTypeName = "";
    if(this.leaveTypeId != 0){
      this.leaveReportingService.getLeaveTypeById(this.leaveTypeId).subscribe((res) => {
      if (res) {
        this.leaveTypeName = res.leaveTypeName;
      }
    });
    }
    this.getLeaveReportingResult(this.pagination);
  }

  onDateChange() {
    if(this.fromDate && this.toDate){
      if (this.paginator) {
      this.paginator.firstPage();
      }
      this.getLeaveReportingResult(this.pagination);
    }
  }

  getSelectDesignationSetupName() {
    this.subscription.push(
      this.designationService.getSelectDesignationSetupName().subscribe((data) => {
        this.designations = data;
      })
    )
  }

  getSelectLeaveType() {
    this.subscription.push(
      this.leaveReportingService.getSelectedLeaveType().subscribe((data) => {
        this.leaveTypes = data;
      })
    )
  }

  onPageChange(event: any) {
    this.pagination.pageSize = event.pageSize;
    this.pagination.pageIndex = event.pageIndex + 1;
    this.getLeaveReportingResult(this.pagination);
  }

  getLeaveReportingResult(queryParams: any) {
    this.subscription.push(
      this.leaveReportingService.getLeaveReporting(queryParams, this.employeeId, this.departmentId, this.sectionId, this.designationId, this.leaveTypeId, this.fromDate, this.toDate).subscribe((res: any) => {
        this.dataSource.data = res.items;
        this.pagination.length = res.totalItemsCount;
      })
    )
  }


  EmployeeListModal() {
    const modalRef: BsModalRef = this.modalService.show(EmployeeListModalComponent, { backdrop: 'static', class: 'modal-xl' });

    modalRef.content.employeeSelected.subscribe((idCardNo: string) => {
      if (idCardNo) {
        this.getEmpInfoByIdCardNo(idCardNo);
      }
    });
  }

  getEmpInfoByIdCardNo(idCardNo: string) {
    if (idCardNo) {
      this.subscription.push(
        this.empTransferPostingService.getEmpBasicInfoByIdCardNo(idCardNo).subscribe((res) => {
          if (res) {
            this.employeeId = res.id;
            this.employeeIdCard = res.idCardNo;
            this.empName = res.firstName + " " + res.lastName;
            if (this.paginator) {
              this.paginator.firstPage();
            }
            this.getLeaveReportingResult(this.pagination);
          }
          else {
            this.toastr.warning('', 'Invalid Employee PMIS No', {
              positionClass: 'toast-top-right',
            });
            this.employeeId = 0;
            this.employeeIdCard = null;
          }
        })
      )
    }
    else {
      this.employeeId = 0;
      this.employeeIdCard = null;
      if (this.paginator) {
        this.paginator.firstPage();
      }
      this.getLeaveReportingResult(this.pagination);
    }
  }


  printSection() {
    // Get the basic information and the specific section to print
    const tableData = document.getElementById('tableData')?.innerHTML;
    const heading = document.getElementById('report_heading')?.innerHTML;

    // Create a new window for printing
    const printWindow = window.open('', 'blank', 'width=800,height=600');
    printWindow?.document.write(`
      <html>
        <head>
          <title>Leave Report</title>
          <style>
            @media print {
              @page {
                margin-top: 0;
                padding-top: 40px;
              }
              header {
                display: none !important;
              }
            }
            table { border-collapse: collapse; text-align: left; width: 100%}
            th, td {border: 1px solid #000; padding: 5px; font-size: 13px;}
            c-col { 
              float: left; 
            }
            c-card-footer {display: none;}
            c-card-header {text-align: end; margin-bottom: 10px;}
            .joinDate {width: 70px;}
            .group-header {background: #add8e6;}
          </style>
        </head>
        <body>
          <div>${heading}</div>
          <div>${tableData}</div>
        </body>
      </html>
    `);
    printWindow?.document.close();
    printWindow?.print();
  }
}
