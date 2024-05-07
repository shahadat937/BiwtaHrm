import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpDemoComponent } from './emp-demo.component';

describe('EmpDemoComponent', () => {
  let component: EmpDemoComponent;
  let fixture: ComponentFixture<EmpDemoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpDemoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpDemoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
