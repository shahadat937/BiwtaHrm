import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { OfficerFormService } from '../officer-form/service/officer-form.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import {FormRecordService} from '../services/form-record.service'
import { FormRecordModel } from '../models/form-record-model';
import { cilPencil, cilTrash, cibZoom, cilZoom, cilEyedropper } from '@coreui/icons';
import { BsModalService } from 'ngx-bootstrap/modal';
import { UpdateFormComponent } from '../update-form/update-form.component';
import {ViewFormRecordComponent} from './view-form-record/view-form-record.component'
import {FormRecordFilter} from '../models/form-record-filter'

@Component({
  selector: 'app-manage-form',
  templateUrl: './manage-form.component.html',
  styleUrl: './manage-form.component.scss'
})
export class ManageFormComponent implements OnInit, OnDestroy {
  loading: boolean ;
  subscription: Subscription = new Subscription();
  @Input()
  filters: FormRecordFilter;
  @Input()
  appraisalUserRole: number;
  formRecord: FormRecordModel[] = [];
  formRecordHeader: any[] ;
  icons = {cilPencil, cilTrash, cilZoom, cilEyedropper}
  globalFilter:string;


  constructor(
    public formRecordService: FormRecordService,
    private officerService: OfficerFormService,
    private toastr: ToastrService,
    private confirmService: ConfirmService,
    private modalService: BsModalService
  ) {
    this.loading = false;
    this.formRecordHeader = [{header:"PMS No.", field:"idCardNo"}, {header:"Name",field:"fullName"}, {header:"Department", field:"department"}, 
      {header: "From", field:"reportFrom", IsDate: true}, {header:"To", field:"reportTo", IsDate:true}]
    this.globalFilter="";
    this.filters = new FormRecordFilter();
    this.appraisalUserRole = -1;
  }

  ngOnInit(): void {
    this.getFormRecord();
  }

  ngOnDestroy(): void {

  }

  getFormRecord() {
    this.formRecordService.getFormRecordFiltered(this.filters).subscribe({
      next: (response)=> {
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

  onUpdate(formRecordId:number) {
    const initialState = {
      formRecordId : formRecordId
    } 

    this.modalService.show(UpdateFormComponent,{initialState:initialState});
  }


  onView(formRecordId:number) {
    console.log(formRecordId);
    const department = this.formRecord.find(x=>x.recordId == formRecordId)?.department;
    console.log(department);
    const initialState = {
      formRecordId: formRecordId,
      department: department
    }

    this.modalService.show(ViewFormRecordComponent, {initialState: initialState});
  }

}
