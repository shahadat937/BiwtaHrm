import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JoiningReportingListComponent } from './joining-reporting-list.component';

describe('JoiningReportingListComponent', () => {
  let component: JoiningReportingListComponent;
  let fixture: ComponentFixture<JoiningReportingListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [JoiningReportingListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(JoiningReportingListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
