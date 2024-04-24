import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficerFormComponent } from './officer-form.component';

describe('OfficerFormComponent', () => {
  let component: OfficerFormComponent;
  let fixture: ComponentFixture<OfficerFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OfficerFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OfficerFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
