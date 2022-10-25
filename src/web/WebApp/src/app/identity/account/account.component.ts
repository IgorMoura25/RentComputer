import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from "@angular/core";
import { FormBuilder, FormControl, FormControlName, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { fromEvent, merge, Observable } from "rxjs";

import { CustomValidators } from "ng2-validation";
import { ToastrService } from "ngx-toastr";

import { DisplayMessage, GenericFormValidator, ValidationMessages } from "src/app/utils/generic-form-validator";

import { IdentityService } from "../services/identity.service";

import { User } from "src/app/models/user.model";
import { ApiAuthDataModel } from "src/app/api-models/identity/api-auth-data.model";
import { NgxSpinnerService } from "ngx-spinner";

@Component({
    selector: 'app-identity-account',
    templateUrl: './account.component.html'
})
export class AccountComponent implements OnInit, AfterViewInit {

    user: User;
    registerForm: FormGroup;
    errors: any[] = [];
    hasUnsavedChanges: boolean;

    @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

    displayMessages: DisplayMessage = {};
    validationMessages: ValidationMessages;
    genericFormValidator: GenericFormValidator;

    constructor(
        private formBuilder: FormBuilder,
        private identityService: IdentityService,
        private router: Router,
        private toastr: ToastrService,
        private spinner: NgxSpinnerService) {

        this.validationMessages = {
            email: {
                required: 'Preencha o E-mail',
                email: 'Preencha um E-mail válido'
            },
            password: {
                required: 'Preencha uma senha',
                pattern: 'A senha precisa ter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caracter especial',
                rangeLength: 'A senha precisa ter entre 8 e 20 caracteres'
            },
            passwordConfirmation: {
                required: 'Preencha uma senha',
                pattern: 'A senha precisa ter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caracter especial',
                rangeLength: 'A senha precisa ter entre 8 e 20 caracteres',
                equalTo: 'As senhas devem ser iguais'
            }
        };

        this.genericFormValidator = new GenericFormValidator(this.validationMessages);
    }

    ngOnInit(): void {
        this.spinner.show();

        let passwordPattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{4,}$";

        let password = new FormControl('', [Validators.required, Validators.pattern(passwordPattern), CustomValidators.rangeLength([8, 20])]);
        let passwordConfirmation = new FormControl('', [Validators.required, Validators.pattern(passwordPattern), CustomValidators.rangeLength([8, 20]), CustomValidators.equalTo(password)]);

        this.registerForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.email]],
            password: password,
            passwordConfirmation: passwordConfirmation
        });
    }

    ngAfterViewInit(): void {
        let controlBlurs: Observable<any>[] = this.formInputElements
            .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

        merge(...controlBlurs).subscribe(() => {
            this.displayMessages = this.genericFormValidator.processMessages(this.registerForm);
        });

        this.hasUnsavedChanges = true;
        this.spinner.hide();
    }

    registerUser() {
        if (this.registerForm.dirty && this.registerForm.valid) {
            this.spinner.show();

            this.user = Object.assign({}, this.user, this.registerForm.value);
            this.identityService.registerUser(this.user)
                .subscribe({
                    next: (result) => { this.processSuccess(result); },
                    error: (fail) => { this.processError(fail); }
                });
        }
    }

    processSuccess(response: ApiAuthDataModel) {
        this.registerForm.reset();
        this.errors = [];

        this.identityService.LocalStorage.setUserToken(response.access_token);

        this.hasUnsavedChanges = false;
        this.spinner.hide();

        this.toastr.success("Registro realizado com sucesso!", "Bem vindo!");
        this.router.navigate(['/catalog']);
    }

    processError(fail: any) {
        if (fail.error?.errors) {
            this.errors = fail.error?.errors;
        }

        this.toastr.error("Ocorreu um erro!", "Opa :(");
        this.spinner.hide();
    }
}