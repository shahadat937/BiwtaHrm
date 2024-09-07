import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonalLeaveComponent } from './personal-leave.component';

describe('PersonalLeaveComponent', () => {
  let component: PersonalLeaveComponent;
  let fixture: ComponentFixture<PersonalLeaveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PersonalLeaveComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PersonalLeaveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
