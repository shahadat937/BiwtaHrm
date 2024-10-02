import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResponsibilityTypeComponent } from './responsibility-type.component';

describe('ResponsibilityTypeComponent', () => {
  let component: ResponsibilityTypeComponent;
  let fixture: ComponentFixture<ResponsibilityTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ResponsibilityTypeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ResponsibilityTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
