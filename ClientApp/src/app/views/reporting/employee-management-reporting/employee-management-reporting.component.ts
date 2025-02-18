import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { PaginatorModel } from 'src/app/core/models/paginator-model';
import { ReportingService } from '../service/reporting.service';
import { EmpCountOnReportingDto } from '../models/emp-count-on-reporting-dto';
import { DepartmentService } from 'src/app/views/basic-setup/service/department.service';
import { SectionService } from 'src/app/views/basic-setup/service/section.service';
import { EmpPhotoSignService } from '../../employee/service/emp-photo-sign.service';
import { jsPDF } from 'jspdf';
import html2canvas from 'html2canvas';

@Component({
  selector: 'app-employee-management-reporting',
  templateUrl: './employee-management-reporting.component.html',
  styleUrl: './employee-management-reporting.component.scss'
})
export class EmployeeManagementReportingComponent  implements OnInit, OnDestroy {

  subscription: Subscription[]=[];
  informationType: number = 0;
  queryTypeName: string = '';
  queryType: SelectedModel[] = [];
  departments: SelectedModel[] = [];
  sections: SelectedModel[] = [];
  displayedColumns: string[] = [
      // 'slNo',
      'employee',
      'department/section',
      'designation',
      'typeName',
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
  typeId: number = 0;
  typeName: string = 'All';
  unAssigned: boolean = false;
  departmentId: number = 0;
  sectionId: number = 0;
  departmentName: string = "";
  sectionName: string = "";
  
  biwtaLogo : string = `${this.empPhotoSignService.imageUrl}TempleteImage/biwta-logo.png`;

  constructor(
    public reportingService: ReportingService,
    public departmentService: DepartmentService,
    public sectionService : SectionService,
    public empPhotoSignService: EmpPhotoSignService,
    ) {
  
    }

  ngOnInit(): void {
    this.getAllSelectedDepartments();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  onTypeValueChange(type: string, id: number){
    if(type == 'All'){
      this.unAssigned = false;
      this.typeId = 0;
      this.typeName = type;
    }
    else if(type == 'Unassigned'){
      this.unAssigned = true;
      this.typeId = 0;
      this.typeName = type;
    }
    else {
      this.unAssigned = false;
      this.typeId = id;
      this.typeName = type;
    }
    this.onQueryTypeChange(false);
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
    this.onQueryTypeChange(false);
  }
  onSectionSelect(){
    this.sectionName = "";
    this.sectionService.find(this.sectionId).subscribe((res) => {
      if(res){
        this.sectionName = res.sectionName;
      }
    });
    this.onQueryTypeChange(false);
  }

  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    this.pagination.pageIndex = event.pageIndex + 1;
    this.onQueryTypeChange(true);
  }

  onTypeChange(){
    this.departmentName = "";
    this.sectionName = "";
    this.departmentId = 0;
    this.sectionId = 0;
    this.typeId = 0;
    this.typeName = 'All';
    this.onQueryTypeChange(false);
  }
  onQueryTypeChange(pageChanged: boolean){
    if(!pageChanged){
      this.pagination.pageIndex = 1;
      if (this.paginator) {
        this.paginator.firstPage();
      }
    }
    if(this.queryTypeName == 'Employee Type'){
      this.getEmployeeTypeCount();
      this.getEmployeeTypeReportingResult(this.pagination);
    }
    else if(this.queryTypeName == 'Blood Group'){
      this.getBloodGroupCount();
      this.getBloodGroupReportingResult(this.pagination);
    }
    else if(this.queryTypeName == 'Religion'){
      this.getReligionCount();
      this.getReligionReportingResult(this.pagination);
    }
    else if(this.queryTypeName == 'Gender'){
      this.getGenderCount();
      this.getGenderReportingResult(this.pagination);
    }
    else if(this.queryTypeName == 'Marital Status'){
      this.getMaritalStatusCount();
      this.getMaritalStatusReportingResult(this.pagination);
    }
    else if(this.queryTypeName == 'Language'){
      this.getLanguageCount();
      this.getLanguageReportingResult(this.pagination);
    }
    else if(this.queryTypeName == 'Training Type'){
      this.getTrainingTypeCount();
      this.getTrainingTypeReportingResult(this.pagination);
    }
    else {
      this.dataSource.data = [];
      this.pagination.length = 0;
      this.queryCount = new EmpCountOnReportingDto();
    }
  }

      // Employee Type
  getEmployeeTypeCount(){
    this.subscription.push(
      this.reportingService.getEmployeeTypeCount(this.departmentId, this.sectionId).subscribe((res: any) => {
      this.queryCount = res;
    })
    )
  }
  getEmployeeTypeReportingResult(queryParams: any){
    this.subscription.push(
      this.reportingService.getEmployeeTypeReportingResult(queryParams, this.typeId, this.unAssigned, this.departmentId, this.sectionId).subscribe((res: any) => {
      this.dataSource.data = res.items;
      this.pagination.length = res.totalItemsCount;
    })
    )
  }


      // Religion
  getReligionCount(){
    this.subscription.push(
      this.reportingService.getReligionCount(this.departmentId, this.sectionId).subscribe((res: any) => {
      this.queryCount = res;
    })
    )
  }
  getReligionReportingResult(queryParams: any){
    this.subscription.push(
      this.reportingService.getReligionReportingResult(queryParams, this.typeId, this.unAssigned, this.departmentId, this.sectionId).subscribe((res: any) => {
      this.dataSource.data = res.items;
      this.pagination.length = res.totalItemsCount;
    })
    )
  }

  
  // Blood Group
  getBloodGroupCount(){
    this.subscription.push(
      this.reportingService.getBloodGroupCount(this.departmentId, this.sectionId).subscribe((res: any) => {
      this.queryCount = res;
    })
    )
  }
  getBloodGroupReportingResult(queryParams: any){
    this.subscription.push(
      this.reportingService.getBloodGroupReportingResult(queryParams, this.typeId, this.unAssigned, this.departmentId, this.sectionId).subscribe((res: any) => {
      this.dataSource.data = res.items;
      this.pagination.length = res.totalItemsCount;
    })
    )
  }
  
  // Gender
  getGenderCount(){
    this.subscription.push(
      this.reportingService.getGenderCount(this.departmentId, this.sectionId).subscribe((res: any) => {
      this.queryCount = res;
    })
    )
  }
  getGenderReportingResult(queryParams: any){
    this.subscription.push(
      this.reportingService.getGenderReportingResult(queryParams, this.typeId, this.unAssigned, this.departmentId, this.sectionId).subscribe((res: any) => {
      this.dataSource.data = res.items;
      this.pagination.length = res.totalItemsCount;
    })
    )
  }
  
  // MaritalStatus
  getMaritalStatusCount(){
    this.subscription.push(
      this.reportingService.getMaritalStatusCount(this.departmentId, this.sectionId).subscribe((res: any) => {
      this.queryCount = res;
    })
    )
  }
  getMaritalStatusReportingResult(queryParams: any){
    this.subscription.push(
      this.reportingService.getMaritalStatusReportingResult(queryParams, this.typeId, this.unAssigned, this.departmentId, this.sectionId).subscribe((res: any) => {
      this.dataSource.data = res.items;
      this.pagination.length = res.totalItemsCount;
    })
    )
  }

  
  // Language
  getLanguageCount(){
    this.subscription.push(
      this.reportingService.getLanguageCount(this.departmentId, this.sectionId).subscribe((res: any) => {
      this.queryCount = res;
    })
    )
  }
  getLanguageReportingResult(queryParams: any){
    this.subscription.push(
      this.reportingService.getLanguageReportingResult(queryParams, this.typeId, this.unAssigned, this.departmentId, this.sectionId).subscribe((res: any) => {
      this.dataSource.data = res.items;
      this.pagination.length = res.totalItemsCount;
    })
    )
  }

  
  // TrainingType
  getTrainingTypeCount(){
    this.subscription.push(
      this.reportingService.getTrainingTypeCount(this.departmentId, this.sectionId).subscribe((res: any) => {
      this.queryCount = res;
    })
    )
  }
  getTrainingTypeReportingResult(queryParams: any){
    this.subscription.push(
      this.reportingService.getTrainingTypeReportingResult(queryParams, this.typeId, this.unAssigned, this.departmentId, this.sectionId).subscribe((res: any) => {
      this.dataSource.data = res.items;
      this.pagination.length = res.totalItemsCount;
    })
    )
  }

  downloadSection() {
    const data = document.getElementById('tableData');
    html2canvas(data!).then(canvas => {
        const imgWidth = 208;
        const imgHeight = canvas.height * imgWidth / canvas.width;

        const contentDataURL = canvas.toDataURL('image/png');

        // Define margin values (in mm)
        const marginTop = 30; // Adjusted for header space
        const marginLeft = 10; // Left margin for the content
        const marginBottom = 10;

        const pdf = new jsPDF('p', 'mm', 'a4'); // A4 size page of PDF

        // Add the heading and paragraph manually
        pdf.setFontSize(20); // Set font size for the header
        pdf.text('BANGLADESH INLAND WATER TRANSPORT AUTHORITY', marginLeft, marginTop);
        
        pdf.setFontSize(13); // Set font size for the paragraph
        pdf.text('BIWTA BHABAN, 141-143, MOTIJHEEL C/A, POST BOX 76, DHAKA-1000, BANGLADESH', marginLeft, marginTop + 10);

        let departmentName = this.departmentName ? this.departmentName + '/' : '';
        let sectionName = this.sectionName ? this.sectionName + '/' : '';

        pdf.setFontSize(11); // Set font size for the paragraph
        pdf.text(this.queryTypeName + ' Report: ' + departmentName + sectionName + this.typeName, marginLeft, marginTop + 20);

        // Adjust position for the table content after the header
        const position = marginTop + 30; // Adjusted to leave space for header and paragraph

        // Add image of the table data to the PDF with margin adjustments
        // Ensure that the image is not cropped by properly adjusting its position and size
        pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight);

        // Save the generated PDF
        pdf.save('exported-file.pdf');
    });
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
          <title>${this.queryTypeName} Report</title>
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
