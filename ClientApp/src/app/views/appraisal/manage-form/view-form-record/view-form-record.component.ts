import { Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormRecordService } from '../../services/form-record.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Subscription } from 'rxjs';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { environment } from 'src/environments/environment';
import { SharedService } from 'src/app/shared/shared.service';


@Component({
  selector: 'app-view-form-record',
  templateUrl: './view-form-record.component.html',
  styleUrl: './view-form-record.component.scss'
})
export class ViewFormRecordComponent implements OnInit, OnDestroy{
  loading: boolean;
  @Input() formRecordId: number;
  @Input() department: string;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
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
    private modalRef: BsModalRef,
    public sharedService : SharedService
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
      this.subscription.forEach(subs=>subs.unsubscribe())
    } 
  }

  getFormData() {
    this.subscription.push(
      this.formRecordService.getFormData(this.formRecordId).subscribe({
      next: (response)=> {

           // format Applyer signiture date 
        if (response.sections[0]?.fields[13]?.fieldValue) {
          response.sections[0].fields[13].fieldValue = this.sharedService.parseDate(response.sections[0].fields[13].fieldValue)
        }

          // format Joining date 
        if (response.sections[0]?.fields[6]?.fieldValue) {
          response.sections[0].fields[6].fieldValue = this.sharedService.parseDate(response.sections[0].fields[6].fieldValue)
        }
          // format Joining Date Of Current Designation:
        if (response.sections[0]?.fields[7]?.fieldValue) {
          response.sections[0].fields[7].fieldValue = this.sharedService.parseDate(response.sections[0].fields[7].fieldValue)
        }
          // format Birth Date Of Current Designation:
        if (response.sections[0]?.fields[8]?.fieldValue) {
          response.sections[0].fields[8].fieldValue = this.sharedService.parseDate(response.sections[0].fields[8].fieldValue)
        }

        // format Repoting Office Signiture Date
        if (response.sections[4]?.fields[11]?.fieldValue) {
          response.sections[4].fields[11].fieldValue = this.sharedService.parseDate(response.sections[4].fields[11].fieldValue)
        }

          // format Counter Office Signiture Date
        if (response.sections[5]?.fields[4]?.fieldValue) {
          response.sections[5].fields[4].fieldValue = this.sharedService.parseDate(response.sections[5].fields[4].fieldValue)
        }


                     // format Joining Date Of Current Designation:
        if (response.sections[0]?.fields[11]?.childFields[0]?.fieldValue) {
          response.sections[0].fields[11].childFields[0].fieldValue = this.sharedService.parseDate( response.sections[0].fields[11].childFields[0].fieldValue)
        }
          // format Birth Date Of Current Designation:
        if ( response?.sections[0]?.fields[11]?.childFields[1]?.fieldValue) {
           response.sections[0].fields[11].childFields[1].fieldValue= this.sharedService.parseDate( response.sections[0].fields[11].childFields[1].fieldValue)
        }

            if (response.sections[5]?.fields[0]?.childFields[0]?.fieldValue) {
          response.sections[5].fields[0].childFields[0].fieldValue = this.sharedService.parseDate( response.sections[5].fields[0].childFields[0].fieldValue)
        }
          // format Birth Date Of Current Designation:
        if ( response?.sections[5]?.fields[0]?.childFields[1]?.fieldValue) {
           response.sections[5].fields[0].childFields[1].fieldValue= this.sharedService.parseDate( response.sections[5].fields[0].childFields[1].fieldValue)
        }



          // Staff Form Date Format


          // format Joining date 
          if (response.sections[0]?.fields[8]?.fieldValue) {
            response.sections[0].fields[8].fieldValue = this.sharedService.parseDate(response.sections[0].fields[8].fieldValue)
          }
          // format Joining Date Of Current Designation:
          if (response.sections[0]?.fields[9]?.fieldValue) {
            response.sections[0].fields[9].fieldValue = this.sharedService.parseDate(response.sections[0].fields[9].fieldValue)
          }
          // format Birth Date:
          if (response.sections[0]?.fields[5]?.fieldValue) {
            response.sections[0].fields[5].fieldValue = this.sharedService.parseDate(response.sections[0].fields[5].fieldValue)
          }

          // format Applyer Signiture dateTime to Date
          if (response?.sections[0]?.fields[14]?.fieldValue) {
            response.sections[0].fields[14].fieldValue = this.sharedService.parseDate(response?.sections[0]?.fields[14]?.fieldValue)
          };

          // format Tenure Of Service Under Reporting Officer From:
          if (response.sections[0]?.fields[13]?.childFields[0]?.fieldValue) {
            response.sections[0].fields[13].childFields[0].fieldValue = this.sharedService.parseDate(response.sections[0].fields[13].childFields[0].fieldValue)
          }
          //  format Tenure Of Service Under Reporting Officer To
          if (response?.sections[0]?.fields[13]?.childFields[1]?.fieldValue) {
            response.sections[0].fields[13].childFields[1].fieldValue = this.sharedService.parseDate(response.sections[0].fields[13].childFields[1].fieldValue)
          }



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
    )
    
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
