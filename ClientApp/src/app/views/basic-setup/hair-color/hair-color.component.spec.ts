import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HairColorComponent } from './hair-color.component';

describe('HairColorComponent', () => {
  let component: HairColorComponent;
  let fixture: ComponentFixture<HairColorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HairColorComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HairColorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
