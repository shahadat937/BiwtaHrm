import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChartWidgetsComponent } from './chart-widgets.component';

describe('ChartWidgetsComponent', () => {
  let component: ChartWidgetsComponent;
  let fixture: ComponentFixture<ChartWidgetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChartWidgetsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ChartWidgetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
