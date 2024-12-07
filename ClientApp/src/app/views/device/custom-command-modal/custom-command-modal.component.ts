import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AttendanceDeviceService } from '../service/attendance-device.service';
import { Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { cilWarning } from '@coreui/icons';

@Component({
  selector: 'app-custom-command-modal',
  templateUrl: './custom-command-modal.component.html',
  styleUrl: './custom-command-modal.component.scss'
})
export class CustomCommandModalComponent implements OnInit, OnDestroy {
  subscription:Subscription = new Subscription();
  loading: boolean
  @Input()
  deviceId: number;
  deviceName: string;
  command: string;
  icons = {cilWarning}
  constructor(
    private bsModalRef: BsModalRef,
    private modalService: BsModalService,
    private attendanceDeviceService: AttendanceDeviceService,
    private toastr: ToastrService
  ) {
    this.deviceId = 0;
    this.deviceName = "Default";
    this.loading = false;
    this.command = "";
  }

  ngOnInit(): void {
    
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    }
  }


  closeModal() {
    this.bsModalRef.hide();
  }

  onSubmit() {
    const formData = new FormData();
    formData.append('deviceId',this.deviceId.toString());
    formData.append('command',this.command);


    this.loading = true;
    this.subscription = this.attendanceDeviceService.customCommand(formData).subscribe({
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
      error: (err) => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    })
  }
}
