import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { RewardPunishmentSetupService } from '../service/reward-punishment-setup.service';

@Component({
  selector: 'app-reward-punishment-priority',
  templateUrl: './reward-punishment-priority.component.html',
  styleUrl: './reward-punishment-priority.component.scss'
})
export class RewardPunishmentPriorityComponent  implements OnInit, OnDestroy, AfterViewInit {

  btnText: string = 'Submit';
  loading = false;
  @ViewChild('RewardPriorityForm', { static: true }) RewardPriorityForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'name', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public rewardPunishmentSetupService: RewardPunishmentSetupService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getAllRewardPriority();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;
  }
  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.rewardPunishmentSetupService.rewardPunishmentPriority = {
      id : 0,
      name : "",
      menuPosition : 0,
      remark : "",
      isActive : true,
    };
  }

  resetForm() {
    this.btnText = 'Submit';
    if (this.RewardPriorityForm?.form != null) {
      this.RewardPriorityForm.form.reset();
      this.RewardPriorityForm.form.patchValue({
        id : 0,
        name : "",
        menuPosition : 0,
        remark : "",
        isActive : true,
      });
    }
  }

  getRewardPriorityById(id: number){
    this.subscription = this.rewardPunishmentSetupService.findRewardPriotiry(id).subscribe((item) => {
      if(item){
        this.btnText = "Update";
        this.RewardPriorityForm.form.patchValue(item);
        window.scrollTo(0, 0);
      }
    });
  }

  getAllRewardPriority() {
    this.subscription = this.rewardPunishmentSetupService.getAllRewardPriotiry().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.rewardPunishmentSetupService.cachedPriorityData = [];
    const id = form.value.id;
    const action$ = id
      ? this.rewardPunishmentSetupService.updateRewardPriority(id, form.value)
      : this.rewardPunishmentSetupService.submitRewardPriority(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllRewardPriority();
        this.resetForm();
        this.loading = false;
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
      this.loading = false;
    });
  }

  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.rewardPunishmentSetupService.deleteRewardPriority(element.id).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
            },
            (err) => {
              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
              console.log(err);
            }
          );
        }
      });
  }
}
