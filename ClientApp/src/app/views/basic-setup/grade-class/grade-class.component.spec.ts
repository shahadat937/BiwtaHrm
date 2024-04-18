import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GradeClassComponent } from './grade-class.component';

describe('GradeClassComponent', () => {
  let component: GradeClassComponent;
  let fixture: ComponentFixture<GradeClassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GradeClassComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GradeClassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
