import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';

import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';

import {CompetenceService} from '../service/competence.service';

@Component({
  selector: 'app-competence',
  templateUrl: './competence.component.html',
  styleUrl: './competence.component.scss'
})
export class CompetenceComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  headerText: string | undefined;
  @ViewChild('CompetenceForm', { static: true }) CompetenceForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'competenceName', 'isActive', 'Action'];
  loading = false;

  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public competenceService: CompetenceService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getALlCompetences();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('competenceId');
      if (id) {
        this.btnText = 'Update';
        this.headerText = 'Update Competence';
        this.competenceService.getById(+id).subscribe((res) => {
          this.CompetenceForm?.form.patchValue(res);
        });
      } else {
        this.resetForm();
        this.headerText = 'Add Competence';
        this.btnText = 'Submit';
      }
    });
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
  toggleToast() {
    this.visible = !this.visible;
  }

  onVisibleChange($event: boolean) {
    this.visible = $event;
    this.percentage = !this.visible ? 0 : this.percentage;
  }

  onTimerChange($event: number) {
    this.percentage = $event * 25;
  }
  initaialUpazila(form?: NgForm) {
    if (form != null) form.resetForm();
    this.competenceService.competences = {
      competenceId: 0,
      competenceName: '',
      //districtName:"",
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    // console.log(this.CompetenceForm?.form.value);
    this.btnText = 'Submit';
    if (this.CompetenceForm?.form != null) {
      // console.log(this.CompetenceForm?.form);
      this.CompetenceForm.form.reset();
      this.CompetenceForm.form.patchValue({
        competenceId: 0,
        competenceName: '',
        //  districtName:"",
        menuPosition: 0,
        isActive: true,
      });
    }
  }

  getALlCompetences() {
    this.subscription = this.competenceService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  // onSubmit(form: NgForm) {
  //   const id = this.CompetenceForm.form.get('competenceId')?.value;
  //   if (id) {
  //     this.competenceService.update(+id, this.CompetenceForm.value).subscribe(
  //       (response: any) => {
  //         //console.log(response);
  //         if (response.success) {
  //           this.toastr.success('Successfully', 'Update', {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getALlCompetences();
  //           this.resetForm();
  //           this.router.navigate(['/bascisetup/competence']);
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
  //     this.subscription = this.competenceService.submit(form?.value).subscribe(
  //       (response: any) => {
  //         if (response.success) {
  //           this.toastr.success('Successfully', `${response.message}`, {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getALlCompetences();
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
    this.loading = false;
    this.competenceService.cachedData = [];
    const id = form.value.competenceId;
    const action$ = id
      ? this.competenceService.update(id, form.value)
      : this.competenceService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlCompetences();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/competence']);
        }
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
          console.log('competence id ' + element.competenceId);
          this.competenceService.delete(element.competenceId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
              this.toastr.success('Delete sucessfully ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            },
            (err) => {
              // console.log(err);

              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            }
          );
        }
      });
  }
}
