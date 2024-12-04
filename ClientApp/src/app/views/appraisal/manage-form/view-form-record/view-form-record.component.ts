import { Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormRecordService } from '../../services/form-record.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Subscription } from 'rxjs';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-view-form-record',
  templateUrl: './view-form-record.component.html',
  styleUrl: './view-form-record.component.scss'
})
export class ViewFormRecordComponent implements OnInit, OnDestroy{
  loading: boolean;
  @Input() formRecordId: number;
  @Input() department: string;
  subscription: Subscription = new Subscription();
  formData: any;
  companyTitle :string = "Bangladesh Inland Water Transport Authority"
  address = "141-143, Motijheel Commerial Area, Dhaka-1000"
  @Input()
  formName = "Annual Confidential Report Of Officer"
  @ViewChild('recordDetail', {static:false}) recordDetail!: ElementRef;
  constructor(
    public formRecordService: FormRecordService,
    private toastr: ToastrService,
    private confirmService: ConfirmService,
    private modalService: BsModalService,
    private modalRef: BsModalRef
  ) {
    this.loading = false;
    this.formRecordId = 0;
    this.department = "";
    this.companyTitle = environment.companyTitle,
    this.address = environment.companyAddress
  }

  ngOnInit(): void {
    this.loading = true;
    this.getFormData();
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    } 
  }

  getFormData() {
    this.formRecordService.getFormData(this.formRecordId).subscribe({
      next: (response)=> {
        this.formData = response;
        console.log(this.formData)
      },
      error: (err)=> {
        console.log(err);
        this.loading=false;
      },
      complete:()=>  {
        this.loading=false;
      }
    })
  }

  onHide() {
    this.modalRef.hide();
  }

  printRecordDetail() {
      const element = this.recordDetail.nativeElement; // Access the DOM element
    const printContents = this.recordDetail.nativeElement.innerHTML;
    const originalContents = document.body.innerHTML;
    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
    window.location.reload();
  }
}
