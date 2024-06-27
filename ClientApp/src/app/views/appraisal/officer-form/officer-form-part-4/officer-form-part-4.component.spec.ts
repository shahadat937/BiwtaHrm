import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficerFormPart4Component } from './officer-form-part-4.component';

describe('OfficerFormPart4Component', () => {
  let component: OfficerFormPart4Component;
  let fixture: ComponentFixture<OfficerFormPart4Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OfficerFormPart4Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OfficerFormPart4Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
