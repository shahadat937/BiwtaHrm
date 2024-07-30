import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JoiningReportingComponent } from './joining-reporting.component';

describe('JoiningReportingComponent', () => {
  let component: JoiningReportingComponent;
  let fixture: ComponentFixture<JoiningReportingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [JoiningReportingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(JoiningReportingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
