import { PunishmentService } from './../service/Punishment.service';
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
  selector: 'app-punishment',
  templateUrl: './punishment.component.html',
  styleUrl: './punishment.component.scss'
})
export class PunishmentComponent implements OnInit,OnDestroy,AfterViewInit{
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText:string | undefined;
  @ViewChild("PunishmentForm", { static: true }) PunishmentForm!: NgForm;
  subscription: Subscription = new Subscription;
  displayedColumns: string[] = ['slNo','punishmentName', 'isActive','Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
constructor(
  public punishmentService:PunishmentService,
  private snackBar: MatSnackBar,
  private route: ActivatedRoute,
  private router: Router,
  private confirmService: ConfirmService,
  private toastr: ToastrService
  )
  {
    //  const id = this.route.snapshot.paramMap.get('bloodGroupId'); 
  
      this.route.paramMap.subscribe(params => {
        const id = params.get('punishmentId');
        if (id) {
          this.btnText = 'Update';
          this.punishmentService.find(+id).subscribe(
            res => {
              console.log(res);
              this.PunishmentForm?.form.patchValue(res);
            }
          );
        }
        else {
  
          this.btnText = 'Submit';
        }
      });
  
    
   }
    ngOnInit(): void {
     
      this.getALlPunishment();
  
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
    initaialPunishment(form?:NgForm){
      if(form!=null)
      form.resetForm();
      this.punishmentService.punishments = {
        punishmentId:0,
        punishmentName:"",
        menuPosition: 0,
        isActive:true
  
      }
  
     }
     resetForm() {
      console.log(this.PunishmentForm?.form.value )
      this.btnText = 'Submit';
      if (this.PunishmentForm?.form != null) {
        console.log(this.PunishmentForm?.form )
        this.PunishmentForm.form.reset();
        this.PunishmentForm.form.patchValue({
          punishmentId:0,
          punishmentName:"",
          menuPosition:0,
          isActive:true
  
        });
      }
  
    }
  
    getALlPunishment(){
     this.subscription=this.punishmentService.getAll().subscribe(item=>{
       this.dataSource=new MatTableDataSource(item);
       this.dataSource.paginator = this.paginator;
       this.dataSource.sort = this.matSort;
  
      });
  
    }
     onSubmit(form:NgForm){
      const id = this.PunishmentForm.form.get('punishmentId')?.value;
      if (id) {
        this.punishmentService.update(+id,this.PunishmentForm.value).subscribe((response:any) => {
          console.log(response)
          if(response.success){
            this.toastr.success('Successfully', 'Update',{ positionClass: 'toast-top-right' });
            this.getALlPunishment()
            this.resetForm();
            this.router.navigate(["/bascisetup/punishment"]);  
          }else{
            this.toastr.warning('', `${response.message}`,{ positionClass: 'toast-top-right' });
          }
          
        }, err => {
          console.log(err)
        })
      }else{
     this.subscription=this.punishmentService.submit(form?.value).subscribe((response:any)=>{
      if(response.success){
        this.toastr.success('Successfully', `${response.message}`,{ positionClass: 'toast-top-right' });
        this.getALlPunishment()
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
          this.punishmentService.delete(element.punishmentId).subscribe(res=>{
            this.getALlPunishment()
          },(err) => { 
         console.log(err)
          });
        }
      })
     
      
    }
  }
  