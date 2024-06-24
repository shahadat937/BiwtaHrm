import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpForeignTourInfoComponent } from './emp-foreign-tour-info.component';

describe('EmpForeignTourInfoComponent', () => {
  let component: EmpForeignTourInfoComponent;
  let fixture: ComponentFixture<EmpForeignTourInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpForeignTourInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpForeignTourInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
