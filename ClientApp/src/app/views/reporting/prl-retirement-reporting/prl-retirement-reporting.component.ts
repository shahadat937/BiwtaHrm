import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { PrlRetirementReporting } from '../models/prl-retirement-reporting';
import { PrlRetirmentService } from '../service/prl-retirment.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { cilSearch } from '@coreui/icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from '../../../core/models/selectedModel';
import { DesignationSetupService } from '../../basic-setup/service/designation-setup.service';
import { DesignationService } from '../../basic-setup/service/designation.service';
import { EmployeeListModalComponent } from '../../employee/employee-list-modal/employee-list-modal.component';
import { EmpPhotoSignService } from '../../employee/service/emp-photo-sign.service';
import { EmpTransferPostingService } from '../../transferPosting/service/emp-transfer-posting.service';

import { DepartmentService } from 'src/app/views/basic-setup/service/department.service';
import { SectionService } from 'src/app/views/basic-setup/service/section.service';
import { PaginatorModel } from 'src/app/core/models/paginator-model';
import { FinancialYearService } from '../../basic-setup/service/financial-year.service';

@Component({
  selector: 'app-prl-retirement-reporting',
  templateUrl: './prl-retirement-reporting.component.html',
  styleUrl: './prl-retirement-reporting.component.scss'
})
export class PrlRetirementReportingComponent implements OnInit, OnDestroy {

  subscription: Subscription[] = [];
  departments: SelectedModel[] = [];
  sections: SelectedModel[] = [];
  designations: SelectedModel[] = [];
  financialYears: SelectedModel[] = [];

  displayedColumns: string[] = [
    // 'slNo',
    'employee',
    'department/section',
    'designation',
    'prlDate',
    'retirmentDate',
    'prlStatus',
    'retirmentStatus',
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
  financialYearId: number = 0;
  currentDate: string = new Date().toISOString().substring(0, 10);
  startDate: any;
  endDate: any;
  isPrl: boolean = false;
  isRetirment: boolean = false;
  isGone: boolean = false;
  isWillGone: boolean = false;
  departmentName: string = "";
  sectionName: string = "";
  designationName: string = "";
  financialYearName: string = "";
  typeName: string = "All";
  headingType: string = "PRL & Retirment";
  statusName: string = "All";
  totalEmployee: number = 0;
  isCustom : boolean = false;
  biwtaLogo: string = `${this.empPhotoSignService.imageUrl}TempleteImage/biwta-logo.png`;
  employees!: PrlRetirementReporting[];
  constructor(
    public prlRetirmentService: PrlRetirmentService,
    public departmentService: DepartmentService,
    public sectionService: SectionService,
    public empPhotoSignService: EmpPhotoSignService,
    public designationService: DesignationService,
    public designationSetupService: DesignationSetupService,
    private modalService: BsModalService,
    public empTransferPostingService: EmpTransferPostingService,
    private toastr: ToastrService,
    public financialYearService: FinancialYearService,
  ) {

  }

  icons = { cilSearch };

  ngOnInit(): void {
    this.getAllSelectedDepartments();
    this.getSelectDesignationSetupName();
    this.getSelectedFinancialYear();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs => subs.unsubscribe());
    }
  }

  getSelectedFinancialYear() {
    this.subscription.push(
      this.financialYearService.getSelectedFinancialYear().subscribe((res) => {
        this.financialYears = res;
      })
    )
  }

  onFinancialYearSelect() {
    this.financialYearName = "";
    if (this.paginator) {
      this.paginator.firstPage();
    }
    this.financialYearService.find(this.financialYearId).subscribe((res) => {
      if (res) {
        this.startDate = res.startDate;
        this.endDate = res.endDate;
        this.financialYearName = res.yearName;
        console.log(res)
        this.getPrlReportingResult(this.pagination);
      }
    })
  }

  getAllSelectedDepartments() {
    this.subscription.push(
      this.departmentService.getSelectedAllDepartment().subscribe((res) => {
        this.departments = res;
      })
    )
  }
  onDepartmentSelect() {
    this.departmentName = "";
    this.sectionName = "";
    if (this.paginator) {
      this.paginator.firstPage();
    }
    this.sectionId = 0;
    this.sectionService.getSectionByOfficeDepartment(this.departmentId).subscribe((res) => {
      this.sections = res;
    });
    this.departmentService.getById(this.departmentId).subscribe((res) => {
      if (res) {
        this.departmentName = res.departmentName;
      }
    });
    this.getPrlReportingResult(this.pagination);
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
    this.getPrlReportingResult(this.pagination);
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
    this.getPrlReportingResult(this.pagination);
  }

  onReportTypeChange(){
    this.isPrl = this.typeName == "PRL" ? true : false;
    this.isRetirment = this.typeName == "Retirment" ? true : false;
    this.headingType = this.typeName == "All" ? "PRL & Retirment" : this.typeName == "PRL" ? "PRL" : "Retirment";
    this.getPrlReportingResult(this.pagination);
  }

  onStatusChange(){
    this.isGone = this.statusName == "Gone" ? true : false;
    this.isWillGone = this.statusName == "Will Go" ? true : false;
    this.getPrlReportingResult(this.pagination);
  }

  onDateChange() {
    if(this.startDate < this.endDate){
      if (this.paginator) {
      this.paginator.firstPage();
      }
      this.getPrlReportingResult(this.pagination);
    }
  }

  getSelectDesignationSetupName() {
    this.subscription.push(
      this.designationService.getSelectDesignationSetupName().subscribe((data) => {
        this.designations = data;
      })
    )
  }

  onPageChange(event: any) {
    this.pagination.pageSize = event.pageSize;
    this.pagination.pageIndex = event.pageIndex + 1;
    this.getPrlReportingResult(this.pagination);
  }

  getPrlReportingResult(queryParams: any) {
    if(this.startDate && this.endDate){
      this.subscription.push(
      this.prlRetirmentService.getPrlReporting(queryParams, this.currentDate, this.startDate, this.endDate, this.departmentId, this.sectionId, this.designationId, this.isPrl, this.isRetirment, this.isGone, this.isWillGone).subscribe((res: any) => {
        console.log(res)
        this.dataSource.data = res;
        this.pagination.length = res[0].totalCount;
      })
    )
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
