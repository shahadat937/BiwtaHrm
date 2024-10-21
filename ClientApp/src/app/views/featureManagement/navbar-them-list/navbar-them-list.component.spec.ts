import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavbarThemListComponent } from './navbar-them-list.component';

describe('NavbarThemListComponent', () => {
  let component: NavbarThemListComponent;
  let fixture: ComponentFixture<NavbarThemListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NavbarThemListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NavbarThemListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
