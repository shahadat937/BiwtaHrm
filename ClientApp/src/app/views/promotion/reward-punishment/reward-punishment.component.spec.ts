import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RewardPunishmentComponent } from './reward-punishment.component';

describe('RewardPunishmentComponent', () => {
  let component: RewardPunishmentComponent;
  let fixture: ComponentFixture<RewardPunishmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RewardPunishmentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RewardPunishmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
