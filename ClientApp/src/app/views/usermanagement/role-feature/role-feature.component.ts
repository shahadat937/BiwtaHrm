import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { Subscription } from 'rxjs';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RoleFeature } from '../../featureManagement/model/role-feature';
import { SelectedStringModel } from 'src/app/core/models/selectedStringModel';
import { ToastrService } from 'ngx-toastr';

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
  loading: boolean = false;

  constructor(
    public roleFeatureService: RoleFeatureService,
    private fb: FormBuilder,
    private toastr: ToastrService,
  ) {
    this.RoleFeaturesForm = this.fb.group({
      featuresList: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.roleFeatureService.roleFeature.roleId = '';
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
        roleName: [featureInfo.roleName],
        featureKey: [featureInfo.featureKey],
        featureName: [featureInfo.featureName],
        featurePath: [featureInfo.featurePath],
        selectAll: [featureInfo.selectAll === true],
        viewStatus: [featureInfo.viewStatus === true],
        add: [featureInfo.add === true],
        update: [featureInfo.update === true],
        delete: [featureInfo.delete === true],
      }));
    });

    this.RoleFeaturesForm.markAsDirty();
}

toggleAllSelection(event: any) {
  const isChecked = event.target.checked;
  this.FeaturesListArray.controls.forEach(control => {
    control.patchValue({
      viewStatus: isChecked,
      add: isChecked,
      update: isChecked,
      delete: isChecked,
      selectAll: isChecked
    });
  });
}

toggleRowSelection(index: number) {
  const control = this.FeaturesListArray.controls[index];
  const isChecked = control.get('selectAll')?.value;
  control.patchValue({
    viewStatus: isChecked,
    add: isChecked,
    update: isChecked,
    delete: isChecked
  });
}

  submitFeature() {
    this.loading = true;
    this.roleFeatureService.saveRoleFeatures(this.RoleFeaturesForm.get("featuresList")?.value).subscribe(((res: any) => {
      if (res.success) {
        this.toastr.success('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      } else {
        this.toastr.warning('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      }
    }));
    this.onRoleChange();
  }

}
