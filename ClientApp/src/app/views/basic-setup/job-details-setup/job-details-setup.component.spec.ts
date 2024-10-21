import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobDetailsSetupComponent } from './job-details-setup.component';

describe('JobDetailsSetupComponent', () => {
  let component: JobDetailsSetupComponent;
  let fixture: ComponentFixture<JobDetailsSetupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [JobDetailsSetupComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobDetailsSetupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
