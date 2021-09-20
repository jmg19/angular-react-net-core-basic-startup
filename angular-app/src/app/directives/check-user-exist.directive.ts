import { Directive, forwardRef, Input } from '@angular/core';
import { AbstractControl, NG_ASYNC_VALIDATORS, Validator } from '@angular/forms';
import { AccountsService } from "../api-services/accounts.service"

@Directive({
  selector: '[appCheckUserExist][ngModel]',
  providers: [{
    provide: NG_ASYNC_VALIDATORS, useExisting: forwardRef(() => CheckUserExistDirective), multi: true
  }],
  host: {
    '(focus)': 'onFocus($event)',
    '(blur)': 'onBlur($event)',
    '(keyup)': 'onKeyup($event)',
    '(change)': 'onChange($event)',
    '(ngModelChange)': 'onNgModelChange($event)'
  }
})
export class CheckUserExistDirective implements Validator{
  @Input('validateFormControl') validateFormControl;

  private editing: boolean;
  private wasChanged: boolean;

  onFocus($event) {
    this.wasChanged = false;
    this.editing = true;
  }
  onKeyup($event) {
      this.wasChanged = true; // keyboard change
  }
  onChange($event) {
      this.wasChanged = true; // copypaste change
  }
  onNgModelChange($event) {
      this.wasChanged = true; // ng-value change
  }

  validate(c: AbstractControl): Promise<{ [key: string]: any; }> {
    var username = c.value;
    var directive = this;

    return new Promise(resolve  => {
      if(this.editing){
        resolve(null);
      }else{
        if(username === '' || username === null){
          resolve({required: true});
        }else{
          directive.accountsService.CheckUserExistsAlready(username).subscribe(r => {
            if(r){
              resolve({userExists: true});
            }else{
              resolve(null);
            }
          });
        }
      }
    });
  }

  onBlur($event: FocusEvent) {
    this.editing = false;
    if (this.wasChanged){
      this.validateFormControl.updateValueAndValidity();
    }
  }

  registerOnValidatorChange?(fn: () => void): void {
  }

  constructor(private accountsService: AccountsService) { }

}

