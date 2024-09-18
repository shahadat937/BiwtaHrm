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
  employeeType: SelectedModel[] = [];
  shifts: SelectedModel[] = [];
  loading: boolean = false;
  fileSelected = false;
  
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
    this.subscription = this.empBasicInfoService.getSelectedEmployeeType().subscribe((data) => { 
      this.employeeType = data;
    });
  }
  getSelectedShift(){
    this.subscription = this.shiftService.getSelectedShift().subscribe((data) => { 
      this.shifts = data;
    });
  }

  uploadFile(){
    const fileInput = this.formFileInput.nativeElement;
    const file = fileInput.files[0];
    if(file){
      this.uploadedFile = true;
      this.headingText = "Check Provided Informations"
    }
  }

  patchEmpBasicInfoInfo(EmpBasicInfoList: any[]) {
    const control = <FormArray>this.EmpBasicInfoForm.controls['empBasicInfoList'];
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
        personalFileNo: [basicInfo.occupationId],
        employeeTypeId: [1, Validators.required],
        shiftId: [1, Validators.required],
      }));
    });
  }
  
  EmpBasicInfoForm: FormGroup = new FormGroup({
    empChildList: new FormArray([])
  });

  get empBasicInfoListArray() {
    return this.EmpBasicInfoForm.controls["empBasicInfoList"] as FormArray;
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
      employeeTypeId: new FormControl(1, Validators.required),
      shiftId: new FormControl(1, Validators.required),
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
}
