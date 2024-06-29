import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficerFormPart7Component } from './officer-form-part-7.component';

describe('OfficerFormPart7Component', () => {
  let component: OfficerFormPart7Component;
  let fixture: ComponentFixture<OfficerFormPart7Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OfficerFormPart7Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OfficerFormPart7Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
