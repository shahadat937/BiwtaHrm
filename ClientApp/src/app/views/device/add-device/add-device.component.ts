import { Component, OnDestroy, OnInit } from '@angular/core';
import { cilPlus, cilReload, cilTrash } from '@coreui/icons';
import { Subscription } from 'rxjs';
import { AttendanceDeviceService } from '../service/attendance-device.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';
import {PendingDeviceModel} from '../model/pending-device-model'
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { DeviceModalComponent } from '../device-modal/device-modal.component';
import { NotExpr } from '@angular/compiler';
import * as signalR from '@microsoft/signalr'
@Component({
  selector: 'app-add-device',
  templateUrl: './add-device.component.html',
  styleUrl: './add-device.component.scss'
})
export class AddDeviceComponent implements OnInit, OnDestroy {
  subscription: Subscription = new Subscription();
  pendingDevice: PendingDeviceModel[];
  loading: boolean

  icons = {cilReload, cilTrash, cilPlus}
  
  constructor(
    private AttendanceDeviceService: AttendanceDeviceService,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef
  ) {
    this.pendingDevice = [];
    this.loading = false
  }



  ngOnInit(): void {
    this.getPendingDevice(); 
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    }
  }



  getPendingDevice() {
    this.loading = true;
    this.subscription = this.AttendanceDeviceService.getPendingDevice().subscribe({
      next: response => {
        this.pendingDevice = response;
      },
      error: err => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    })
  }

  addDevice(pendingDevice: PendingDeviceModel) {

    const initialState = {
      pendingDevice: pendingDevice
    }
    const modalRef: BsModalRef = this.modalService.show(DeviceModalComponent, { initialState, backdrop: 'static' });

    if(modalRef&&modalRef.onHide) {
      this.subscription = modalRef.onHide.subscribe(()=> {
        this.getPendingDevice();
      });
    }
  }
}
