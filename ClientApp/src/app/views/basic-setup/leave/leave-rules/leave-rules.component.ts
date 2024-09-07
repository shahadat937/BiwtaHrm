import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { cilPencil, cilTrash } from '@coreui/icons';
import {LeaveRuleService} from '../../service/leave-rule.service'
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { NgForm } from '@angular/forms';
import { LeaveRulesModel } from '../../model/leave-rules-model';

@Component({
  selector: 'app-leave-rules',
  templateUrl: './leave-rules.component.html',
  styleUrl: './leave-rules.component.scss'
})
export class LeaveRulesComponent implements OnInit, OnDestroy {
  @ViewChild("leaveRuleForm",{static:true}) leaveRuleForm!:NgForm;
  loading: boolean;
  isUpdate: boolean;
  updateIndex: number = -1;
  leaveRules: any[] = [];
  icons = {cilPencil, cilTrash};
  leaveTypeIdFilter: number|null;
  leaveTypeOption: any[] = [];
  RuleNameOption: any[] = [];
  GenderOption: any[] = [];

  constructor(
    public leaveRuleService: LeaveRuleService,
    private toastr: ToastrService,
    private confirmService: ConfirmService
  ) {
    this.loading = false;
    this.isUpdate = false;
    this.leaveTypeIdFilter = null;
  }

  ngOnInit(): void {
    this.leaveRuleService.getSelectedLeaveType().subscribe({
      next: option => {
        this.leaveTypeOption = option;
      }
    });
    
    this.leaveRuleService.getSelectedRuleName().subscribe({
      next: (option)=> {
        this.RuleNameOption = option;
      }
    });
    this.GetSelectedGender();
  }

  getAllLeaveRule() {

  }
  
  ngOnDestroy(): void {
    
  }

  onDelete(id:number, index:number) {
    this.confirmService.confirm("Delete Confirmation","Are you sure?").subscribe({
      next: response=> {
        if(response) {
          this.leaveRuleService.deleteLeaveRule(id).subscribe({
            next: (response)=> {
              if(response.success==true) {
                this.toastr.success('',`${response.message}`, {
                  positionClass: 'toast-top-right'
                });

                delete this.leaveRules[index];
              } else {
                this.toastr.warning('',`${response.message}`, {
                  positionClass: 'toast-top-right'
                });
              }
            }
          })
        }
      }
    })
  }

  onUpdate() {
    this.loading = true;
    console.log(this.leaveRuleService.leaveRule);
    this.leaveRuleService.updateLeaveRule(this.leaveRuleService.leaveRule).subscribe({
      next: response => {
        if(response.success == true) {
          this.toastr.success('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
          
          if(this.leaveTypeIdFilter == this.leaveRuleService.leaveRule.leaveTypeId) {
            this.leaveRules[this.updateIndex].ruleName = this.leaveRuleService.leaveRule.ruleName;
            this.leaveRules[this.updateIndex].ruleValue = this.leaveRuleService.leaveRule.ruleValue;
            this.leaveRules[this.updateIndex].isActive = this.leaveRuleService.leaveRule.isActive;
          }
          this.onReset();
        } else {
          this.toastr.warning('',`${response.message}`, {
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
    })
  }

  onReset() {
    this.isUpdate = false;
    this.updateIndex = -1;
    this.leaveRuleForm.reset();
    this.leaveRuleService.leaveRule = new LeaveRulesModel();
  }


  onSubmit() {
    this.isUpdate?this.onUpdate():this.saveLeaveRule();
  }

  saveLeaveRule() {
    console.log(this.leaveRuleService.leaveRule);
    this.loading = true;
    this.leaveRuleService.saveLeaveRule(this.leaveRuleService.leaveRule).subscribe({
      next: (response)=> {
        if(response.success) {
          this.toastr.success('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })

          if(this.leaveTypeIdFilter == this.leaveRuleService.leaveRule.leaveTypeId && this.leaveTypeIdFilter!=null) {
            this.leaveRuleService.getLeaveRules(this.leaveTypeIdFilter).subscribe({
              next: data => {
                this.leaveRules = data;
              }
            });

          }
        }
      },
      error: err=> {
        this.loading = false;
      },
      complete: ()=> {
        this.loading = false;
      }
    })
  }

  UpdateButtonAction(data:any,index:number) {
    console.log("Yet to be implemented UpdateButtonAction");
    this.isUpdate = true;
    this.updateIndex = index;
    this.leaveRuleForm.form?.patchValue(data);
    this.leaveRuleService.leaveRule.ruleId = data.ruleId;
    this.leaveRuleService.leaveRule.leaveTypeId = this.leaveTypeIdFilter;
  }

  onLeaveTypeChange() {
    if(this.leaveTypeIdFilter==null) {
      return;
    }

    this.leaveRuleService.getLeaveRules(this.leaveTypeIdFilter).subscribe({
      next: data => {
        this.leaveRules = data;
      }
    });
  }

  GetSelectedGender() {
    this.leaveRuleService.getSelectedGender().subscribe({
      next: response=> {
        this.GenderOption = response;
      }
    })
  }

}
