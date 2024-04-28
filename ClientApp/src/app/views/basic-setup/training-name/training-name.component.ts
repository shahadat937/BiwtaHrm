import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import {TrainingNameService} from '../service/trainingName.service';



@Component({
  selector: 'app-training-name',
  templateUrl: './training-name.component.html',
  styleUrl: './training-name.component.scss'
})
export class TrainingNameComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  @ViewChild('TrainingNameForm', { static: true }) TrainingNameForm!: NgForm;
  loading = false;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'trainingNames', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public trainingNameService: TrainingNameService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    //  const id = this.route.snapshot.paramMap.get('trainingNameId');
  }
  ngOnInit(): void {
    this.getALlTrainingNames();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('trainingNameId');
      if (id) {
        this.btnText = 'Update';
        this.trainingNameService.find(+id).subscribe((res) => {
          this.TrainingNameForm?.form.patchValue(res);
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

  initaialTrainingName(form?: NgForm) {
    if (form != null) form.resetForm();
    this.trainingNameService.trainingNameses = {
      trainingNameId: 0,
      trainingNames: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.TrainingNameForm?.form != null) {
      this.TrainingNameForm.form.reset();
      this.TrainingNameForm.form.patchValue({
        trainingNameId: 0,
        trainingNames: '',
        menuPosition: 0,
        isActive: true,
      });
    }
  }

  getALlTrainingNames() {
    this.subscription = this.trainingNameService.getAll().subscribe((item) => {
      console.log(item)
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.trainingNameService.cachedData = [];
    const id = form.value.trainingNameId;
    const action$ = id
      ? this.trainingNameService.update(id, form.value)
      : this.trainingNameService.submit(form.value);
    
    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlTrainingNames();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/trainingName']);
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
          this.trainingNameService.delete(element.trainingNameId).subscribe(
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
