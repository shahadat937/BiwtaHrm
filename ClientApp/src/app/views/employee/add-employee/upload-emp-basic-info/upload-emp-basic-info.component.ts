import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { FormArray, Validators, FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { EmpBasicInfoService } from '../../service/emp-basic-info.service';
import { ShiftService } from 'src/app/views/attendance/services/shift.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-upload-emp-basic-info',
  templateUrl: './upload-emp-basic-info.component.html',
  styleUrl: './upload-emp-basic-info.component.scss'
})
export class UploadEmpBasicInfoComponent implements OnInit, OnDestroy {

  modalOpened: boolean = false;
  subscription: Subscription = new Subscription();
  @ViewChild('formFile') formFileInput!: ElementRef;
  uploadedFile: boolean = false;
  headingText: string = "Upload Employee Basic Information";
  employeeTypes: SelectedModel[] = [];
  shifts: SelectedModel[] = [];
  loading: boolean = false;
  fileSelected = false;
  empTypeId : any;
  empShiftId : any;
  
  constructor(
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2,
    private fb: FormBuilder,
    public empBasicInfoService: EmpBasicInfoService,
    public shiftService: ShiftService,
  ) { }

  
  ngOnInit(): void {
    this.getSelectedEmployeeType();
    this.getSelectedShift();
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);
  }

  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getSelectedEmployeeType(){
    this.subscription = this.empBasicInfoService.getFirstEmpTypeId().subscribe((res) => {
      this.empTypeId = res;
    })
    this.subscription = this.empBasicInfoService.getSelectedEmployeeType().subscribe((data) => { 
      this.employeeTypes = data;
    });
  }
  getSelectedShift(){
    this.subscription = this.empBasicInfoService.getFirstShiftId().subscribe((res) => {
      this.empShiftId = res;
    })
    this.subscription = this.shiftService.getSelectedShift().subscribe((data) => { 
      this.shifts = data;
    });
  }

  uploadFile() {
    const fileInput = this.formFileInput.nativeElement;
    const file = fileInput.files[0];
  
    if (file) {
      const reader = new FileReader();
  
      reader.onload = (event: any) => {
        const binaryStr = event.target.result;
        const workbook = XLSX.read(binaryStr, { type: 'binary' });
  
        // Assuming the data is in the first sheet
        const firstSheetName = workbook.SheetNames[0];
        const worksheet = workbook.Sheets[firstSheetName];
  
        // Parse the Excel data into JSON format
        const excelData = XLSX.utils.sheet_to_json(worksheet, { header: 1 });
  
        // Remove header row, filter out empty rows, and format the data
        const employeeData = excelData.slice(1)
          .filter((row: any) => row[1] && row[2]) // Filter rows where idCardNo (row[1]) and firstName (row[2]) are not empty
          .map((row: any) => ({
            idCardNo: row[1],         // PMIS No
            firstName: row[2],        // First Name
            lastName: row[3],         // Last Name
            firstNameBangla: row[4],  // First Name Bangla
            lastNameBangla: row[5],   // Last Name Bangla
            dateOfBirth: this.parseExcelDate(row[6]), // Date of Birth in DD/MM/YYYY
            nid: row[7],              // NID Number
            personalFileNo: row[8],   // Personal File No.
          }));
  
        // Patch the data to the form array
        this.patchEmpBasicInfoInfo(employeeData);
  
        this.uploadedFile = true;
        this.headingText = "Check Provided Information";
      };
  
      reader.readAsBinaryString(file);
    }
  }
  
  // Helper function to parse Excel date in DD/MM/YYYY format
  parseExcelDate(excelDate: string): string | null {
    if (!excelDate) return null;
  
    // Check for DD/MM/YYYY format with or without leading zeros
    const dateRegex = /^(0?[1-9]|[12][0-9]|3[01])\/(0?[1-9]|1[0-2])\/(\d{4})$/;
    const match = excelDate.match(dateRegex);
  
    if (match) {
      const day = match[1].padStart(2, '0');   // Ensure day is two digits
      const month = match[2].padStart(2, '0'); // Ensure month is two digits
      const year = match[3];                    // Full year
  
      // Return the date in YYYY-MM-DD format
      return `${year}-${month}-${day}`;
    }
  
    return null; // Return null if the date doesn't match the expected format
  }
  

  patchEmpBasicInfoInfo(EmpBasicInfoList: any[]) {
    const control = <FormArray>this.EmpBasicInfoForm.controls['empBasicList'];
    control.clear();

    EmpBasicInfoList.forEach(basicInfo => {
      control.push(this.fb.group({
        id: [0],
        idCardNo: [basicInfo.idCardNo, Validators.required],
        firstName: [basicInfo.firstName, Validators.required],
        lastName: [basicInfo.lastName],
        firstNameBangla: [basicInfo.lastName],
        lastNameBangla: [basicInfo.lastName],
        dateOfBirth: [basicInfo.dateOfBirth],
        nid: [basicInfo.nid],
        personalFileNo: [basicInfo.personalFileNo],
        employeeTypeId: [this.empTypeId, Validators.required],
        shiftId: [this.empShiftId, Validators.required],
      }));
    });
  }
  
  EmpBasicInfoForm: FormGroup = new FormGroup({
    empBasicList: new FormArray([])
  });

  get empBasicInfoListArray() {
    return this.EmpBasicInfoForm.controls["empBasicList"] as FormArray;
  }

  addEmpBasicInfo() {
    this.empBasicInfoListArray.push(new FormGroup({
      id: new FormControl(0),
      idCardNo: new FormControl(undefined, Validators.required),
      firstName: new FormControl(undefined, Validators.required),
      lastName: new FormControl(undefined),
      firstNameBangla: new FormControl(undefined),
      lastNameBangla: new FormControl(undefined),
      dateOfBirth: new FormControl(undefined),
      nid: new FormControl(undefined),
      personalFileNo: new FormControl(undefined),
      employeeTypeId: new FormControl(this.empTypeId, Validators.required),
      shiftId: new FormControl(this.empShiftId, Validators.required),
    }));
  }
  
  removeBasicInfoList(index: number) {
    if (this.empBasicInfoListArray.controls.length > 0)
      this.empBasicInfoListArray.removeAt(index);
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

  saveEmployeeBasicInfo(){
    this.loading = true;
    console.log(this.EmpBasicInfoForm.get("empBasicList")?.value);
    this.empBasicInfoService.saveImportedEmployeeBasicInfo(this.EmpBasicInfoForm.get("empBasicList")?.value).subscribe(((res: any) => {
      if (res.success) {
        this.toastr.success('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
        this.closeModal();
      } else {
        this.toastr.warning('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      }
      this.loading = false;
    })
    )
  }
}
