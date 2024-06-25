import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpazilaComponent } from './upazila.component';

describe('UpazilaComponent', () => {
  let component: UpazilaComponent;
  let fixture: ComponentFixture<UpazilaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpazilaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UpazilaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
