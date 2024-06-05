import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployePostingJoinListComponent } from './employe-posting-join-list.component';

describe('EmployePostingJoinListComponent', () => {
  let component: EmployePostingJoinListComponent;
  let fixture: ComponentFixture<EmployePostingJoinListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmployePostingJoinListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmployePostingJoinListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
