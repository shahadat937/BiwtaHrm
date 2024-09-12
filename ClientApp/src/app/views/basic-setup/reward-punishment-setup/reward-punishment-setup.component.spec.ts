import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RewardPunishmentSetupComponent } from './reward-punishment-setup.component';

describe('RewardPunishmentSetupComponent', () => {
  let component: RewardPunishmentSetupComponent;
  let fixture: ComponentFixture<RewardPunishmentSetupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RewardPunishmentSetupComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RewardPunishmentSetupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
