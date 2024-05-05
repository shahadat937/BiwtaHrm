import { Directive } from '@angular/core';
import { NG_VALIDATORS, AbstractControl, ValidationErrors, Validator } from '@angular/forms';

@Directive({
  selector: '[appAppBloodGroupPattern]',
  providers: [{ provide: NG_VALIDATORS, useExisting: AppBloodGroupPatternDirective, multi: true }]
})
export class AppBloodGroupPatternDirective implements Validator{

  validate(control: AbstractControl): ValidationErrors | null {
    const bloodGroupPattern = /^[ABO][+-]$/; // Define your blood group pattern here
    if (control.value && !bloodGroupPattern.test(control.value)) {
      return { 'invalidBloodGroup': true };
    }
    return null;
  }

}
