import { Component } from '@angular/core';

@Component({
  selector: 'app-officer-form-application',
  templateUrl: './officer-form-application.component.html',
  styleUrl: './officer-form-application.component.scss'
})
export class OfficerFormApplicationComponent {
  ActiveSection: boolean[] = [true,false,false,false,false,false,false];
}
