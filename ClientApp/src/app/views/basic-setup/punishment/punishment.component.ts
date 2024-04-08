import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { PunishmentService } from './../service/Punishment.service';

@Component({
  selector: 'app-punishment',
  templateUrl: './punishment.component.html',
  styleUrl: './punishment.component.scss',
})
export class PunishmentComponent implements OnInit, OnDestroy, AfterViewInit {
  private actionSubscription: Subscription | undefined;
  position = 'top-end';

  btnText: string | undefined;
  @ViewChild('PunishmentForm', { static: true }) PunishmentForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'punishmentName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public punishmentService: PunishmentService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    //  const id = this.route.snapshot.paramMap.get('bloodGroupId');

 
  }
  ngOnInit(): void {
    this.getALlPunishment();
    this.handleRouteParams();
  }
  handleRouteParams(){
    this.route.paramMap.subscribe((params) => {
      const id = params.get('punishmentId');
      if (id) {
        this.btnText = 'Update';
        this.punishmentService.find(+id).subscribe((res) => {
          console.log(res);
          this.PunishmentForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;
  }
  ngOnDestroy() {
    if (this.actionSubscription) {
      this.actionSubscription.unsubscribe();
    }
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }
  
  initaialPunishment(form?: NgForm) {
    if (form != null) form.resetForm();
    this.punishmentService.punishments = {
      punishmentId: 0,
      punishmentName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    console.log(this.PunishmentForm?.form.value);
    this.btnText = 'Submit';
    if (this.PunishmentForm?.form != null) {
      console.log(this.PunishmentForm?.form);
      this.PunishmentForm.form.reset();
      this.PunishmentForm.form.patchValue({
        punishmentId: 0,
        punishmentName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bascisetup/punishment']);
  }

  getALlPunishment() {
    this.actionSubscription = this.punishmentService.getAll().subscribe(
      (item) => {
        this.dataSource = new MatTableDataSource(item);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.matSort;
      },
      (err) => {}
    );
  }
  // onSubmit(form: NgForm) {
  //   const id = this.PunishmentForm.form.get('punishmentId')?.value;
  //   if (id) {
  //     this.punishmentService.update(+id, this.PunishmentForm.value).subscribe(
  //       (response: any) => {
  //         console.log(response);
  //         if (response.success) {
  //           this.toastr.success('Successfully', 'Update', {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getALlPunishment();
  //           this.resetForm();
  //           this.router.navigate(['/bascisetup/punishment']);
  //         } else {
  //           this.toastr.warning('', `${response.message}`, {
  //             positionClass: 'toast-top-right',
  //           });
  //         }
  //       },
  //       (err) => {
  //         console.log(err);
  //       }
  //     );
  //   } else {
  //     this.subscription = this.punishmentService.submit(form?.value).subscribe(
  //       (response: any) => {
  //         if (response.success) {
  //           this.toastr.success('Successfully', `${response.message}`, {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getALlPunishment();
  //           this.resetForm();
  //         } else {
  //           this.toastr.warning('', `${response.message}`, {
  //             positionClass: 'toast-top-right',
  //           });
  //         }
  //       },
  //       (err) => {
  //         console.log(err);
  //       }
  //     );
  //   }
  // }
  onSubmit(form: NgForm): void {
    this.punishmentService.cachedData = [];
    const id = form.value.punishmentId;
    const action$ = id
      ? this.punishmentService.update(id, form.value)
      : this.punishmentService.submit(form.value);

    this.actionSubscription = action$.subscribe(
      (response: any) => {
        if (response.success) {
          const successMessage = id ? 'Update' : 'Successfully';
          this.toastr.success(successMessage, `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.getALlPunishment();
          this.resetForm();
          if (!id) {
            this.router.navigate(['/bascisetup/punishment']);
          }
        } else {
          this.toastr.warning('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
        }
      },
      (err) => {
        console.error('Error submitting punishment:', err);
        // Handle error (e.g., show error message to user)
      }
    );
  }
  delete(element: any) {
    this.actionSubscription =this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((confirm) => {
        if (confirm) {
          this.actionSubscription = this.punishmentService
            .delete(element.punishmentId)
            .subscribe(
              (res) => {
                const index = this.dataSource.data.indexOf(element);
                if (index !== -1) {
                  this.dataSource.data.splice(index, 1);
                  this.dataSource = new MatTableDataSource(
                    this.dataSource.data
                  );
                }
              },
              (err) => {
                console.log(err);
              }
            );
        }
      });
  }
}
