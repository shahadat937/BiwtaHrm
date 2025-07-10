import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
import { Subscription } from 'rxjs';
import { EmpPhotoSignService } from '../../employee/service/emp-photo-sign.service';
import { EmpCountOnReportingDto } from '../models/emp-count-on-reporting-dto';
import { ReportingService } from '../service/reporting.service';
import { DepartmentService } from 'src/app/views/basic-setup/service/department.service';
import { SectionService } from 'src/app/views/basic-setup/service/section.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { PaginatorModel } from 'src/app/core/models/paginator-model';
import { CountryService } from 'src/app/views/basic-setup/service/Country.service';
import { DistrictService } from 'src/app/views/basic-setup/service/district.service';
import { DivisionService } from 'src/app/views/basic-setup/service/division.service';
import { UapzilaService } from 'src/app/views/basic-setup/service/uapzila.service';

@Component({
  selector: 'app-address-reporting',
  templateUrl: './address-reporting.component.html',
  styleUrl: './address-reporting.component.scss'
})
export class AddressReportingComponent implements OnInit, OnDestroy {

  subscription: Subscription[]=[];
  addressType: string = 'Present Address';
  departments: SelectedModel[] = [];
  sections: SelectedModel[] = [];
  countris: SelectedModel[] = [];
  divisions: SelectedModel[] = [];
  districts: SelectedModel[] = [];
  upazilas: SelectedModel[] = [];
  displayedColumns: string[] = [
      // 'slNo',
      'employee',
      'department/section',
      'designation',
      'country',
      'divisionName',
      'districtName',
      'upazilaName',
      'phone',
      'status'
    ];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  pagination: PaginatorModel = new PaginatorModel();
  queryCount: EmpCountOnReportingDto = new EmpCountOnReportingDto();
  departmentId: number = 0;
  sectionId: number = 0;
  departmentName: string = "";
  sectionName: string = "";
  countryId: number = 0;
  countryName: string = "";
  divisionId: number = 0;
  divisionName: string = "";
  districtId: number = 0;
  districtName: string = "";
  upazilaId: number = 0;
  upazilaName: string = "";
  isPresentAddress: boolean = true;
  
  biwtaLogo : string = `${this.empPhotoSignService.imageUrl}TempleteImage/biwta-logo.png`;

  constructor(
    public reportingService: ReportingService,
    public departmentService: DepartmentService,
    public sectionService : SectionService,
    public empPhotoSignService: EmpPhotoSignService,
    public countryService: CountryService,
    public districtService: DistrictService,
    public uapzilaService: UapzilaService,
    public divisionService: DivisionService,
    ) {
  
    }

  ngOnInit(): void {
    this.getAllSelectedDepartments();
    this.loadcountris();
    this.getAddressReport(false);
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  getAllSelectedDepartments(){
    this.subscription.push(
      this.departmentService.getSelectedAllDepartment().subscribe((res) => {
          this.departments = res;
    })
    )
  }
  onDepartmentSelect(departmentId : number){
    this.departmentName = "";
    this.sectionName = "";
    this.sectionId = 0;
    this.sectionService.getSectionByOfficeDepartment(+departmentId).subscribe((res) => {
      if(res){
        this.sections = res;
      }
    });
    this.departmentService.getById(+departmentId).subscribe((res) => {
      if(res){
        this.departmentName = res.departmentName;
      }
    });
    this.getAddressReport(false);
  }
  onSectionSelect(){
    this.sectionName = "";
    this.sectionService.find(this.sectionId).subscribe((res) => {
      if(res){
        this.sectionName = res.sectionName;
      }
    });
    this.getAddressReport(false);
  }

  loadcountris() {
    this.subscription.push(
      this.countryService.selectGetCountry().subscribe((data) => {
      this.countris = data;
    })
    )
  }
  onCounterChange(counterId: number) {
    this.subscription.push(
      this.divisionService.getDivisionByCountryId(counterId).subscribe((data) => {
        this.divisions = data;
      })
    )
    this.getAddressReport(false);
  }
  onDivisionChange(divisionId: number) {
    this.subscription.push(
      this.districtService.getDistrictByDivisionId(divisionId).subscribe((data) => {
      this.districts = data;
    })
    )
    this.getAddressReport(false);
  }
  onDistrictChange(districtId: number) {
    this.subscription.push(
     this.uapzilaService.getUpapzilaByDistrictId(districtId).subscribe((data) => {
      this.upazilas = data;
    })
    )
    this.getAddressReport(false);
  }
  onUpazilaChange() {
    this.getAddressReport(false);
  }

  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    this.pagination.pageIndex = event.pageIndex + 1;
    this.getAddressReport(true);
  }

  getAddressReport(pageChanged: boolean){
    if(!pageChanged){
      this.pagination.pageIndex = 1;
      if (this.paginator) {
        this.paginator.firstPage();
      }
    }
    this.isPresentAddress = this.addressType == 'Present Address' ? true : false;
    this.subscription.push(
      this.reportingService.getAddressReportingResult(this.pagination, this.isPresentAddress, this.departmentId, this.sectionId, this.countryId, this.divisionId, this.districtId, this.upazilaId).subscribe((res: any) => {
      this.dataSource.data = res.items;
      this.pagination.length = res.totalItemsCount;
    })
    )
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
          <title>${this.addressType} Report</title>
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
