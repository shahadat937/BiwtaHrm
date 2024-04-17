import { GroupService } from './../service/group.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import {
  AfterViewInit,
  Component,
  OnInit,
  ViewChild,
  OnDestroy,
} from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { Subscription } from 'rxjs';
 
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrl: './group.component.scss'
})
export class GroupComponent   implements OnInit, OnDestroy, AfterViewInit {
    position = 'top-end';
    visible = false;
    percentage = 0;
    btnText: string | undefined;
    @ViewChild('GroupForm', { static: true }) GroupForm!: NgForm;
    subscription: Subscription = new Subscription();
    displayedColumns: string[] = ['slNo', 'groupName', 'isActive', 'Action'];
    dataSource = new MatTableDataSource<any>();
    @ViewChild(MatPaginator)
    paginator!: MatPaginator;
    @ViewChild(MatSort)
    matSort!: MatSort;
    subjects: SelectedModel[] = [];
    constructor(
      public groupService: GroupService,
      private snackBar: MatSnackBar,
      private route: ActivatedRoute,
      private router: Router,
      private confirmService: ConfirmService,
      private toastr: ToastrService
    ) 
    { 
  
      this.route.paramMap.subscribe((params) => {
        const id = params.get('groupId');
        if (id) {
          this.btnText = 'Update';
          this.groupService.find(+id).subscribe((res) => {
            console.log(res);
            this.GroupForm?.form.patchValue(res);
          });
        } else {
          this.btnText = 'Submit';
        }
      });
    }
    ngOnInit(): void {
      this.getALlGroup();
      this.loadsubjects();
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
    initaialGroup(form?: NgForm) {
      if (form != null) form.resetForm();
      this.groupService.groups = {
        groupId: 0,
        groupName: '',
        subjectId :0,
        //subjectName:"",
        menuPosition: 0,
        isActive: true,
      };
    }
    resetForm() {
      console.log(this.GroupForm?.form.value);
      this.btnText = 'Submit';
      if (this.GroupForm?.form != null) {
        console.log(this.GroupForm?.form);
        this.GroupForm.form.reset();
        this.GroupForm.form.patchValue({
          groupId: 0,
          groupName: '',
          subjectId :0,
        //  subjectName:"",
          menuPosition: 0,
          isActive: true,
        });
      }
    }
  
    loadsubjects() {
      console.log('subject')
      this.groupService.getSubject().subscribe(data => {
        console.log('subject'+ data)
        this.subjects = data;
      });
    }
  
    getALlGroup() {
      this.subscription = this.groupService.getAll().subscribe((item) => {
        this.dataSource = new MatTableDataSource(item);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.matSort;
      });
    }
    onSubmit(form: NgForm) {
      const id = this.GroupForm.form.get('groupId')?.value;
      if (id) {
        this.groupService.update(+id, this.GroupForm.value).subscribe(
          (response: any) => {
            console.log(response);
            if (response.success) {
              this.toastr.success('Successfully', 'Update', {
                positionClass: 'toast-top-right',
              });
              this.getALlGroup();
              this.resetForm();
              this.router.navigate(['/bascisetup/group']);
            } else {
              this.toastr.warning('', `${response.message}`, {
                positionClass: 'toast-top-right',
              });
            }
          },
          (err) => {
            console.log(err);
          }
        );
      } else {
        this.subscription = this.groupService.submit(form?.value).subscribe(
          (response: any) => {
            if (response.success) {
              this.toastr.success('Successfully', `${response.message}`, {
                positionClass: 'toast-top-right',
              });
              this.getALlGroup();
              this.resetForm();
            } else {
              this.toastr.warning('', `${response.message}`, {
                positionClass: 'toast-top-right',
              });
            }
          },
          (err) => {
            console.log(err);
          }
        );
      }
    }
    delete(element: any) {
      this.confirmService
        .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
        .subscribe((result) => {
          if (result) {
            this.groupService.delete(element.groupId).subscribe(
              (res) => {
                this.getALlGroup();
              },
              (err) => {
                this.toastr.error('Somethig Wrong ! ', ` `, {
                  positionClass: 'toast-top-right',})
                console.log(err);
              }
            );
          }
        });
    }
  }
  
