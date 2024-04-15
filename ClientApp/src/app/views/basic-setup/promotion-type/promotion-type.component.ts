import { promotionTypeService } from './../service/PromotionType.service';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-promotion-type',
  templateUrl: './promotion-type.component.html',
  styleUrl: './promotion-type.component.scss'
})
export class PromotionTypeComponent implements OnInit,OnDestroy,AfterViewInit{
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText:string | undefined;
  @ViewChild("PromotionTypeForm", { static: true }) PromotionTypeForm!: NgForm;
  subscription: Subscription = new Subscription;
  displayedColumns: string[] = ['slNo','promotionTypeName', 'isActive','Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
constructor(
  public promotionTypeService:promotionTypeService,
  private snackBar: MatSnackBar,
  private route: ActivatedRoute,
  private router: Router,
  private confirmService: ConfirmService,
  private toastr: ToastrService
  )
  {
  //  const id = this.route.snapshot.paramMap.get('bloodGroupId'); 

    this.route.paramMap.subscribe(params => {
      const id = params.get('promotionTypeId');
      if (id) {
        this.btnText = 'Update';
        this.promotionTypeService.find(+id).subscribe(
          res => {
            console.log(res);
            this.PromotionTypeForm?.form.patchValue(res);
          }
        );
      }
      else {

        this.btnText = 'Submit';
      }
    });

  
 }
  ngOnInit(): void {
   
    this.getALlPromotionType();

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
  initaialPromotionType(form?:NgForm){
    if(form!=null)
    form.resetForm();
    this.promotionTypeService.promotionTypes = {
      promotionTypeId:0,
      promotionTypeName:"",
      menuPosition: 0,
      isActive:true

    }

   }
   resetForm() {
    console.log(this.PromotionTypeForm?.form.value )
    this.btnText = 'Submit';
    if (this.PromotionTypeForm?.form != null) {
      console.log(this.PromotionTypeForm?.form )
      this.PromotionTypeForm.form.reset();
      this.PromotionTypeForm.form.patchValue({
        promotionTypeId:0,
        promotionTypeName:"",
        menuPosition:0,
        isActive:true

      });
    }

  }

  getALlPromotionType(){
   this.subscription=this.promotionTypeService.getAll().subscribe(item=>{
     this.dataSource=new MatTableDataSource(item);
     this.dataSource.paginator = this.paginator;
     this.dataSource.sort = this.matSort;

    });

  }
   onSubmit(form:NgForm){
    const id = this.PromotionTypeForm.form.get('promotionTypeId')?.value;
    if (id) {
      this.promotionTypeService.update(+id,this.PromotionTypeForm.value).subscribe((response:any) => {
        console.log(response)
        if(response.success){
          this.toastr.success('Successfully', 'Update',{ positionClass: 'toast-top-right' });
          this.getALlPromotionType()
          this.resetForm();
          this.router.navigate(["/bascisetup/promotionType"]);  
        }else{
          this.toastr.warning('', `${response.message}`,{ positionClass: 'toast-top-right' });
        }
        
      }, err => {
        console.log(err)
      })
    }else{
   this.subscription=this.promotionTypeService.submit(form?.value).subscribe((response:any)=>{
    if(response.success){
      this.toastr.success('Successfully', `${response.message}`,{ positionClass: 'toast-top-right' });
      this.getALlPromotionType()
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
        this.promotionTypeService.delete(element.promotionTypeId).subscribe(res=>{
          this.getALlPromotionType()
        },(err) => { 
       console.log(err)
        });
      }
    })
   
    
  }
}
