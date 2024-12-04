import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { EmpFingerprint } from '../../../model/emp-fingerprint';
import { EmpFingerPrintService } from '../../../service/emp-finger-print.service';
import { Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-upload-finger-print',
  templateUrl: './upload-finger-print.component.html',
  styleUrls: ['./upload-finger-print.component.scss'],
})
export class UploadFingerPrintComponent implements OnInit {
  
  subscription: Subscription = new Subscription();
  imageUrl = environment.imageUrl + "EmpFingerprint/";
  
  empFingerprint: EmpFingerprint = new EmpFingerprint();

  leftFingers = [
    { id: 'LeftLittle', count: '5', label: 'Little', image: null as string | null, file: null },
    { id: 'LeftRing', count: '4', label: 'Ring', image: null as string | null, file: null },
    { id: 'LeftMiddle', count: '3', label: 'Middle', image: null as string | null, file: null },
    { id: 'LeftIndex', count: '2', label: 'Index', image: null as string | null, file: null },
    { id: 'LeftThumb', count: '1', label: 'Thumb', image: null as string | null, file: null },
  ];
  rightFingers = [
    { id: 'RightThumb', count: '1', label: 'Thumb', image: null as string | null, file: null },
    { id: 'RightIndex', count: '2', label: 'Index', image: null as string | null, file: null },
    { id: 'RightMiddle', count: '3', label: 'Middle', image: null as string | null, file: null },
    { id: 'RightRing', count: '4', label: 'Ring', image: null as string | null, file: null },
    { id: 'RightLittle', count: '5', label: 'Little', image: null as string | null, file: null },
  ];
  

  fingerprintForm: FormGroup;
  
  constructor(private fb: FormBuilder,
    private empFingerPrintService: EmpFingerPrintService
  ) {
    this.fingerprintForm = this.fb.group({});
  }

  ngOnInit(): void {
    this.getEmpFingerPrint();
  }

  getEmpFingerPrint(){
    this.subscription = this.empFingerPrintService.findByEmpId(79).subscribe((res)=> {
      if(res){
        this.empFingerprint = res;
        // Patch the response to leftFingers
        this.leftFingers[0].image = res.leftLittle ? `${this.imageUrl}${res.leftLittle}` : null;
        this.leftFingers[1].image = res.leftRing ? `${this.imageUrl}${res.leftRing}` : null;
        this.leftFingers[2].image = res.leftMiddle ? `${this.imageUrl}${res.leftMiddle}` : null;
        this.leftFingers[3].image = res.leftIndex ? `${this.imageUrl}${res.leftIndex}` : null;
        this.leftFingers[4].image = res.leftThumb ? `${this.imageUrl}${res.leftThumb}` : null;

        // Patch the response to rightFingers
        this.rightFingers[0].image = res.rightThumb ? `${this.imageUrl}${res.rightThumb}` : null;
        this.rightFingers[1].image = res.rightIndex ? `${this.imageUrl}${res.rightIndex}` : null;
        this.rightFingers[2].image = res.rightMiddle ? `${this.imageUrl}${res.rightMiddle}` : null;
        this.rightFingers[3].image = res.rightRing ? `${this.imageUrl}${res.rightRing}` : null;
        this.rightFingers[4].image = res.rightLittle ? `${this.imageUrl}${res.rightLittle}` : null;
      }
    })
  }

  onPhotoSelected(event: any, finger: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        finger.image = e.target.result; // Update the preview image
        finger.file = file; // Store the selected file
      };
      reader.readAsDataURL(file);
    }
  }

  onSubmit() {
    // Map left side files to the model
    this.empFingerprint.leftLittleFile = this.leftFingers[0].file;
    this.empFingerprint.leftRingFile = this.leftFingers[1].file;
    this.empFingerprint.leftMiddleFile = this.leftFingers[2].file;
    this.empFingerprint.leftIndexFile = this.leftFingers[3].file;
    this.empFingerprint.leftThumbFile = this.leftFingers[4].file;

    // Map right side files to the model
    this.empFingerprint.rightThumbFile = this.rightFingers[0].file;
    this.empFingerprint.rightIndexFile = this.rightFingers[1].file;
    this.empFingerprint.rightMiddleFile = this.rightFingers[2].file;
    this.empFingerprint.rightRingFile = this.rightFingers[3].file;
    this.empFingerprint.rightLittleFile = this.rightFingers[4].file;
    this.empFingerprint.empId = 79;
    this.empFingerprint.pNo = '001077';

    this.empFingerPrintService.saveEmpFingerprintInfo(this.empFingerprint).subscribe((res) => {
      console.log(res);
    });

  }
}
