import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { ChildStatusService } from '../service/child-status.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-child-status',
  templateUrl: './child-status.component.html',
  styleUrl: './child-status.component.scss'
})
export class ChildStatusComponent implements OnInit,OnDestroy,AfterViewInit{
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText:string | undefined;
  @ViewChild("ChildStatusForm", { static: true }) ChildStatusForm!: NgForm;
  subscription: Subscription = new Subscription;
  displayedColumns: string[] = ['slNo','childStatusName', 'isActive','Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
constructor(
  public childStatusService:ChildStatusService,
  private route: ActivatedRoute,
  private router: Router,
  private confirmService: ConfirmService,
  private toastr: ToastrService
  )
  {
  //  const id = this.route.snapshot.paramMap.get('bloodGroupId'); 

    this.route.paramMap.subscribe(params => {
      const id = params.get('childStatusId');
      if (id) {
        this.btnText = 'Update';
        this.childStatusService.find(+id).subscribe(
          res => {
            console.log(res);
            this.ChildStatusForm?.form.patchValue(res);
          }
        );
      }
      else {

        this.btnText = 'Submit';
      }
    });

  
 }
  ngOnInit(): void {
   
    this.getALlChildStatus();

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
    this.childStatusService.childStatus = {
      childStatusId:0,
      childStatusName:"",
      menuPosition:0,
      isActive:true 

    }

   }
   resetForm() {
    console.log(this.ChildStatusForm?.form.value )
    this.btnText = 'Submit';
    if (this.ChildStatusForm?.form != null) {
      console.log(this.ChildStatusForm?.form )
      this.ChildStatusForm.form.reset();
      this.ChildStatusForm.form.patchValue({
        childStatusId:0,
        childStatusName:"",
        menuPosition:0,
        isActive:true

      });
    }

  }

  getALlChildStatus(){
   this.subscription=this.childStatusService.getAll().subscribe(item=>{
     this.dataSource=new MatTableDataSource(item);
     this.dataSource.paginator = this.paginator;
     this.dataSource.sort = this.matSort;

    });

  }
   onSubmit(form:NgForm){
    const id = this.ChildStatusForm.form.get('childStatusId')?.value;
    if (id) {
      this.childStatusService.update(+id,this.ChildStatusForm.value).subscribe((response:any) => {
        console.log(response)
        if(response.success){
          this.toastr.success('Successfully', 'Update',{ positionClass: 'toast-top-right' });
          this.getALlChildStatus()
          this.resetForm();
          this.router.navigate(["/bascisetup/child-status"]);  
        }else{
          this.toastr.warning('', `${response.message}`,{ positionClass: 'toast-top-right' });
        }
        
      }, err => {
        console.log(err)
      })
    }else{
   this.subscription=this.childStatusService.submit(form?.value).subscribe((response:any)=>{
    if(response.success){
      this.toastr.success('Successfully', `${response.message}`,{ positionClass: 'toast-top-right' });
      this.getALlChildStatus()
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
        this.childStatusService.delete(element.childStatusId).subscribe(res=>{
          this.getALlChildStatus()
        },(err) => { 
       console.log(err)
        });
      }
    })
   
    
  }
}

