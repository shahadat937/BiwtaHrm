import { ScaleService } from './../service/Scale.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { AfterViewInit, Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { Subscription } from 'rxjs';
import { brandSet, cil3d, cil4k, cilAccountLogout, cilActionRedo, cilAirplaneMode, cilList, cilPaperPlane, cilPencil, cilShieldAlt, cilTrash } from '@coreui/icons';
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Scale } from 'chart.js';

@Component({
  selector: 'app-scale',
  templateUrl: './scale.component.html',
  styleUrl: './scale.component.scss'
})
export class ScaleComponent implements OnInit,OnDestroy,AfterViewInit{
  
  position = 'top-end';
  visible = false;
  percentage = 0;
  @ViewChild("ScaleForm", { static: true }) ScaleForm!: NgForm;
  subscription: Subscription = new Subscription;
  displayedColumns: string[] = ['slNo','scaleName','basicPay','gradeId','isActive','Action'];

  dataSource = new MatTableDataSource<any>();
  icons = {
    'cilList': cilList,
  'cilShieldAlt': cilShieldAlt,
  'cilPaperPlane': cilPaperPlane,
  'cil3d': cil3d,
  'cil4k': cil4k,
  'cilAccountLogout': cilAccountLogout,
  'cilActionRedo': cilActionRedo,
  'cilAirplaneMode': cilAirplaneMode,
  'cilPencil': cilPencil,
  'cilTrash': cilTrash,
  };
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
constructor(
  public ScaleService:ScaleService,
  )
  {

 }
  ngOnInit(): void {
    this.getALlScale();
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
  initaialScale(form?:NgForm){
    if(form!=null)
    form.resetForm();
    this.ScaleService.scales = {
      scaleId:0,
      scaleName:"",
      basicPay:0,
      gradeId:0,
      menuPosition: 0,
      isActive:true,
      gradeName:"",
    }

   }
   resetForm() {
    console.log(this.ScaleForm?.form.value )
    if (this.ScaleForm?.form != null) {
      console.log(this.ScaleForm?.form )
      this.ScaleForm.form.reset();
      this.ScaleForm.form.patchValue({
        scaleId:0,
        scaleName:"",
        basicPay:0,
        gradeId:0,
        menuPosition: 0,
        isActive:true,
        gradeName:"",
      });
    }

  }
  getALlScale(){
   this.subscription=this.ScaleService.getAll().subscribe(item=>{
     this.dataSource=new MatTableDataSource(item);
     this.dataSource.paginator = this.paginator;
     this.dataSource.sort = this.matSort;

    });

  }
   onSubmit(form:NgForm){
    this.subscription=this.ScaleService.submit(form?.value).subscribe(res=>{
      // this.snackBar.open('Information Inserted Successfully ', '', {
      //   duration: 2000,
      //   verticalPosition: 'top',
      //   horizontalPosition: 'right',
      //   panelClass: 'snackbar-success'
      // });
      this.toggleToast();
    this.getALlScale()
    this.resetForm();

   },err=>{
     console.log(err);
   })

  }
  delete(element:any){
    console.log(element)
    this.ScaleService.delete(element.scaleId).subscribe(res=>{
      this.getALlScale()
    },(err) => {

    });

  }
}
