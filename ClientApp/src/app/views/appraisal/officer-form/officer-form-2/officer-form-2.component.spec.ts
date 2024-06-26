import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficerForm2Component } from './officer-form-2.component';

describe('OfficerForm2Component', () => {
  let component: OfficerForm2Component;
  let fixture: ComponentFixture<OfficerForm2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OfficerForm2Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OfficerForm2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
