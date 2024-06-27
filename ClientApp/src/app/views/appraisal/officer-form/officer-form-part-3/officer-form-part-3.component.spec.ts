import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficerFormPart3Component } from './officer-form-part-3.component';

describe('OfficerFormPart3Component', () => {
  let component: OfficerFormPart3Component;
  let fixture: ComponentFixture<OfficerFormPart3Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OfficerFormPart3Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OfficerFormPart3Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
