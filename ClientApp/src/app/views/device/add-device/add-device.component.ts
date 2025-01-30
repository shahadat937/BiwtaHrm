import { Component, OnDestroy, OnInit } from '@angular/core';
import { cilPlus, cilReload, cilTrash } from '@coreui/icons';
import { interval, Subscription } from 'rxjs';
import { AttendanceDeviceService } from '../service/attendance-device.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';
import {PendingDeviceModel} from '../model/pending-device-model'
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { DeviceModalComponent } from '../device-modal/device-modal.component';
import { NotExpr } from '@angular/compiler';
import * as signalR from '@microsoft/signalr'
import {RealTimeService} from '../../../core/service/real-time.service';
import { ActivatedRoute, Router } from '@angular/router';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';
@Component({
  selector: 'app-add-device',
  templateUrl: './add-device.component.html',
  styleUrl: './add-device.component.scss'
})
export class AddDeviceComponent implements OnInit, OnDestroy {
  subscription: Subscription[] = [];
  pendingDevice: PendingDeviceModel[];
  loading: boolean
  checkDeviceInterval: Subscription = new Subscription();

  icons = {cilReload, cilTrash, cilPlus}

  //Authentication
  featurePermission: FeaturePermission = new FeaturePermission();
  
  constructor(
    private realTimeService: RealTimeService,
    private AttendanceDeviceService: AttendanceDeviceService,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef,
    private route: ActivatedRoute,
    private router: Router,
    private roleFeatureService: RoleFeatureService
  ) {
    this.pendingDevice = [];
    this.loading = false
  }



  ngOnInit(): void {
    this.getPermission();
    this.getPendingDevice(true);
    this.deleteExpireDevice(); 
    this.checkNewDevice();
  }

  ngOnDestroy(): void {

    this.subscription.forEach(subscription => {
      subscription.unsubscribe();
    })

    if(this.checkDeviceInterval) {
      this.checkDeviceInterval.unsubscribe();
    }
  }

  getPermission(){
    this.subscription.push(
    this.roleFeatureService.getFeaturePermission('addDevice').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        // To do
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    })
    )
  }


  deleteExpireDevice(): void {
    if(this.checkDeviceInterval) {
      this.checkDeviceInterval.unsubscribe();
    }
    const checkDeviceInterval = interval(15000);
    this.checkDeviceInterval = checkDeviceInterval.subscribe({
      next: () => {
        let currentDatetime = new Date();
        this.pendingDevice = this.pendingDevice.filter(device => {
          let targetDate = new Date(device.expireTime);
          if(targetDate.getTime() >= currentDatetime.getTime()) {
            return true;
          } else {
            return false;
          }
        })
      }
    })
  }

  checkNewDevice() {
    this.realTimeService.eventBus.getEvent('newDevice').subscribe({
      next: (data:any) => {
        this.getPendingDevice(false); 
      }
    })
  }


  getPendingDevice(showLoader:boolean) {
    this.loading = showLoader;
    const subs = this.AttendanceDeviceService.getPendingDevice().subscribe({
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

    this.subscription.push(subs);
  }

  addDevice(pendingDevice: PendingDeviceModel) {

    if(this.featurePermission.add==false) {
      this.roleFeatureService.unauthorizeAccress();
      return;
    }

    const initialState = {
      pendingDevice: pendingDevice
    }
    const modalRef: BsModalRef = this.modalService.show(DeviceModalComponent, { initialState, backdrop: 'static' });

    if(modalRef&&modalRef.onHide) {
      const subs = modalRef.onHide.subscribe(()=> {
        this.getPendingDevice(true);
      });

      this.subscription.push(subs);
    }
  }
}
