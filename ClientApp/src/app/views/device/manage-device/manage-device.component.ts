import { Component, OnDestroy, OnInit } from '@angular/core';
import {AttendanceDeviceModel} from '../model/attendance-device-model'
import {AttendanceDeviceService} from '../service/attendance-device.service'
import { Subscription } from 'rxjs';
import { cilCommand, cilFingerprint, cilReload, cilTrash } from '@coreui/icons';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { setThrowInvalidWriteToSignalError } from '@angular/core/primitives/signals';

@Component({
  selector: 'app-manage-device',
  templateUrl: './manage-device.component.html',
  styleUrl: './manage-device.component.scss'
})
export class ManageDeviceComponent implements OnInit, OnDestroy {
  loading: boolean
  attendanceDevices: AttendanceDeviceModel[];
  subscription: Subscription = new Subscription;

  icons = {cilReload, cilFingerprint, cilTrash, cilCommand}
  constructor(
    private AttendanceDeviceService: AttendanceDeviceService,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.attendanceDevices = []
    this.loading = false;
  }

  ngOnInit(): void {
    this.getAttendanceDevice();
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getAttendanceDevice() {
    this.loading = false;
   this.subscription = this.AttendanceDeviceService.getDevice().subscribe({
    next: response => {
      this.attendanceDevices = response.map(item => {
        let status2;
        if(item.status == false) {
          status2 = {value: "Deactive", color:"danger"}
        } else {
          const now = new Date();
          console.log(now);
          now.setSeconds(now.getSeconds()-30)
          console.log(now)
          console.log(item.lastOnline)
          console.log("------------------------")
          if(item.lastOnline == null || (new Date(item.lastOnline)).getTime() < now.getTime()) {
            status2 = {value:"Offline", color: "warning"}
          } else {
            status2 = {value:"Online", color: "success"}
          }
        }
        return {
          ...item,
          status2: status2
        }
      }) 
    },
    error: (err) => {
      this.loading = false;
    },
    complete: () => {
      this.loading = false;
    }
   }) 
  }

  deleteDevice(deviceId:number) {
    this.loading = true;
    this.confirmService.confirm("Delete Confirmation", "Are You Sure?").subscribe(response=> {
      if(response) {
        this.subscription = this.AttendanceDeviceService.deleteDevice(deviceId).subscribe({
          next: response => {
            if(response.success) {
              this.toastr.success('', `${response.message}`, {
                positionClass: 'toast-top-right'
              })

              this.attendanceDevices = this.attendanceDevices.filter(x => x.id != deviceId);
            } else {
              this.toastr.warning('',`${response.message}`, {
                positionClass: 'toast-top-right'
              })
            }
          },
          error: (err) => {
            this.loading = false;
          },
          complete: () => {
            this.loading = false;
          }
        })
      }
    })
  }

  rebootDevice(deviceId:number) {

    this.subscription = this.confirmService.confirm("Reboot The Device", "Are You Sure?").subscribe(response=> {
      if(response) {
        this.loading = true;
        this.subscription = this.AttendanceDeviceService.rebootDevice(deviceId).subscribe({
          next: response => {
            if(response.success) {
              this.toastr.success("",`${response.message}`, {
                positionClass: 'toast-top-right'
              })
            } else {
              this.toastr.warning("",`${response.message}`, {
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
    })
  }
}
