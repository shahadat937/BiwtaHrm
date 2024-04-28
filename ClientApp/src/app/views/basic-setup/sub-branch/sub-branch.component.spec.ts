import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubBranchComponent } from './sub-branch.component';

describe('SubBranchComponent', () => {
  let component: SubBranchComponent;
  let fixture: ComponentFixture<SubBranchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SubBranchComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SubBranchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
