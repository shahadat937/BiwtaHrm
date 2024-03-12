import { Component, OnInit } from '@angular/core';
import { LayoutComponent } from "../../layout.component";
import { HeaderComponent } from "../../header/header.component";

@Component({
    selector: 'app-main-layout',
    standalone: true,
    templateUrl: './main-layout.component.html',
    styleUrls: [],
    imports: [LayoutComponent, HeaderComponent]
})
export class MainLayoutComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}
}
