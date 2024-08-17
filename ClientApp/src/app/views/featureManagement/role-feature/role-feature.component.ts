import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { RoleFeatureService } from '../service/role-feature.service';
import { Subscription } from 'rxjs';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RoleFeature } from '../model/role-feature';
import { SelectedStringModel } from 'src/app/core/models/selectedStringModel';

@Component({
  selector: 'app-role-feature',
  templateUrl: './role-feature.component.html',
  styleUrls: ['./role-feature.component.scss']
})
export class RoleFeatureComponent implements OnInit, OnDestroy {

  subscription: Subscription = new Subscription();
  roles: SelectedStringModel[] = [];
  features: RoleFeature[] = [];
  RoleFeaturesForm: FormGroup;

  constructor(
    public roleFeatureService: RoleFeatureService,
    private fb: FormBuilder
  ) {
    this.RoleFeaturesForm = this.fb.group({
      featuresList: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.getSelectedRoles();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getSelectedRoles() {
    this.subscription = this.roleFeatureService.getSelectedModule().subscribe((item) => {
      this.roles = item;
    });
  }

  onRoleChange() {
    if(this.roleFeatureService.roleFeature.roleId != ''){
      this.roleFeatureService.getFeaturesByRoleId(this.roleFeatureService.roleFeature.roleId)
      .subscribe((res) => {
        if (res.length > 0) {
          this.features = res;
          this.pathFeaturesList(this.features);
        }
        else{
          this.features = [];
        }
      });
    }
  }

  get FeaturesListArray() {
    return this.RoleFeaturesForm.get('featuresList') as FormArray;
  }

  pathFeaturesList(featuresInfoList: RoleFeature[]) {
    const control = this.FeaturesListArray;
    control.clear();

    featuresInfoList.forEach(featureInfo => {
      control.push(this.fb.group({
        roleFeatureId: [featureInfo.roleFeatureId],
        roleId: [featureInfo.roleId],
        featureKey: [featureInfo.featureKey],
        featureName: [featureInfo.featureName],
        viewStatus: [featureInfo.viewStatus === true],
        add: [featureInfo.add === true],
        update: [featureInfo.update === true],
        delete: [featureInfo.delete === true],
      }));
    });

    // Ensure Angular change detection picks up the update
    this.RoleFeaturesForm.markAsDirty();
}


  submitFeature() {
    console.log("Form Value: ", this.RoleFeaturesForm.get("featuresList")?.value);
  }

}
