import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BloodGroupComponent } from './blood-group.component';

describe('BloodGroupComponent', () => {
  let component: BloodGroupComponent;
  let fixture: ComponentFixture<BloodGroupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BloodGroupComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BloodGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
