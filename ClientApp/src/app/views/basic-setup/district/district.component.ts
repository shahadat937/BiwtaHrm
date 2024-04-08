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
  btnText:string|undefined;
  @ViewChild("DistrictForm", { static: true }) DistrictForm!: NgForm;
  subscription: Subscription = new Subscription;
  displayedColumns: string[] = ['slNo','districtName', 'isActive','Action'];
  dataSource = new MatTableDataSource<any>();
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
   
 }
  ngOnInit(): void {
    this.getALlDistrict();
    this.handleRouteParams();
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
  handleRouteParams(){
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
    this.districtService.cachedData = [];
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
   
    this.districtService.delete(element.districtId).subscribe(res=>{
      const index = this.dataSource.data.indexOf(element);
      if (index !== -1) {
        this.dataSource.data.splice(index, 1);
        this.dataSource = new MatTableDataSource(
          this.dataSource.data
        );
      }
    },(err) => { 

    }); 
    })
  }
    
  }
 

