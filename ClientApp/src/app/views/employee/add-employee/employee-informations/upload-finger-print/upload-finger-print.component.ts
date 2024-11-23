import { Component } from '@angular/core';

@Component({
  selector: 'app-upload-finger-print',
  templateUrl: './upload-finger-print.component.html',
  styleUrls: ['./upload-finger-print.component.scss'],
})
export class UploadFingerPrintComponent {
  leftFingers = [
    { id: 'LeftThumb', label: '5', image: null },
    { id: 'LeftIndex', label: '4', image: null },
    { id: 'LeftMiddle', label: '3', image: null },
    { id: 'LeftRing', label: '2', image: null },
    { id: 'LeftLittle', label: '1', image: null },
  ];

  rightFingers = [
    { id: 'RightThumb', label: '1', image: null },
    { id: 'RightIndex', label: '2', image: null },
    { id: 'RightMiddle', label: '3', image: null },
    { id: 'RightRing', label: '4', image: null },
    { id: 'RightLittle', label: '5', image: null },
  ];

  onPhotoSelected(event: any, finger: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        finger.image = e.target.result;
      };
      reader.readAsDataURL(file);
    }
  }

  
}
