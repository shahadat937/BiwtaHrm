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
import { TrainingTypeService } from './../service/trainingType.service';

import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
@Component({
  selector: 'app-training',
  templateUrl: './training.component.html',
  styleUrl: './training.component.scss',
})
export class TrainingComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  loading = false;
  @ViewChild('TrainingTypeForm', { static: true }) TrainingTypeForm!: NgForm;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = [
    'slNo',
    'trainingTypeName',
    'isActive',
    'Action',
  ];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public trainingTypeService: TrainingTypeService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('trainingTypeId');
      if (id) {
        this.btnText = 'Update';
        this.trainingTypeService.getById(+id).subscribe((res) => {
          this.TrainingTypeForm?.form.patchValue(res);
          
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  ngOnInit(): void {
    this.getALlTrainingTypes();
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;
  }
  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
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
    this.trainingTypeService.trainingTypes = {
      trainingTypeId: 0,
      trainingTypeName: '',
      //districtName:"",
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    // console.log(this.TrainingTypeForm?.form.value);
    this.btnText = 'Submit';
    if (this.TrainingTypeForm?.form != null) {
      // console.log(this.TrainingTypeForm?.form);
      this.TrainingTypeForm.form.reset();
      this.TrainingTypeForm.form.patchValue({
        trainingTypeId: 0,
        trainingTypeName: '',
        //  districtName:"",
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/trainingSetup/training']);
  }

  getALlTrainingTypes() {
    this.subscription.push(
    this.trainingTypeService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }

  onSubmit(form: NgForm):void {
    this.loading=true;
    this.trainingTypeService.cachedData = [];
    const id = this.TrainingTypeForm.form.get('trainingTypeId')?.value;
    if (id) {
      this.trainingTypeService.update(+id, this.TrainingTypeForm.value).subscribe(
        (response: any) => {
      
          this.loading=false;
          if (response.success) {
            this.toastr.success('Successfully', 'Update', {
              positionClass: 'toast-top-right',
            });
            this.getALlTrainingTypes();
            this.resetForm();
            if(!id){
              this.router.navigate(['/trainingSetup/trainingType']);
            }
            this.loading=false
          } else {
            this.toastr.warning('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
          }
        },
        (err) => {
          console.log(err);
        }
      );
    } else {
      
      this.trainingTypeService.submit(form?.value).subscribe(
        (response: any) => {
          if (response.success) {
            this.toastr.success('Successfully', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
            this.getALlTrainingTypes();
            this.resetForm();
          } else {
            this.toastr.warning('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
          }
        },
        (err) => {
          console.log(err);
        }
      );
    }
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.trainingTypeService.cachedData = [];
          console.log('trainingType id ' +element.trainingTypeId);
          this.trainingTypeService.delete(element.trainingTypeId).subscribe(
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
                    positionClass: 'toast-top-right',})
                }
              );
            }
          });
      }
    }
    