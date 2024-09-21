import { Component, Directive } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { ChangeProfileComponent } from './change-profile.component';
import { ToastrModule } from 'ngx-toastr';
import { EmpPhotoSignService } from '../../employee/service/emp-photo-sign.service';
import { SpinnerModule } from '@coreui/angular';

// Create a mock directive
@Directive({
  selector: '[gutter]'
})
class MockGutterDirective {}

describe('ChangeProfileComponent', () => {
  let component: ChangeProfileComponent;
  let fixture: ComponentFixture<ChangeProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
    declarations: [
        ChangeProfileComponent,
        MockGutterDirective // Declare the mock directive
    ],
    imports: [FormsModule,
        MatIconModule,
        SpinnerModule,
        ToastrModule.forRoot()],
    providers: [EmpPhotoSignService, provideHttpClient(withInterceptorsFromDi())]
})
    .compileComponents();
    
    fixture = TestBed.createComponent(ChangeProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
