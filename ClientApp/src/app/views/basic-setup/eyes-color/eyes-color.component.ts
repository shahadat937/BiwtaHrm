import { EyesColorService } from './../service/eyes-color.service';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-eyes-color',
  templateUrl: './eyes-color.component.html',
  styleUrl: './eyes-color.component.scss'
})
export class EyesColorComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  headerText: string | undefined;
  loading = false;
  @ViewChild('EyesColorForm', { static: true }) EyesColorForm!: NgForm;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = ['slNo', 'eyesColorName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public eyesColorService: EyesColorService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    //  const id = this.route.snapshot.paramMap.get('bloodGroupId');
  }
  ngOnInit(): void {
    this.getAllEyesColors();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('eyesColorId');
      if (id) {
        this.btnText = 'Update';
        this.headerText = 'Update Eye Color';
        this.subscription.push(
        this.eyesColorService.find(+id).subscribe((res) => {
          this.EyesColorForm?.form.patchValue(res);
        })
        )
       
      } else {
        this.resetForm();
        this.headerText = 'Add Eye Color';
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
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  initaialEyesColor(form?: NgForm) {
    if (form != null) form.resetForm();
    this.eyesColorService.eyesColors = {
      eyesColorId: 0,
      eyesColorName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.EyesColorForm?.form != null) {
      this.EyesColorForm.form.reset();
      this.EyesColorForm.form.patchValue({
        eyesColorId: 0,
        eyesColorName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/personalInfoSetup/eyesColor']);
  }

  getAllEyesColors() {
    this.subscription.push(
    this.eyesColorService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }
  
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.eyesColorService.cachedData = [];
    const id = form.value.eyesColorId;
    const action$ = id
      ? this.eyesColorService.update(id, form.value)
      : this.eyesColorService.submit(form.value);

      this.subscription.push(
      action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllEyesColors();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/personalInfoSetup/eyesColor']);
        }
        this.loading = false;
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
      this.loading = false;
    })
      )
  
  }
  delete(element: any) {
    this.subscription.push(
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.subscription.push(
          this.eyesColorService.delete(element.eyesColorId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
              this.toastr.success('Delete sucessfully ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            },
            (err) => {
              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
              console.log(err);
            }
          )
          )
          
        }
      })
    )
    
  }
}