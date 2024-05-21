import { AfterViewInit, Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { OrganogramOfficeNameDto, OrganogramService } from '../service/organogram.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-organogram',
  templateUrl: './organogram.component.html',
  styleUrl: './organogram.component.scss'
})
export class OrganogramComponent implements OnInit, OnDestroy  {

  subscription: Subscription = new Subscription();
  organograms:any[] = [];
  offices: OrganogramOfficeNameDto[] = [];

  constructor(
    public organogramService: OrganogramService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
  ) {
  }
  ngOnInit(): void {
    this.getOrganogram();
  }
  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getOrganogram(){
    this.subscription=this.organogramService.getOrganogramNamesOnly().subscribe((data: OrganogramOfficeNameDto[]) => { 
      this.organograms = data;
      this.offices = data;
      console.log(this.offices)
    });
  }

  
}

