import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RewardPunishmentPriorityComponent } from './reward-punishment-priority.component';

describe('RewardPunishmentPriorityComponent', () => {
  let component: RewardPunishmentPriorityComponent;
  let fixture: ComponentFixture<RewardPunishmentPriorityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RewardPunishmentPriorityComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RewardPunishmentPriorityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
