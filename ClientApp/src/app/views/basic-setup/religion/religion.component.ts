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
import { ReligionService } from '../service/religion.service';

@Component({
  selector: 'app-religion',
  templateUrl: './religion.component.html',
  styleUrl: './religion.component.scss'
})
export class ReligionComponent implements OnInit, OnDestroy{
  private actionSubscription: Subscription | undefined;
  btnText: string | undefined;
  @ViewChild('religionForm', { static: true }) religionForm!: NgForm;
  displayedColumns: string[] = ['slNo', 'religionName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public religionService: ReligionService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
   
  }
  ngOnInit(): void {
    this.getALlReligion();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('religionId');
      if (id) {
        this.btnText = 'Update';
        this.religionService.getById(+id).subscribe((res) => {
          this.religionForm?.form.patchValue(res);
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
    this.religionService.religion = {
      religionId: 0,
      religionName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.religionForm?.form != null) {
      this.religionForm.form.reset();
      this.religionForm.form.patchValue({
        religionId: 0,
        religionName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bascisetup/religion']);
  }

  getALlReligion() {
    this.actionSubscription = this.religionService.getAll().subscribe(
      (item) => {
        this.dataSource = new MatTableDataSource(item);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.matSort;
      },
      (err) => {}
    );
  }

  onSubmit(form: NgForm): void {
    const id = form.value.religionId;
    const action$ = id
      ? this.religionService.update(id, form.value)
      : this.religionService.submit(form.value);

    this.actionSubscription = action$.subscribe(
      (response: any) => {
        if (response.success) {
          const successMessage = id ? 'Update' : 'Successfully';
          this.toastr.success(successMessage, `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.getALlReligion();
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
        this.toastr.warning('', 'Error submitting punishment:', {
          positionClass: 'toast-top-right',
        });
      }
    );
  }
  delete(element: any) {
    this.actionSubscription =this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((confirm) => {
        if (confirm) {
          this.actionSubscription = this.religionService
            .delete(element.religionId)
            .subscribe(
              (res) => {
                this.getALlReligion();
              },
              (err) => {
                console.log(err);
              }
            );
        }
      });
  }
}
