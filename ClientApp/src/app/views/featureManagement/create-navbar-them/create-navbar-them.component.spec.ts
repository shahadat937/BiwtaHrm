import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateNavbarThemComponent } from './create-navbar-them.component';

describe('CreateNavbarThemComponent', () => {
  let component: CreateNavbarThemComponent;
  let fixture: ComponentFixture<CreateNavbarThemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateNavbarThemComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateNavbarThemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
