import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YearSetupComponent } from './year-setup.component';

describe('YearSetupComponent', () => {
  let component: YearSetupComponent;
  let fixture: ComponentFixture<YearSetupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [YearSetupComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(YearSetupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
