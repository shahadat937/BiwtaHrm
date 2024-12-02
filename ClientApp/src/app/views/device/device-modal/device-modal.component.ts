import { Component, ElementRef, Input, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { PendingDeviceModel } from '../model/pending-device-model';
import { AttendanceDeviceModel } from '../model/attendance-device-model';
import { AttendanceDeviceService } from '../service/attendance-device.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-device-modal',
  templateUrl: './device-modal.component.html',
  styleUrl: './device-modal.component.scss'
})
export class DeviceModalComponent implements OnInit, OnDestroy {
  subscription: Subscription = new Subscription();
  modalOpened: boolean = false;
  modalName: string;
  @Input()
  pendingDevice: PendingDeviceModel | null;
  @Input()
  deviceModel: AttendanceDeviceModel
  @Input()
  buttonText: string;
  loading: boolean;

  Timezones: any[] = [
  { name: "UTC -12:00", value: "-12" },
  { name: "UTC -11:00", value: "-11" },
  { name: "UTC -10:00", value: "-10" },
  { name: "UTC -09:00", value: "-9" },
  { name: "UTC -08:00", value: "-8" },
  { name: "UTC -07:00", value: "-7" },
  { name: "UTC -06:00", value: "-6" },
  { name: "UTC -05:00", value: "-5" },
  { name: "UTC -04:00", value: "-4" },
  { name: "UTC -03:00", value: "-3" },
  { name: "UTC -02:00", value: "-2" },
  { name: "UTC -01:00", value: "-1" },
  { name: "UTC", value: "0" },
  { name: "UTC +01:00", value: "1" },
  { name: "UTC +02:00", value: "2" },
  { name: "UTC +03:00", value: "3" },
  { name: "UTC +04:00", value: "4" },
  { name: "UTC +05:00", value: "5" },
  { name: "UTC +06:00", value: "6" },
  { name: "UTC +07:00", value: "7" },
  { name: "UTC +08:00", value: "8" },
  { name: "UTC +09:00", value: "9" },
  { name: "UTC +10:00", value: "10" },
  { name: "UTC +11:00", value: "11" },
  { name: "UTC +12:00", value: "12" },
  { name: "Pacific/Kwajalein (UTC +12)", value: "12" },
  { name: "Asia/Kolkata (UTC +05:30)", value: "5.5" },
  { name: "Asia/Dhaka (UTC +06:00)", value: "6" },
  { name: "Asia/Tokyo (UTC +09:00)", value: "9" },
  { name: "Australia/Sydney (UTC +11:00)", value: "11" },
  { name: "Europe/London (UTC +00:00)", value: "0" },
  { name: "Europe/Berlin (UTC +01:00)", value: "1" },
  { name: "America/New_York (UTC -05:00)", value: "-5" },
  { name: "America/Los_Angeles (UTC -08:00)", value: "-8" },
  { name: "America/Chicago (UTC -06:00)", value: "-6" },
  { name: "America/Denver (UTC -07:00)", value: "-7" },
  { name: "Asia/Dubai (UTC +04:00)", value: "4" },
  { name: "Asia/Singapore (UTC +08:00)", value: "8" }
]; 

  constructor(
    private modalService: BsModalService,
    private toastr: ToastrService,
    private attendanceDeviceService: AttendanceDeviceService,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2
  ) {
    this.modalName = "Add New Device"
    this.pendingDevice = null;
    this.deviceModel = new AttendanceDeviceModel();
    this.deviceModel.title = "Default"
    this.buttonText = "Submit"
    this.loading = false;
  }

  ngOnInit(): void {
    if(this.pendingDevice!=null) {
      this.deviceModel.sn = this.pendingDevice.sn;
      console.log(this.deviceModel.title);
    } 
  }

  closeModal() {
    this.bsModalRef.hide();
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    } 
  }

  onSubmit() {
    this.addDevice();
  }

  addDevice() {
    this.loading = true;
    this.attendanceDeviceService.addDevice(this.deviceModel).subscribe({
      next: response => {
        if(response.success) {
          this.toastr.success("",`${response.message}`, {
            positionClass: "toast-top-right"
          })          
        } else {
          this.toastr.warning("",`${response.message}`, {
            positionClass: "toast-top-right"
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
