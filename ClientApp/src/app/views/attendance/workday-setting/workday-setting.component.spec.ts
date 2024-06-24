import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkdaySettingComponent } from './workday-setting.component';

describe('WorkdaySettingComponent', () => {
  let component: WorkdaySettingComponent;
  let fixture: ComponentFixture<WorkdaySettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WorkdaySettingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WorkdaySettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
