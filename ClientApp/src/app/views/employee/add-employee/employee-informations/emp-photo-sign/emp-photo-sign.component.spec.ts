import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpPhotoSignComponent } from './emp-photo-sign.component';

describe('EmpPhotoSignComponent', () => {
  let component: EmpPhotoSignComponent;
  let fixture: ComponentFixture<EmpPhotoSignComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpPhotoSignComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpPhotoSignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
