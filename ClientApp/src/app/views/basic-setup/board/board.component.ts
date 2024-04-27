import { BoardService } from './../service/board.service';
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
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrl: './board.component.scss'
})
export class BoardComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  @ViewChild('BoardForm', { static: true }) BoardForm!: NgForm;
  loading = false;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'boardName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public boardService: BoardService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    //  const id = this.route.snapshot.paramMap.get('bloodGroupId');
  }
  ngOnInit(): void {
    this.getAllBoards();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('boardId');
      if (id) {
        this.btnText = 'Update';
        this.boardService.find(+id).subscribe((res) => {
          this.BoardForm?.form.patchValue(res);
        });
      } else {
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
      this.subscription.unsubscribe();
    }
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  initaialBoard(form?: NgForm) {
    if (form != null) form.resetForm();
    this.boardService.boards = {
      boardId: 0,
      boardName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.BoardForm?.form != null) {
      this.BoardForm.form.reset();
      this.BoardForm.form.patchValue({
        boardId: 0,
        boardName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bascisetup/board']);
  }

  getAllBoards() {
    this.subscription = this.boardService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.boardService.cachedData = [];
    const id = form.value.boardId;
    const action$ = id
      ? this.boardService.update(id, form.value)
      : this.boardService.submit(form.value);
    
    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllBoards();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/board']);
        }
    this.loading = false;
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
      
    this.loading = false;
    });
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.boardService.delete(element.boardId).subscribe(
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
          );
        }
      });
  }
}