import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FieldPrintComponent } from './field-print.component';

describe('FieldPrintComponent', () => {
  let component: FieldPrintComponent;
  let fixture: ComponentFixture<FieldPrintComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FieldPrintComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FieldPrintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
