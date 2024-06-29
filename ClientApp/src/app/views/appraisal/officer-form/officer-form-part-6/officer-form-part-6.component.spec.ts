import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficerFormPart6Component } from './officer-form-part-6.component';

describe('OfficerFormPart6Component', () => {
  let component: OfficerFormPart6Component;
  let fixture: ComponentFixture<OfficerFormPart6Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OfficerFormPart6Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OfficerFormPart6Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
