import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControl, FormControlName, FormGroup, Validators } from '@angular/forms';
import { CustomValidators } from 'ng2-validation';
import { fromEvent, merge, Observable } from 'rxjs';
import { User } from '../models/user.model';
import { DisplayMessage, GenericFormValidator, ValidationMessages } from '../validations/generic-form-validator';

@Component({
  selector: 'app-login',
  templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  loginForm: FormGroup;
  user: User;
  displayMessage: DisplayMessage = {};
  validationMessages: ValidationMessages;
  genericFormValidator: GenericFormValidator;

  constructor(private formBuilder: FormBuilder) {

    this.validationMessages = {
      email: {
        required: 'Preencha o E-mail',
        email: 'Preencha um E-mail válido'
      },
      password: {
        required: 'Preencha uma senha',
        rangeLength: 'A senha precisa ter entre 6 e 15 caracteres'
      },
      confirmPassword: {
        required: 'Preencha uma senha',
        rangeLength: 'A senha precisa ter entre 6 e 15 caracteres',
        equalTo: 'As senhas devem ser iguais'
      }
    };

    this.genericFormValidator = new GenericFormValidator(this.validationMessages);
  }

  ngOnInit(): void {
    let password = new FormControl('', [Validators.required, CustomValidators.rangeLength([6, 15])]);
    let confirmPassword = new FormControl('', [Validators.required, CustomValidators.rangeLength([6, 15]), CustomValidators.equalTo(password)]);

    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: password,
      confirmPassword: confirmPassword
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.displayMessage = this.genericFormValidator.processMessages(this.loginForm);
    });
  }

  login() {
    this.user = Object.assign({}, this.user, this.loginForm.value);
    console.log(this.user);
  }

}
