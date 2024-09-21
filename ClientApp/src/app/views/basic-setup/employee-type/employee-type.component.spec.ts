import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EmployeeTypeComponent } from './employee-type.component';
import { EmployeeTypeService } from '../service/employee-type.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { SpinnerModule } from '@coreui/angular';
import { ToastrModule } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';

describe('EmployeeTypeComponent', () => {
  let component: EmployeeTypeComponent;
  let fixture: ComponentFixture<EmployeeTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
    declarations: [EmployeeTypeComponent],
    imports: [FormsModule,
        MatIconModule,
        SpinnerModule,
        ToastrModule.forRoot()],
    providers: [
        EmployeeTypeService,
        {
            provide: ActivatedRoute,
            useValue: {
                snapshot: { paramMap: { get: () => 'some-value' } },
                params: of({ id: '123' })
            }
        },
        provideHttpClient(withInterceptorsFromDi())
    ]
})
    .compileComponents();
    
    fixture = TestBed.createComponent(EmployeeTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
