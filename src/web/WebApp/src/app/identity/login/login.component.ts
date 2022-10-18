import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from "@angular/core";
import { FormBuilder, FormControlName, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { fromEvent, merge, Observable } from "rxjs";

import { CustomValidators } from "ng2-validation";
import { ToastrService } from "ngx-toastr";

import { DisplayMessage, GenericFormValidator, ValidationMessages } from "src/app/utils/generic-form-validator";

import { IdentityService } from "../services/identity.service";

import { ApiAuthDataModel } from "src/app/api-models/identity/api-auth-data.model";
import { User } from "src/app/models/user.model";

@Component({
    selector: 'app-identity-login',
    templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit, AfterViewInit {

    user: User;
    loginForm: FormGroup;
    errors: any[] = [];

    @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

    displayMessages: DisplayMessage = {};
    validationMessages: ValidationMessages;
    genericFormValidator: GenericFormValidator;

    constructor(
        private formBuilder: FormBuilder,
        private identityService: IdentityService,
        private router: Router,
        private toastr: ToastrService) {

        this.validationMessages = {
            email: {
                required: 'Preencha o E-mail',
                email: 'Preencha um E-mail válido'
            },
            password: {
                required: 'Preencha uma senha',
                pattern: 'A senha precisa ter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caracter especial',
                rangeLength: 'A senha precisa ter entre 8 e 20 caracteres'
            }
        };

        this.genericFormValidator = new GenericFormValidator(this.validationMessages);
    }

    ngOnInit(): void {
        let passwordPattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{4,}$";

        this.loginForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required, Validators.pattern(passwordPattern), CustomValidators.rangeLength([8, 20])]]
        });
    }

    ngAfterViewInit(): void {
        let controlBlurs: Observable<any>[] = this.formInputElements
            .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

        merge(...controlBlurs).subscribe(() => {
            this.displayMessages = this.genericFormValidator.processMessages(this.loginForm);
        });
    }

    login() {
        if (this.loginForm.dirty && this.loginForm.valid) {

            this.user = Object.assign({}, this.user, this.loginForm.value);
            this.identityService.login(this.user)
                .subscribe({
                    next: (result) => { this.processSuccess(result); },
                    error: (fail) => { this.processError(fail); }
                });
        }
    }

    processSuccess(response: ApiAuthDataModel) {
        this.loginForm.reset();
        this.errors = [];

        this.identityService.LocalStorage.setUserToken(response.access_token);

        this.toastr.success("Login realizado com sucesso!", "Bem vindo!");
        this.router.navigate(['/catalog']);
    }

    processError(fail: any) {
        if (fail.error?.errors) {
            this.errors = fail.error?.errors;
        }
        
        this.toastr.error("Ocorreu um erro!", "Opa :(");
    }
}