import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { AfterViewInit, Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { Subscription } from 'rxjs';
import { brandSet, cil3d, cil4k, cilAccountLogout, cilActionRedo, cilAirplaneMode, cilList, cilPaperPlane, cilPencil, cilShieldAlt, cilTrash } from '@coreui/icons';
import { FormGroup, NgForm, FormBuilder, FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UapzilaService } from './../service/uapzila.service'
import { ActivatedRoute } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-upazila',
  templateUrl: './upazila.component.html',
  styleUrl: './upazila.component.scss'
})
export class UpazilaComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild("UpazilaForm", { static: true }) UpazilaForm!: NgForm;
  subscription: Subscription = new Subscription;
  selectedDistricts: SelectedModel[] = [];
  isShowDistrictName: boolean = false;

  displayedColumns: string[] = ['slNo', 'districtName', 'upazilaName', 'isActive', 'Action'];


  dataSource = new MatTableDataSource < any > ();
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
    public upazilaService: UapzilaService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private confirmService: ConfirmService,
  ) {
    this.route.paramMap.subscribe(params => {
      const id = params.get('upazilaId');
      if (id) {
        this.btnText = 'Update';
        this.upazilaService.getById(+id).subscribe(
          res => {
            console.log(res);
            this.UpazilaForm?.form.patchValue(res);
          }
        );
      }
      else {
        this.btnText = 'Submit'
      }
    });

  }
  ngOnInit(): void {
    this.getALlUpazila();
    this.getselecteddistricts();



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
  initaialUpazila(form?: NgForm) {
    if (form != null)
      form.resetForm();
    this.upazilaService.upazilas = {
      upazilaId: 0,
      upazilaName: "",
      districtId: 0,
      menuPosition: 0,
      isActive: true

    }

  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.UpazilaForm?.form != null) {
      console.log(this.UpazilaForm?.form)
      this.UpazilaForm.form.reset();
      this.UpazilaForm.form.patchValue({
        upazilaId: 0,
        upazilaName: "",
        districtId: 0,
        menuPosition: 0,
        isActive: true

      });
    }

  }
  getALlUpazila() {
    this.subscription = this.upazilaService.getAll().subscribe(item => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;

    });

  }



  getselecteddistricts() {
    this.upazilaService.getdistrict().subscribe(res => {
      this.selectedDistricts = res
    });
  }

  onSubmit(form: NgForm) {
    this.upazilaService.cachedData = [];
    const id = this.UpazilaForm.form.get('upazilaId')?.value;
    if (id) {
      this.upazilaService.update(+id, this.UpazilaForm.value).subscribe(response => {
        this.getALlUpazila()
        this.resetForm();
      }, err => {
        console.log(err)
      })
    }
    else {
      this.subscription = this.upazilaService.submit(form?.value).subscribe(res => {
        // this.snackBar.open('Information Inserted Successfully ', '', {
        //   duration: 2000,
        //   verticalPosition: 'top',
        //   horizontalPosition: 'right',
        //   panelClass: 'snackbar-success'
        // });  
        this.toggleToast();
        this.getALlUpazila()
        this.resetForm();

      }, err => {
        console.log(err);
      })
    }
  }
  delete(element: any) {
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item').subscribe(result => {
      if (result)
        console.log(element)
      this.upazilaService.delete(element.upazilaId).subscribe(res => {
        this.getALlUpazila()
      }, (err) => {

      });
    })
  }

}


