import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { OfficerFormService } from '../officer-form/service/officer-form.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import {FormRecordService} from '../services/form-record.service'
import { FormRecordModel } from '../models/form-record-model';
import { cilPencil, cilTrash, cibZoom, cilZoom } from '@coreui/icons';

@Component({
  selector: 'app-manage-form',
  templateUrl: './manage-form.component.html',
  styleUrl: './manage-form.component.scss'
})
export class ManageFormComponent implements OnInit, OnDestroy {
  loading: boolean ;
  subscription: Subscription = new Subscription();
  formRecord: FormRecordModel[] = [];
  formRecordHeader: any[] ;
  icons = {cilPencil, cilTrash, cilZoom}
  globalFilter:string;


  constructor(
    public formRecordService: FormRecordService,
    private officerService: OfficerFormService,
    private toastr: ToastrService,
    private confirmService: ConfirmService,
  ) {
    this.loading = false;
    this.formRecordHeader = [{header:"Record Id",field:"recordId"}, {header:"PMS No.", field:"idCardNo"}, {header:"Name",field:"fullName"}, {header:"Department", field:"department"}]
    this.globalFilter="";
  }

  ngOnInit(): void {
    this.getFormRecord();
  }

  ngOnDestroy(): void {

  }

  getFormRecord() {
    this.formRecordService.getFormRecord().subscribe({
      next: (response)=> {
        console.log(response);
        this.formRecord = response;
        this.formRecord.forEach(item=> {
          item.fullName = `${item.empFirstName} ${item.empLastName}`;
        })
      },
      error: err=> {
        console.error(err);
      },
      complete: () => {

      }
    })
  }

  onDelete(recordId:number, index:number) {
    this.confirmService.confirm('Delete Confirmation', 'Are you sure?').subscribe({
      next:(response)=> {
        if(response) {
          this.formRecordService.deleteRecordById(recordId).subscribe({
            next:(response)=> {
              if(response.success) {
                this.toastr.success('',`${response.message}`, {
                  positionClass: 'toast-top-right'
                })

                delete this.formRecord[index];
              } else {
                this.toastr.warning('',`${response.message}`, {
                  positionClass: 'toast-top-right'
                })
              }
            }
          })

        }
      }
    })
  }
}
