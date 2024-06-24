import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EyesColorComponent } from './eyes-color.component';

describe('EyesColorComponent', () => {
  let component: EyesColorComponent;
  let fixture: ComponentFixture<EyesColorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EyesColorComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EyesColorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
