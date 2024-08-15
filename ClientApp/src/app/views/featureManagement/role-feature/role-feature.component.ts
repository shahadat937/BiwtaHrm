import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { RoleFeatureService } from '../service/role-feature.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { Subscription } from 'rxjs';
import { NgForm } from '@angular/forms';
import { SelectedStringModel } from 'src/app/core/models/selectedStringModel';
import { RoleFeature } from '../model/role-feature';

@Component({
  selector: 'app-role-feature',
  templateUrl: './role-feature.component.html',
  styleUrl: './role-feature.component.scss'
})
export class RoleFeatureComponent  implements OnInit, OnDestroy {

  subscription: Subscription = new Subscription();
  roles: SelectedStringModel[] = [];
  features: RoleFeature[] = [];
  @ViewChild('RoleFeatureForm', { static: true }) RoleFeatureForm!: NgForm;

  constructor(
    public roleFeatureService: RoleFeatureService,
  ) {

  }

  ngOnInit(): void {
    this.getSelectedRoles();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getSelectedRoles(){
    this.subscription = this.roleFeatureService.getSelectedModule().subscribe((item) => {
      this.roles = item;
    });
  }

  onRoleChange() {
    this.roleFeatureService.getFeaturesByRoleId(this.roleFeatureService.roleFeature.roleId || '')
      .subscribe((res) => {
        this.features = res;
      });
  }


}
