import { getStyle } from '@coreui/utils';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { GradeTypeService } from '../service/GradeType.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-grade-type',
  templateUrl: './grade-type.component.html',
  styleUrl: './grade-type.component.scss'
})
export class GradeTypeComponent implements OnInit,OnDestroy,AfterViewInit{
  btnText:string | undefined;
  @ViewChild("GradeTypeForm", { static: true }) GradeTypeForm!: NgForm;
  subscription: Subscription = new Subscription;
  displayedColumns: string[] = ['slNo','gradeTypeName', 'isActive','Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
constructor(
  public gradeTypeService:GradeTypeService,
  private route: ActivatedRoute,
  private router: Router,
  private confirmService: ConfirmService,
  private toastr: ToastrService
  )
  {
  //  const id = this.route.snapshot.paramMap.get('bloodGroupId'); 
 }
  ngOnInit(): void {
   
    this.getALlGradeType();
    this.handleRouteParams()
  }
  handleRouteParams() {
    this.route.paramMap.subscribe(params => {
      const id = params.get('gradeTypeId');
      if (id) {
        this.btnText = 'Update';
        this.gradeTypeService.find(+id).subscribe(
          res => {
      
            this.GradeTypeForm?.form.patchValue(res);
          }
        );
      }
      else {

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
 
  initaialBloodGroup(form?:NgForm){
    if(form!=null)
    form.resetForm();
    this.gradeTypeService.gradeTypes = {
      gradeTypeId:0,
      gradeTypeName:"",
      menuPosition: 0,
      isActive:true

    }

   }
   resetForm() {
    this.btnText = 'Submit';
    if (this.GradeTypeForm?.form != null) {
      this.GradeTypeForm.form.reset();
      this.GradeTypeForm.form.patchValue({
        gradeTypeId:0,
        gradeTypeName:"",
        menuPosition:0,
        isActive:true

      });
    }
    this.router.navigate(['/bascisetup/grade-type']);

  }

  getALlGradeType(){
   this.subscription=this.gradeTypeService.getAll().subscribe(item=>{
     this.dataSource=new MatTableDataSource(item);
     this.dataSource.paginator = this.paginator;
     this.dataSource.sort = this.matSort;

    });

  }
   onSubmit(form:NgForm){
    this.gradeTypeService.cachedData = [];
    const id = this.GradeTypeForm.form.get('gradeTypeId')?.value;
    if (id) {
      this.gradeTypeService.update(+id,this.GradeTypeForm.value).subscribe((response:any) => {
        if(response.success){
          this.toastr.success('Successfully', 'Update',{ positionClass: 'toast-top-right' });
          this.getALlGradeType()
          this.resetForm();
          this.router.navigate(["/bascisetup/grade-type"]);  
        }else{
          this.toastr.warning('', `${response.message}`,{ positionClass: 'toast-top-right' });
        }
        
      }, err => {
        console.log(err)
      })
    }else{
   this.subscription=this.gradeTypeService.submit(form?.value).subscribe((response:any)=>{
    if(response.success){
      this.toastr.success('Successfully', `${response.message}`,{ positionClass: 'toast-top-right' });
      this.getALlGradeType()
      this.resetForm();
    }else{
      this.toastr.warning('', `${response.message}`,{ positionClass: 'toast-top-right' });
    }

   },err=>{
     console.log(err);
   })
    }

  }
  delete(element:any){
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item').subscribe(result=>{
      if (result) {
        this.gradeTypeService.delete(element.gradeTypeId).subscribe(res=>{
          const index = this.dataSource.data.indexOf(element);
          if (index !== -1) {
            this.dataSource.data.splice(index, 1);
            this.dataSource = new MatTableDataSource(
              this.dataSource.data
            );
          }
          this.toastr.success('Delete sucessfully ! ', ` `, {
            positionClass: 'toast-top-right',})
        },(err) => { 
          this.toastr.error('Somethig Wrong ! ', ` `, {
            positionClass: 'toast-top-right',})
       console.log(err)
        });
      }
    })
   
    
  }
}