import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-emp-job-details',
  templateUrl: './emp-job-details.component.html',
  styleUrl: './emp-job-details.component.scss'
})
export class EmpJobDetailsComponent implements OnInit, OnDestroy  {

  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();
  visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  countris: SelectedModel[] = [];
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  @ViewChild('EmpPresentAddressForm', { static: true }) EmpPresentAddressForm!: NgForm;

  constructor(
    private toastr: ToastrService,) {

  }

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }
  
  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }

  resetForm() {
  }

  cancel() {
    this.close.emit();
  }

  onSubmit(form: NgForm): void {
  }

}
