import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft } from '@coreui/icons';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DepartmentService } from '../../basic-setup/service/department.service';
import { OfficeService } from '../../basic-setup/service/office.service';
import { EmpJobDetailsService } from '../../employee/service/emp-job-details.service';
import { EmpTransferPosting } from '../../transferPosting/model/emp-transfer-posting';
import { EmpTransferPostingService } from '../../transferPosting/service/emp-transfer-posting.service';

@Component({
  selector: 'app-increment-and-promotion',
  templateUrl: './increment-and-promotion.component.html',
  styleUrl: './increment-and-promotion.component.scss'
})
export class IncrementAndPromotionComponent  implements OnInit, OnDestroy {

  id!: number;
  headerText: string = '';
  btnText: string = '';
  offices: SelectedModel[] = [];
  departments: SelectedModel[] = [];
  designations: SelectedModel[] = [];
  grades: SelectedModel[] = [];
  scales: SelectedModel[] = [];
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  isValidEmp: boolean = false;
  isValidOrderByEmp: boolean = false;
  isApproveByEmp: boolean = false;
  loginEmpId: number = 0;
  empJobDetailsId: number = 0;
  // empTransferPosting: EmpPromotionIncrement = new EmpTransferPosting;
  @ViewChild('EmpTransferPostingForm', { static: true }) EmpTransferPostingForm!: NgForm;

  constructor(
    private toastr: ToastrService,
    public empTransferPostingService: EmpTransferPostingService,
    public empJobDetailsService: EmpJobDetailsService,
    public officeService: OfficeService,
    public departmentService: DepartmentService,
    private route: ActivatedRoute,
  ) {

  }

  icons = { cilArrowLeft };

  ngOnInit(): void {
    // this.initaialForm();
    const currentUserString = localStorage.getItem('currentUser');
    const currentUserJSON = currentUserString ? JSON.parse(currentUserString) : null;
    this.loginEmpId = currentUserJSON.empId;
    // this.getEmployeeByEmpId();
    // this.loadOffice();
    // this.getAllDepartment();
    // this.getSelectedSection();
    // this.getSelectedReleaseType();
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
