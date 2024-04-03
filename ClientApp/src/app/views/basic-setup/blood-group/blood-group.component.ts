import { BloodGroupService } from './../service/BloodGroup.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { AfterViewInit, Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { Subscription } from 'rxjs';
import { brandSet, cil3d, cil4k, cilAccountLogout, cilActionRedo, cilAirplaneMode, cilList, cilPaperPlane, cilPencil, cilShieldAlt, cilTrash } from '@coreui/icons';
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-blood-group',
  templateUrl: './blood-group.component.html',
  styleUrl: './blood-group.component.scss'
})
export class BloodGroupComponent implements OnInit,OnDestroy,AfterViewInit{
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText:string | undefined;
  @ViewChild("BloodGroupForm", { static: true }) BloodGroupForm!: NgForm;
  subscription: Subscription = new Subscription;
  displayedColumns: string[] = ['slNo','bloodGroupName', 'isActive','Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
constructor(
  public bloodGroupService:BloodGroupService,
  private snackBar: MatSnackBar,
  private route: ActivatedRoute,
  private router: Router,
  private confirmService: ConfirmService,
  private toastr: ToastrService
  )
  {
  //  const id = this.route.snapshot.paramMap.get('bloodGroupId'); 

    this.route.paramMap.subscribe(params => {
      const id = params.get('bloodGroupId');
      if (id) {
        this.btnText = 'Update';
        this.bloodGroupService.find(+id).subscribe(
          res => {
            console.log(res);
            this.BloodGroupForm?.form.patchValue(res);
          }
        );
      }
      else {

        this.btnText = 'Submit';
      }
    });

  
 }
  ngOnInit(): void {
   
    this.getALlBloodGroup();

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
  initaialBloodGroup(form?:NgForm){
    if(form!=null)
    form.resetForm();
    this.bloodGroupService.bloodGroups = {
      bloodGroupId:0,
      bloodGroupName:"",
      menuPosition: 0,
      isActive:true

    }

   }
   resetForm() {
    console.log(this.BloodGroupForm?.form.value )
    this.btnText = 'Submit';
    if (this.BloodGroupForm?.form != null) {
      console.log(this.BloodGroupForm?.form )
      this.BloodGroupForm.form.reset();
      this.BloodGroupForm.form.patchValue({
        bloodGroupId:0,
        bloodGroupName:"",
        menuPosition:0,
        isActive:true

      });
    }

  }

  getALlBloodGroup(){
   this.subscription=this.bloodGroupService.getAll().subscribe(item=>{
     this.dataSource=new MatTableDataSource(item);
     this.dataSource.paginator = this.paginator;
     this.dataSource.sort = this.matSort;

    });

  }
   onSubmit(form:NgForm){
    const id = this.BloodGroupForm.form.get('bloodGroupId')?.value;
    if (id) {
      this.bloodGroupService.update(+id,this.BloodGroupForm.value).subscribe((response:any) => {
        console.log(response)
        if(response.success){
          this.toastr.success('Successfully', 'Update',{ positionClass: 'toast-top-right' });
          this.getALlBloodGroup()
          this.resetForm();
          this.router.navigate(["/bascisetup/blood-group"]);  
        }else{
          this.toastr.warning('', `${response.message}`,{ positionClass: 'toast-top-right' });
        }
        
      }, err => {
        console.log(err)
      })
    }else{
   this.subscription=this.bloodGroupService.submit(form?.value).subscribe((response:any)=>{
    if(response.success){
      this.toastr.success('Successfully', `${response.message}`,{ positionClass: 'toast-top-right' });
      this.getALlBloodGroup()
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
        this.bloodGroupService.delete(element.bloodGroupId).subscribe(res=>{
          this.getALlBloodGroup()
        },(err) => { 
       console.log(err)
        });
      }
    })
   
    
  }
}





