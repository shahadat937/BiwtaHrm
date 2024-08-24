import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { SectionService } from '../service/section.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-section',
  templateUrl: './section.component.html',
  styleUrl: './section.component.scss'
})
export class SectionComponent implements OnInit, OnDestroy, AfterViewInit {
  //grades: any[] = [];
  editMode: boolean = false;
  banks: any[] = [];

  btnText: string | undefined;
  loading = false;
  @ViewChild('SectionForm', { static: true }) SectionForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'sectionName',  'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public sectionService: SectionService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService

  ) { }
  ngOnInit(): void {
    this.getAllSections();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {

      const id = params.get('sectionId');

      if (id) {
        this.btnText = 'Update';
        this.sectionService.find(+id).subscribe((res) => {
          this.SectionForm?.form.patchValue(res);
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
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }


  initaialBankBranch(form?: NgForm) {

    if (form != null) form.resetForm();
    this.sectionService.sections = {
      sectionId: 0,
        sectionName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    if (this.SectionForm?.form != null) {
      this.SectionForm.form.reset();
      this.SectionForm.form.patchValue({
        sectionId: 0,
        sectionName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/officeSetup/section']);
  }
  getAllSections() {
    this.subscription = this.sectionService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.sectionService.cachedData = [];
    const id = form.value.sectionId;
    const action$ = id
      ? this.sectionService.update(id, form.value)
      : this.sectionService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {

      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllSections();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/officeSetup/section']);
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
          this.sectionService.delete(element.sectionId).subscribe(
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
