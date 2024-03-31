import { filter } from 'rxjs/operators';
import { Subscription } from 'rxjs';
import { ResultService } from 'src/app/views/basic-setup/service/result.service';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { cil3d, cil4k, cilAccountLogout, cilActionRedo, cilAirplaneMode, cilList, cilPaperPlane, cilPencil, cilShieldAlt, cilTrash } from '@coreui/icons';
import { NgForm, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrl: './result.component.scss'
})
export class ResultComponent implements OnInit,OnDestroy,AfterViewInit{
  position = 'top-end';
  visible = false;
  percentage = 0;
  @ViewChild("ResultForm", { static: true }) ResultForm!: NgForm;
  subscription: Subscription = new Subscription;
  displayedColumns: string[] = ['slNo','resultName', 'isActive','Action'];

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
  public resultService:ResultService,
  private snackBar: MatSnackBar
  )
  {

 }
  ngOnInit(): void {
    this.getALlResult();
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
  initaialResult(form?:NgForm){
    if(form!=null)
    form.resetForm();
    this.resultService.result = {
      resultId:0,
      resultName:"",
      menuPosition: 0,
      isActive:true
      
    }
    
   }
   resetForm() {
    console.log(this.ResultForm?.form.value )
    if (this.ResultForm?.form != null) {
      console.log(this.ResultForm?.form )
      this.ResultForm.form.reset();
      this.ResultForm.form.patchValue({
        resultId:0,
        resultName:"",
        menuPosition:0,
        isActive:true
       
      });
    }
    
  }
  getALlResult(){ 
   this.subscription=this.resultService.getAll().subscribe(item=>{
     this.dataSource=new MatTableDataSource(item);
     this.dataSource.paginator = this.paginator;
     this.dataSource.sort = this.matSort;
   
    });
   
  }
   onSubmit(form:NgForm){
    this.subscription=this.resultService.submit(form?.value).subscribe ( res=>{ 
      // this.snackBar.open('Information Inserted Successfully ', '', {
      //   duration: 2000,
      //   verticalPosition: 'top',
      //   horizontalPosition: 'right',
      //   panelClass: 'snackbar-success'
      // });  
      this.toggleToast();
    this.getALlResult()
    this.resetForm();
   },err=>{
     console.log(err);
   })
 
  }
  delete(element:any){
    console.log(element)
    this.resultService.delete(element.resultId).subscribe(res=>{
      this.getALlResult()
    },(err) => { 
    });
    
  }
  
}
