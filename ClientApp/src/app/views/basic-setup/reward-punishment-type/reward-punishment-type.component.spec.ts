import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RewardPunishmentTypeComponent } from './reward-punishment-type.component';

describe('RewardPunishmentTypeComponent', () => {
  let component: RewardPunishmentTypeComponent;
  let fixture: ComponentFixture<RewardPunishmentTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RewardPunishmentTypeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RewardPunishmentTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
