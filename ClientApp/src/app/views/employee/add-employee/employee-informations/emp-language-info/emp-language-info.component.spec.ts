import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpLanguageInfoComponent } from './emp-language-info.component';

describe('EmpLanguageInfoComponent', () => {
  let component: EmpLanguageInfoComponent;
  let fixture: ComponentFixture<EmpLanguageInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpLanguageInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpLanguageInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
