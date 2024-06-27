import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficerFormPart5Component } from './officer-form-part-5.component';

describe('OfficerFormPart5Component', () => {
  let component: OfficerFormPart5Component;
  let fixture: ComponentFixture<OfficerFormPart5Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OfficerFormPart5Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OfficerFormPart5Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
