import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChildStatusComponent } from './child-status.component';

describe('ChildStatusComponent', () => {
  let component: ChildStatusComponent;
  let fixture: ComponentFixture<ChildStatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChildStatusComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ChildStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
