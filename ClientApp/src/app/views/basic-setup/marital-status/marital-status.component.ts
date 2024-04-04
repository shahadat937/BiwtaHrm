import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { MaritalStatusService } from '../service/marital-status.service';

@Component({
  selector: 'app-marital-status',
  templateUrl: './marital-status.component.html',
  styleUrl: './marital-status.component.scss'
})
export class MaritalStatusComponent  implements OnInit,OnDestroy,AfterViewInit{
  percentage = 0;
  btnText:string | undefined;
  @ViewChild("maritalStatusForm", { static: true }) maritalStatusForm!: NgForm;
  subscription: Subscription = new Subscription;
  displayedColumns: string[] = ['slNo','maritalStatusName', 'isActive','Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
constructor(
  public maritalStatusService:MaritalStatusService,
  private route: ActivatedRoute,
  private router: Router,
  private confirmService: ConfirmService,
  private toastr: ToastrService
  )
  {
  //  const id = this.route.snapshot.paramMap.get('bloodGroupId'); 

    this.route.paramMap.subscribe(params => {
      const id = params.get('maritalStatusId');
      if (id) {
        this.btnText = 'Update';
        this.maritalStatusService.find(+id).subscribe(
          res => {
            console.log(res);
            this.maritalStatusForm?.form.patchValue(res);
          }
        );
      }
      else {

        this.btnText = 'Submit';
      }
    });

  
 }
  ngOnInit(): void {
   
    this.getMaritalStatus();

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
    this.maritalStatusService.maritalStatus = {
      maritalStatusId:0,
      maritalStatusName:"",
      menuPosition:0,
      isActive:true 

    }

   }
   resetForm() {
    console.log(this.maritalStatusForm?.form.value )
    this.btnText = 'Submit';
    if (this.maritalStatusForm?.form != null) {
      console.log(this.maritalStatusForm?.form )
      this.maritalStatusForm.form.reset();
      this.maritalStatusForm.form.patchValue({
      maritalStatusId:0,
      maritalStatusName:"",
      menuPosition:0,
      isActive:true 

      });
    }

  }

  getMaritalStatus(){
   this.subscription=this.maritalStatusService.getAll().subscribe(item=>{
     this.dataSource=new MatTableDataSource(item);
     this.dataSource.paginator = this.paginator;
     this.dataSource.sort = this.matSort;

    });

  }
   onSubmit(form:NgForm){
    const id = this.maritalStatusForm.form.get('maritalStatusId')?.value;
    if (id) {
      this.maritalStatusService.update(+id,this.maritalStatusForm.value).subscribe((response:any) => {
        console.log(response)
        if(response.success){
          this.toastr.success('Successfully', 'Update',{ positionClass: 'toast-top-right' });
          this.getMaritalStatus()
          this.resetForm();
          this.router.navigate(["/bascisetup/marital-status"]);  
        }else{
          this.toastr.warning('', `${response.message}`,{ positionClass: 'toast-top-right' });
        }
        
      }, err => {
        console.log(err)
      })
    }else{
   this.subscription=this.maritalStatusService.submit(form?.value).subscribe((response:any)=>{
    if(response.success){
      this.toastr.success('Successfully', `${response.message}`,{ positionClass: 'toast-top-right' });
      this.getMaritalStatus()
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
        this.maritalStatusService.delete(element.maritalStatusId).subscribe(res=>{
          this.getMaritalStatus()
        },(err) => { 
       console.log(err)
        });
      }
    })
   
    
  }
}
