import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { GenderService } from '../service/gender.service';

@Component({
  selector: 'app-gender',
  templateUrl: './gender.component.html',
  styleUrl: './gender.component.scss',
})
export class GenderComponent implements OnInit, OnDestroy {
  private actionSubscription: Subscription | undefined;
  btnText: string | undefined;
  @ViewChild('genderForm', { static: true }) genderForm!: NgForm;
  displayedColumns: string[] = ['slNo', 'genderName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public genderService: GenderService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getALlGender();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('genderId');
      if (id) {
        this.btnText = 'Update';
        this.genderService.getById(+id).subscribe((res) => {
          this.genderForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
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
    this.genderService.gender = {
      genderId: 0,
      genderName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.genderForm?.form != null) {
      this.genderForm.form.reset();
      this.genderForm.form.patchValue({
        genderId: 0,
        genderName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/personalInfoSetup/gender']);
  }

  getALlGender() {
    this.actionSubscription = this.genderService.getAll().subscribe(
      (item) => {
        this.dataSource = new MatTableDataSource(item);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.matSort;
      },
      (err) => {
        this.toastr.error('Somethig Wrong ! ', ` `, {
          positionClass: 'toast-top-right',
        });
      }
    );
  }

  // onSubmit(form: NgForm): void {
  //   this.genderService.cachedData = [];
  //   const id = form.value.genderId;
  //   const action$ = id
  //     ? this.genderService.update(id, form.value)
  //     : this.genderService.submit(form.value);

  //   this.actionSubscription = action$.subscribe(
  //     (response: any) => {
  //       if (response.success) {
  //         const successMessage = id ? 'Update' : 'Successfully';
  //         this.toastr.success(successMessage, `${response.message}`, {
  //           positionClass: 'toast-top-right',
  //         });
  //         this.getALlGender();
  //         this.resetForm();
  //         if (!id) {
  //           this.router.navigate(['/bascisetup/gender']);
  //         }
  //       } else {
  //         this.toastr.warning('', `${response.message}`, {
  //           positionClass: 'toast-top-right',
  //         });
  //       }
  //     },
  //     (err) => {
  //       this.toastr.warning('', 'Error submitting punishment:', {
  //         positionClass: 'toast-top-right',
  //       });
  //     }
  //   );
  // }
  onSubmit(form: NgForm): void {
    this.genderService.cachedData = [];
    const id = form.value.genderId;
    const action$ = id
      ? this.genderService.update(id, form.value)
      : this.genderService.submit(form.value);

    this.actionSubscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlGender();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/personalInfoSetup/gender']);
        }
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
    });
  }
  delete(element: any) {
    this.actionSubscription = this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((confirm) => {
        if (confirm) {
          this.actionSubscription = this.genderService
            .delete(element.genderId)
            .subscribe(
              (res) => {
                const index = this.dataSource.data.indexOf(element);
                if (index !== -1) {
                  this.dataSource.data.splice(index, 1);
                  this.dataSource = new MatTableDataSource(
                    this.dataSource.data
                  );
                }
                this.toastr.success('Delete sucessfully ! ', ` `, {
                  positionClass: 'toast-top-right',})
              },
              (err) => {
                console.log(err);
              }
            );
        }
      });
  }
}
