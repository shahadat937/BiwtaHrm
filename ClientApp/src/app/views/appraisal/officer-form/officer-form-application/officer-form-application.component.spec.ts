import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficerFormApplicationComponent } from './officer-form-application.component';

describe('OfficerFormApplicationComponent', () => {
  let component: OfficerFormApplicationComponent;
  let fixture: ComponentFixture<OfficerFormApplicationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OfficerFormApplicationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfficerFormApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
