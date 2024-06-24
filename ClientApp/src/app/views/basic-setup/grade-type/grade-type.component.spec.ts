import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GradeTypeComponent } from './grade-type.component';

describe('GradeTypeComponent', () => {
  let component: GradeTypeComponent;
  let fixture: ComponentFixture<GradeTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GradeTypeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GradeTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
