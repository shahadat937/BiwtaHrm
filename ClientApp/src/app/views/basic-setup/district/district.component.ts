import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { AfterViewInit, Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { Subscription } from 'rxjs';
import { brandSet, cil3d, cil4k, cilAccountLogout, cilActionRedo, cilAirplaneMode, cilList, cilPaperPlane, cilPencil, cilShieldAlt, cilTrash } from '@coreui/icons';
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import {DistrictService} from './../service/district.service'
import { ActivatedRoute } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
@Component({
  selector: 'app-district',
  templateUrl: './district.component.html',
  styleUrl: './district.component.scss'
})
export class DistrictComponent implements OnInit,OnDestroy,AfterViewInit{
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText:string|undefined;
  @ViewChild("DistrictForm", { static: true }) DistrictForm!: NgForm;
  subscription: Subscription = new Subscription;
  displayedColumns: string[] = ['slNo','districtName', 'isActive','Action'];

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
  public districtService:DistrictService,
  private snackBar: MatSnackBar,
  private route :ActivatedRoute,
  private confirmService:ConfirmService,
  )
  {
    this.route.paramMap.subscribe(params=>{
      const id = params.get('districtId');
      if(id){
        this.btnText='Update';
        this.districtService.getById(+id).subscribe(
          res=>{
            console.log(res);
            this.DistrictForm?.form.patchValue(res);
          }
        );
      }
      else{
        this.btnText='Submit'
      }
    });

 }
  ngOnInit(): void {
    this.getALlDistrict();
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
  initaialDistrict(form?:NgForm){
    if(form!=null)
    form.resetForm();
    this.districtService.districts={
      districtId:0,
      districtName:"",
      menuPosition: 0,
      isActive:true
      
    }
    
   }
   resetForm() {
    console.log(this.DistrictForm?.form.value )
    this.btnText='Submit';
    if (this.DistrictForm?.form != null) {
      console.log(this.DistrictForm?.form )
      this.DistrictForm.form.reset();
      this.DistrictForm.form.patchValue({
        districtId:0,
        districtName:"",
        menuPosition:0,
        isActive:true
       
      });
    }
    
  }
  getALlDistrict(){ 
   this.subscription=this.districtService.getAll().subscribe(item=>{
     this.dataSource=new MatTableDataSource(item);
     this.dataSource.paginator = this.paginator;
     this.dataSource.sort = this.matSort;
   
    });
   
  }
   onSubmit(form:NgForm){
    const id=this.DistrictForm.form.get('districtId')?.value;
    if(id){
      this.districtService.update(+id,this.DistrictForm.value).subscribe(response=>{
        this.getALlDistrict()
        this.resetForm();
      },err=>{
        console.log(err)
      })
    }
    else{
    this.subscription=this.districtService.submit(form?.value).subscribe(res=>{ 
      // this.snackBar.open('Information Inserted Successfully ', '', {
      //   duration: 2000,
      //   verticalPosition: 'top',
      //   horizontalPosition: 'right',
      //   panelClass: 'snackbar-success'
      // });  
      this.toggleToast();
    this.getALlDistrict()
    this.resetForm();
   
   },err=>{
     console.log(err);
   })
  }
  }
  delete(element:any){
    this.confirmService.confirm('Confirm delete message','Are You Sure Delete This Item').subscribe(result=>{
      if(result) 
      console.log(element)
    this.districtService.delete(element.districtId).subscribe(res=>{
      this.getALlDistrict()
    },(err) => { 

    }); 
    })
  }
    
  }
 

