import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReusableToastComponent } from './reusable-toast.component';

describe('ReusableToastComponent', () => {
  let component: ReusableToastComponent;
  let fixture: ComponentFixture<ReusableToastComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReusableToastComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReusableToastComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
