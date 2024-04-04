import { GradeClass } from './../model/GradeClass';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { GradeService } from '../service/Grade.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';
import{GradeClassService} from '../service/GradeClass.service'
import{GradeTypeService} from '../service/GradeType.service'
import { GradeType } from '../model/GradeType';
import { Grade } from '../model/Grade';

@Component({
  selector: 'app-grade',
  templateUrl: './grade.component.html',
  styleUrl: './grade.component.scss'
})
export class GradeComponent implements OnInit, OnDestroy, AfterViewInit{
  
  gradeType: any[] = [];
  gradeClass: any[] = [];
  editMode: boolean = false;
  grades: any = []


  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText:string | undefined;
  @ViewChild("GradeForm", { static: true }) GradeForm!: NgForm;
  subscription: Subscription = new Subscription;
  displayedColumns: string[] = ['slNo', 'gradeName', 'gradeTypeId', 'gradeClassId', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(

    public gradeService: GradeService,
    private snackBar: MatSnackBar,
    private gradeServiceClass:GradeClassService,
    private gradeTypeService:GradeTypeService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ){


    this.route.paramMap.subscribe(params => {
      const id = params.get('gradeId');
      if (id) {
        this.btnText = 'Update';
        this.gradeService.find(+id).subscribe(
          res => {
            console.log(res);
            this.GradeForm?.form.patchValue(res);
          }
        );
      }
      else {

        this.btnText = 'Submit';
      }
    });
  }

ngOnInit(): void {
  this.getALlGrade();
  this.SelectedModelGradeClass();
  this.SelectedModelGradeType();
}
// loadGrades() {
//   this.gradeService.getGrade_cls_type_Vw().subscribe(data => {
//     this.gradeClass;
//     this.gradeTypes;
//   });
// }

SelectedModelGradeClass(){
  this.gradeServiceClass.getSelectedGradeClass().subscribe(res=>{
   //console.log(res)
   this.grades = res;
  })
}
SelectedModelGradeType(){
  this.gradeTypeService.getSelectGradeType().subscribe(res=>{
   //console.log(res)
   this.grades = res;
  })
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
initaialGrade(form?: NgForm) {
  if (form != null)
    form.resetForm();
  this.gradeService.grades = {
    gradeId: 0,
    gradeName: "",
    gradeTypeId: 0,
    gradeClassId: 0,
    menuPosition: 0,
    isActive: true,
  }

}
resetForm() {
  console.log(this.GradeForm?.form.value)
  if (this.GradeForm?.form != null) {
    console.log(this.GradeForm?.form)
    this.GradeForm.form.reset();
    this.GradeForm.form.patchValue({
      gradeId: 0,
      gradeName: "",
      gradeTypeId: 0,
      gradeClassId: 0,
      menuPosition: 0,
      isActive: true,
      
    });
  }

}
getALlGrade() {
  this.subscription = this.gradeService.getAll().subscribe(item => {
    this.dataSource = new MatTableDataSource(item);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;

  });

}
onSubmit(form: NgForm) {
  const id = this.GradeForm.form.get('gradeId')?.value;
  if (id) {
    this.gradeService.update(+id, this.GradeForm.value).subscribe((response: any) => {
      console.log(response)
      if (response.success) {
        this.toastr.success('Successfully', 'Update', { positionClass: 'toast-top-right' });
        this.getALlGrade()
        this.resetForm();
        this.router.navigate(["/bascisetup/grade"]);
      } else {
        this.toastr.warning('', `${response.message}`, { positionClass: 'toast-top-right' });
      }

    }, err => {
      console.log(err)
    })
  } else {
    this.subscription = this.gradeService.submit(form?.value).subscribe((response: any) => {
      if (response.success) {
        this.toastr.success('Successfully', `${response.message}`, { positionClass: 'toast-top-right' });
        this.getALlGrade()
        this.resetForm();
      } else {
        this.toastr.warning('', `${response.message}`, { positionClass: 'toast-top-right' });
      }

    }, err => {
      console.log(err);
    })
  }

}
delete(element: any) {
  this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item').subscribe(result => {
    if (result) {
      this.gradeService.delete(element.gradeId).subscribe(res => {
        this.getALlGrade()
      }, (err) => {
        console.log(err)
      });
    }
  })


}
}
