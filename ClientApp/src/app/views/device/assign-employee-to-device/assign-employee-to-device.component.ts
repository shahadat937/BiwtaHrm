import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { cilFingerprint, cilSearch } from '@coreui/icons';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { EmployeeListModalComponent } from '../../employee/employee-list-modal/employee-list-modal.component';
import {AssignEmployeeModel} from '../model/assign-employee-model'
import { NgForm } from '@angular/forms';
import { AttendanceDeviceService } from '../service/attendance-device.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-assign-employee-to-device',
  templateUrl: './assign-employee-to-device.component.html',
  styleUrl: './assign-employee-to-device.component.scss'
})
export class AssignEmployeeToDeviceComponent implements OnInit, OnDestroy {
  subscription: Subscription = new Subscription()
  @ViewChild("assignForm",{static:true}) assignForm!: NgForm
  employeeAssignModel: AssignEmployeeModel;
  devices: any[];
  loading: boolean;
  loadingEnroll: boolean
  FID: number;

  privilages: any[] = [
    {name:"User", value:'0'},
    {name:"Super Admin", value:'14'}
  ]

  fingers: any[] = [[
      {name: "Pinky", value: '0'},
      {name: "Ring Finger", value: '1'},
      {name: "Middle Finger", value: "2"},
      {name: "Index Finger", value: "3"},
      {name: "Thumb", value: "4"},
    ],[
      {name: "Thumb", value: "5"},
      {name: "Index Finger", value: "6"},
      {name: "Middle Finger", value: "7"},
      {name: "Ring Finger", value: "8"},
      {name: "Pinky", value: "9"}
    ]
  ]

  icons = {cilSearch, cilFingerprint}

  constructor(
    private modalService: BsModalService,
    private attendanceDeviceService: AttendanceDeviceService,
    private toastr: ToastrService
  ) {
    this.employeeAssignModel = new AssignEmployeeModel();
    this.loading = false;
    this.loadingEnroll = false;
    this.devices = [];
    this.FID = 5;
  }

  ngOnInit(): void {
    this.getDevice();
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getDevice() {
    this.subscription = this.attendanceDeviceService.getSelectedDevice().subscribe({
      next: response => {
        this.devices = response;
      }
    })
  }

  openEmployeeModal() {
    const modalRef: BsModalRef = this.modalService.show(EmployeeListModalComponent, { backdrop: 'static', class: 'modal-xl'  });

    if(modalRef) {
      this.subscription = modalRef.content.employeeSelected.subscribe((idCardNo:string) => {
        this.employeeAssignModel.idCardNo = idCardNo
      })
    }
  }

  onReset() {
    this.assignForm.form.reset();
    this.employeeAssignModel = new AssignEmployeeModel();
    this.FID = 5;
    const patchValue = {
      ...this.employeeAssignModel,
      fid: this.FID
    }
    this.assignForm.form.patchValue(patchValue);
  }

  onSubmit() {
    this.loading = true;

    this.subscription = this.attendanceDeviceService.assignEmployee(this.employeeAssignModel).subscribe({
      next: response => {
        if(response.success) {
          this.toastr.success('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
        } else {
          this.toastr.warning('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
        }
      },
      error: err => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    })
  }

  onFingerprintEnroll() {
    this.loadingEnroll = true;
    const fingerprint = new FormData();
    if(this.employeeAssignModel.idCardNo==null) {
      return;
    }
    if(this.employeeAssignModel.deviceId==null) {
      return;
    }

    fingerprint.append('idCardNo', this.employeeAssignModel.idCardNo?.toString());
    fingerprint.append("deviceId", this.employeeAssignModel.deviceId?.toString());
    fingerprint.append("fid", this.FID.toString());
    this.subscription = this.attendanceDeviceService.enrollFingerprint(fingerprint).subscribe({
      next: response => {
        if(response.success) {
          this.toastr.success('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
        } else {
          this.toastr.warning('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
        }
      },
      error: err => {
        this.loadingEnroll = false;
      },
      complete: () => {
        this.loadingEnroll = false;
      }
    })
  }
}
