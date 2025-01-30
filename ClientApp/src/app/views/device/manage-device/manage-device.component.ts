import { Component, OnDestroy, OnInit } from '@angular/core';
import {AttendanceDeviceModel} from '../model/attendance-device-model'
import {AttendanceDeviceService} from '../service/attendance-device.service'
import { Subscription } from 'rxjs';
import { cilCommand, cilFingerprint, cilPencil, cilReload, cilTrash } from '@coreui/icons';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { setThrowInvalidWriteToSignalError } from '@angular/core/primitives/signals';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { DeviceModalComponent } from '../device-modal/device-modal.component';
import {CustomCommandModalComponent} from '../custom-command-modal/custom-command-modal.component'
import { RealTimeService } from '../../../core/service/real-time.service';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';
import { ActivatedRoute, Router } from '@angular/router';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';

@Component({
  selector: 'app-manage-device',
  templateUrl: './manage-device.component.html',
  styleUrl: './manage-device.component.scss'
})
export class ManageDeviceComponent implements OnInit, OnDestroy {
  loading: boolean
  attLoading: boolean
  attendanceDevices: AttendanceDeviceModel[];
  subscription: Subscription[] = []; 

  icons = {cilReload, cilPencil , cilFingerprint, cilTrash, cilCommand}

  //authentication
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
    this.attendanceDevices = []
    this.loading = false;
    this.attLoading = false;
  }

  ngOnInit(): void {
    this.getAttendanceDevice();

    const subs = this.realTimeService.eventBus.getEvent('AttDeviceUpdate').subscribe({
      next: (data: any) => {
        this.onDeviceUpdateEvent(data);
      }
    })
  }

  getPermission(){
    this.subscription.push(
    this.roleFeatureService.getFeaturePermission('manageDevice').subscribe((item) => {
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


  ngOnDestroy(): void {
    this.subscription.forEach(subs => subs.unsubscribe());
  }

  getAttendanceDevice() {
    this.loading = true;
    this.attLoading = true;
    const subs = this.AttendanceDeviceService.getDevice().subscribe({
    next: response => {
      this.attendanceDevices = this.transformAttRecord(response);
    },
    error: (err) => {
      this.loading = false;
      this.attLoading = false;
    },
    complete: () => {
      this.loading = false;
      this.attLoading = false;
    }
   }) 

   this.subscription.push(subs);
  }

  deleteDevice(deviceId:number) {
    if(this.featurePermission.delete==false) {
      this.roleFeatureService.unauthorizeAccress();
      return;
    }

    this.confirmService.confirm("Delete Confirmation", "Are You Sure?").subscribe(response=> {
      if(response) {
        this.loading = true;
        const subs = this.AttendanceDeviceService.deleteDevice(deviceId).subscribe({
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

        this.subscription.push(subs);
      }
    })
  }

  transformAttRecord(data:any[]) {
      let result = data.map(item => {
        let status2;
        if(item.status == false) {
          status2 = {value: "Deactive", color:"danger"}
        } else {
          const now = new Date();
          now.setSeconds(now.getSeconds()-15)
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

      return result
  }

  rebootDevice(deviceId:number) {

    const subs = this.confirmService.confirm("Reboot The Device", "Are You Sure?").subscribe(response=> {
      if(response) {
        this.loading = true;
        this.subscription.push(this.AttendanceDeviceService.rebootDevice(deviceId).subscribe({
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
        }));

      }
    })

    this.subscription.push(subs);
  }

  onUpdate(device: AttendanceDeviceModel) {
    if(this.featurePermission.update==false) {
      this.roleFeatureService.unauthorizeAccress();
      return;
    }
    const initialState = {
      deviceModel: device,
      IsUpdate: true,
      buttonText: "Update",
      modalName: "Update Device"
    }

    const modalRef: BsModalRef = this.modalService.show(DeviceModalComponent,{initialState: initialState});

    if(modalRef&&modalRef.onHide) {
      this.subscription.push(modalRef.onHide.subscribe(()=> {
        this.getAttendanceDevice();
      }));
    }
  }

  customCommandHandler(deviceId:number,title:string) {
    const initialState = {
      deviceId: deviceId,
      deviceName: title
    }

    const modalRef: BsModalRef = this.modalService.show(CustomCommandModalComponent,{initialState:initialState});

  }

  onDeviceUpdateEvent(data:any) {
    if(data.op=="update") {
      let device = [data.data];
      let IsNewDevice = true;

      let index = this.attendanceDevices.findIndex(item => item.id == data.data.id);

      if(index!=-1) {
        this.attendanceDevices[index] = this.transformAttRecord(device)[0];
      } else {
        device = this.transformAttRecord(device);
        this.attendanceDevices = [...device,...this.attendanceDevices];
      }
    }
  }
}
