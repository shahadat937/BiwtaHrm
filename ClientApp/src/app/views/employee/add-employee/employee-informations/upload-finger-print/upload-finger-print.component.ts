import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { EmpFingerprint } from '../../../model/emp-fingerprint';

@Component({
  selector: 'app-upload-finger-print',
  templateUrl: './upload-finger-print.component.html',
  styleUrls: ['./upload-finger-print.component.scss'],
})
export class UploadFingerPrintComponent {
  leftFingers = [
    { id: 'LeftLittle', label: 'Little', image: null, file: null },
    { id: 'LeftRing', label: 'Ring', image: null, file: null },
    { id: 'LeftMiddle', label: 'Middle', image: null, file: null },
    { id: 'LeftIndex', label: 'Index', image: null, file: null },
    { id: 'LeftThumb', label: 'Thumb', image: null, file: null },
  ];
  rightFingers = [
    { id: 'RightThumb', label: 'Thumb', image: null, file: null },
    { id: 'RightIndex', label: 'Index', image: null, file: null },
    { id: 'RightMiddle', label: 'Middle', image: null, file: null },
    { id: 'RightRing', label: 'Ring', image: null, file: null },
    { id: 'RightLittle', label: 'Little', image: null, file: null },
  ];

  fingerprintForm: FormGroup;
  
  constructor(private fb: FormBuilder) {
    this.fingerprintForm = this.fb.group({});
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
    const empFingerprint = new EmpFingerprint();

    // Map left side files to the model
    empFingerprint.leftLittleFile = this.leftFingers[0].file;
    empFingerprint.leftRingFile = this.leftFingers[1].file;
    empFingerprint.leftMiddleFile = this.leftFingers[2].file;
    empFingerprint.leftIndexFile = this.leftFingers[3].file;
    empFingerprint.leftThumbFile = this.leftFingers[4].file;

    // Map right side files to the model
    empFingerprint.rightThumbFile = this.rightFingers[0].file;
    empFingerprint.rightIndexFile = this.rightFingers[1].file;
    empFingerprint.rightMiddleFile = this.rightFingers[2].file;
    empFingerprint.rightRingFile = this.rightFingers[3].file;
    empFingerprint.rightLittleFile = this.rightFingers[4].file;

    console.log('EmpFingerprint Model:', empFingerprint);

  }
}
