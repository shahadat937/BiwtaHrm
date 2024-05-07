import { Component } from '@angular/core';
import { SharedModule } from '@coreui/angular';
@Component({
  selector: 'app-emp-demo',
  templateUrl: './emp-demo.component.html',
  styleUrl: './emp-demo.component.scss'
  
})
export class EmpDemoComponent {
  public visible = false;

  toggleLiveDemo() {
    this.visible = !this.visible;
  }

  handleLiveDemoChange(event: any) {
    this.visible = event;
  }
}