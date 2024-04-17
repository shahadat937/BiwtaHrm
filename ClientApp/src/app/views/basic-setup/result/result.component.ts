import { ResultService } from './../service/result.service';
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

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrl: './result.component.scss'
})
export class ResultComponent  implements OnInit,OnDestroy,AfterViewInit{
  btnText:string | undefined;
  @ViewChild("ResultForm", { static: true }) ResultForm!: NgForm;
  subscription: Subscription = new Subscription;
  displayedColumns: string[] = ['slNo','resultName', 'isActive','Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
constructor(
  public resultService:ResultService,
  private snackBar: MatSnackBar,
  private route: ActivatedRoute,
  private router: Router,
  private confirmService: ConfirmService,
  private toastr: ToastrService
  )
  {  
 }
  ngOnInit(): void {
   
    this.getALlResult();
    this.handleRouteParams();

  }
  handleRouteParams(){
    this.route.paramMap.subscribe(params => {
      const id = params.get('resultId');
      if (id) {
        this.btnText = 'Update';
        this.resultService.find(+id).subscribe(
          res => {
            this.ResultForm?.form.patchValue(res);
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
    this.btnText = 'Submit';
    if (this.ResultForm?.form != null) {
      this.ResultForm.form.reset();
      this.ResultForm.form.patchValue({
        resultId:0,
        resultName:"",
        menuPosition:0,
        isActive:true

      });
    }
    this.router.navigate(['/bascisetup/result']);

  }

  getALlResult(){
   this.subscription=this.resultService.getAll().subscribe(item=>{
     this.dataSource=new MatTableDataSource(item);
     this.dataSource.paginator = this.paginator;
     this.dataSource.sort = this.matSort;

    });

  }
   onSubmit(form:NgForm){
    this.resultService.cachedData = [];
    const id = this.ResultForm.form.get('resultId')?.value;
    if (id) {
      this.resultService.update(+id,this.ResultForm.value).subscribe((response:any) => {
        if(response.success){
          this.toastr.success('Successfully', 'Update',{ positionClass: 'toast-top-right' });
          this.getALlResult()
          this.resetForm();
          this.router.navigate(["/bascisetup/result"]);  
        }else{
          this.toastr.warning('', `${response.message}`,{ positionClass: 'toast-top-right' });
        }
        
      }, err => {
        console.log(err)
      })
    }else{
   this.subscription=this.resultService.submit(form?.value).subscribe((response:any)=>{
    if(response.success){
      this.toastr.success('Successfully', `${response.message}`,{ positionClass: 'toast-top-right' });
      this.getALlResult()
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
        this.resultService.delete(element.resultId).subscribe(res=>{
          const index = this.dataSource.data.indexOf(element);
          if (index !== -1) {
            this.dataSource.data.splice(index, 1);
            this.dataSource = new MatTableDataSource(
              this.dataSource.data
            );
          }
        },(err) => { 
          this.toastr.error('Somethig Wrong ! ', ` `, {
            positionClass: 'toast-top-right',})
       console.log(err)
        });
      }
    })
   
    
  }
}