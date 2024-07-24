import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReleaseTypeComponent } from './release-type.component';

describe('ReleaseTypeComponent', () => {
  let component: ReleaseTypeComponent;
  let fixture: ComponentFixture<ReleaseTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ReleaseTypeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReleaseTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
