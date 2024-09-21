import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateUserInfoComponent } from './update-user-info.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import {
  ButtonGroupModule,
  ButtonModule,
  CardModule,
  CollapseDirective,
  DropdownModule,
  FormModule,
  GridModule,
  ListGroupModule,
  ModalModule,
  ProgressModule,
  SharedModule,
  SpinnerModule,
  TableModule,
  TooltipModule,
} from '@coreui/angular';
import { ToastrModule } from 'ngx-toastr';
import { UserService } from 'src/app/views/usermanagement/service/user.service';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { SharedCustomModule } from 'src/app/shared/shared.module';
import { CommonModule } from '@angular/common';
describe('UpdateUserInfoComponent', () => {
  let component: UpdateUserInfoComponent;
  let fixture: ComponentFixture<UpdateUserInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
    declarations: [UpdateUserInfoComponent],
    imports: [CommonModule,
        FormsModule,
        MatIconModule,
        SpinnerModule,
        SharedCustomModule,
        CardModule,
        ToastrModule.forRoot(),
        SharedModule,
        TableModule,
        CollapseDirective,
        DropdownModule,
        FormModule,
        GridModule,
        ListGroupModule,
        ModalModule,
        ProgressModule,
        ButtonGroupModule,
        ButtonModule,
        TooltipModule],
    providers: [
        UserService,
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
    
    fixture = TestBed.createComponent(UpdateUserInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
