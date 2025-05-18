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
import { AppraisalRole } from '../enum/appraisal-role';
import { AuthService } from '../../../../../src/app/core/service/auth.service';
import { environment } from '../../../../../src/environments/environment';
import { ActivatedRoute, Router } from '@angular/router';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { PaginatorModel } from '../../../../../src/app/core/models/paginator-model';
import { DepartmentService } from '../../../../../src/app/views/basic-setup/service/department.service';
import { SectionService } from '../../../../../src/app/views/basic-setup/service/section.service';

@Component({
  selector: 'app-manage-form',
  templateUrl: './manage-form.component.html',
  styleUrl: './manage-form.component.scss'
})
export class ManageFormComponent implements OnInit, OnDestroy {
  loading: boolean ;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  @Input()
  filters: FormRecordFilter;
  @Input()
  appraisalUserRole: number;
  officerFormId: number;
  staffFormId: number;
  formRecord: FormRecordModel[] = [];
  formRecordHeader: any[] ;
  icons = {cilPencil, cilTrash, cilZoom, cilEyedropper}
  globalFilter:string;
  officerFormEditRoute: any;
  appraisalRole = AppraisalRole;
  routelink: string = "";
  pagination: PaginatorModel = new PaginatorModel();
  departments: any;
  sections: any;
  selectedDepartment: any = null;
  selectedSection: any = null;
  totalRecords: number = 0;
  reportFrom: any = null;
  reportTo: any = null;

  //authentication
  @Input()
  featureName: string;
  featurePermission: FeaturePermission = new FeaturePermission();

  constructor(
    private authService: AuthService,
    public formRecordService: FormRecordService,
    private officerService: OfficerFormService,
    private toastr: ToastrService,
    private confirmService: ConfirmService,
    private modalService: BsModalService,
    private route: ActivatedRoute,
    private router: Router,
    private roleFeatureService: RoleFeatureService,
    public departmentService: DepartmentService,
    public sectionService : SectionService,
  ) {
    this.officerFormId = environment.officerFormId;
    this.staffFormId = environment.staffFormId;
    this.staffFormId = environment.staffFormId;
    this.loading = false;
    this.formRecordHeader = [{header:"PMS No.", field:"idCardNo"}, {header:"Name",field:"fullName"}, {header:"Department", field:"department"}, 
      {header: "From", field:"reportFrom", IsDate: true}, {header:"To", field:"reportTo", IsDate:true},
      {header:"Reporting Officer",field:"reportingOfficerApproval",IsBinary:true},
      {header:"Counter Signatory", field:"counterSignatoryApproval",IsBinary:true},
      {header: "Receiver", field:"receiverApproval",IsBinary:true}]
    this.globalFilter="";
    this.filters = new FormRecordFilter();
    this.appraisalUserRole = -1;
    // To future developer: Don't change the order of the strings, it 
    /*this.officerFormEditRoute = [
       "",
       "/appraisal/reportingFormOfficer/",
       "/appraisal/counterSignatureFormOfficer/",
       "/appraisal/receiverFormOfficer/"
    ]*/
    this.officerFormEditRoute = {
      [AppraisalRole.ReportingOfficer]: "/appraisal/reportingFormOfficer/",
      [AppraisalRole.CounterSignatory]: "/appraisal/counterSignatureFormOfficer/",
      [AppraisalRole.Receiver]: "/appraisal/receiverFormOfficer/"
    };
    //this.appraisalRole.Receiver

    this.subscription.push(
      this.authService.currentUser.subscribe(user => {
      if(user&&user.empId) {
        this.filters.empId = parseInt(user.empId);
      }
    })
    )
    
    this.featureName = "";

  }

  ngOnInit(): void {
    this.getPermission();
      this.route.queryParams.subscribe(data => {
      if(data['forNotificationId']) {
        this.filters['recordId'] = data['forNotificationId'];
        this.routelink = this.router.url.split('?')[0];
        this.getFormRecord();
      } else if(this.filters['recordId']) {
        this.filters['recordId'] = null;
        this.routelink= "";
        this.getFormRecord();
      }
    })
    this.getFormRecord();
    this.getAllSelectedDepartments();
  }

  getPermission(){
    this.subscription.push(
    this.roleFeatureService.getFeaturePermission(this.featureName).subscribe((item) => {
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
  
  getAllSelectedDepartments(){
    this.subscription.push(
      this.departmentService.getSelectedAllDepartment().subscribe((res) => {
          this.departments = res;
    })
    )
  }

  onDateSelect(){
    if(this.reportFrom && this.reportTo){
      this.filters.reportFrom = this.reportFrom;
      this.filters.reportTo = this.reportTo;
      this.getFormRecord();
    }
  }

  onDepartmentSelectGetSection(){
    this.selectedSection = null;
    this.sections = [];
    this.filters.departmentId = this.selectedDepartment;
    this.filters.sectionId = this.selectedSection;
    this.sectionService.getSectionByOfficeDepartment(this.selectedDepartment).subscribe((res) => {
      this.sections = res;
    });
    this.getFormRecord();
  }
  
  onSectionChange(){
    this.filters.sectionId = this.selectedSection;
    this.getFormRecord();
  }

  
  applyFilter(filterValue: string) {
    filterValue = filterValue.toLowerCase();
    this.filters.pageIndex = 1;
    this.filters.searchText = filterValue;
    this.getFormRecord();
  }

  onPageChange(event: any){
    this.filters.pageSize = event.rows;
    this.filters.pageIndex = (event.first / event.rows) + 1;
    this.getFormRecord();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  getFormRecord() {
    this.subscription.push(
      this.formRecordService.getFormRecordFiltered(this.filters).subscribe({
      next: (response)=> {
        this.formRecord = response.items;
        this.totalRecords = response.totalItemsCount;
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
    )
    
  }

  onDelete(recordId:number, index:number) {

    if(this.featurePermission.delete==false) {
      this.roleFeatureService.unauthorizeAccress();
      return;
    }

    this.subscription.push(
      this.confirmService.confirm('Delete Confirmation', 'Are you sure?').subscribe({
      next:(response)=> {
        if(response) {
          this.formRecordService.deleteRecordById(recordId).subscribe({
            next:(response)=> {
              if(response.success) {
                this.toastr.success('',`${response.message}`, {
                  positionClass: 'toast-top-right'
                })

                this.formRecord = this.formRecord.filter(x=>x.recordId != recordId);
                //delete this.formRecord[index];
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
    )
    
  }

  onUpdate(formRecordId:number) {
    const initialState = {
      formRecordId : formRecordId
    } 

    this.modalService.show(UpdateFormComponent,{initialState:initialState});
  }


  onView(formRecordId:number) {
    let formName = "";
    let formId = this.formRecord.find(x=>x.recordId == formRecordId)?.formId;
    if(formId == this.officerFormId) {
      formName = environment.officerFormName;
    } else if(formId == this.staffFormId) {
      formName = environment.staffFormName;
    }
    const department = this.formRecord.find(x=>x.recordId == formRecordId)?.department;
    const initialState = {
      formRecordId: formRecordId,
      department: department,
      formName: formName
    }

    this.modalService.show(ViewFormRecordComponent, {initialState: initialState});
  }

}
