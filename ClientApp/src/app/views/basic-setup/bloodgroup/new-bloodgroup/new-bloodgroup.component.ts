import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { BloodGroupService } from '../../service/BloodGroup.service';

@Component({
  selector: 'app-new-bloodgroup',
  templateUrl: './new-bloodgroup.component.html',
  styleUrls: ['./new-bloodgroup.component.sass']
})
export class NewBloodGroupComponent implements OnInit {
  pageTitle: string;
  loading = false;
  destination:string;
  btnText:string;
  BloodGroupForm: FormGroup;
  validationErrors: string[] = [];

  constructor(
    private snackBar: MatSnackBar,
    private BloodGroupService: BloodGroupService,
    private fb: FormBuilder, 
    private router: Router,  
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('bloodGroupId'); 
    if (id) {
      this.pageTitle = 'Edit Blood Group';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.BloodGroupService.find(+id).subscribe(
        res => {
          this.BloodGroupForm.patchValue({          

            bloodGroupId: res.bloodGroupId,
            bloodGroupName: res.bloodGroupName,
            //menuPosition: res.menuPosition,
          
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Blood Group';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.BloodGroupForm = this.fb.group({
      bloodGroupId: [0],
      bloodGroupName: ['', Validators.required],
      //menuPosition: ['', Validators.required],
      isActive: [true],
    
    })
  }
  
 

}
