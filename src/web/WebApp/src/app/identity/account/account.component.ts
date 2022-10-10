import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from "@angular/core";
import { FormBuilder, FormControl, FormControlName, FormGroup, Validators } from "@angular/forms";
import { CustomValidators } from "ng2-validation";
import { fromEvent, merge, Observable } from "rxjs";

import { DisplayMessage, GenericFormValidator, ValidationMessages } from "src/app/utils/generic-form-validator";
import { User } from "../models/user.model";
import { IdentityService } from "../services/identity.service";

@Component({
    selector: 'app-identity-account',
    templateUrl: './account.component.html'
})
export class AccountComponent implements OnInit, AfterViewInit {

    user: User;
    registerForm: FormGroup;
    errors: any[] = [];

    @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

    displayMessages: DisplayMessage = {};
    validationMessages: ValidationMessages;
    genericFormValidator: GenericFormValidator;

    constructor(private formBuilder: FormBuilder, private identityService: IdentityService) {

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
    }

    registerUser() {
        if (this.registerForm.dirty && this.registerForm.valid) {
            this.user = <User>{
                email: this.registerForm.value.email,
                password: this.registerForm.value.password,
                passwordConfirmation: this.registerForm.value.passwordConfirmation
            };

            this.identityService.registerUser(this.user)
                .subscribe({
                    next: (result) => { console.log(result) },
                    error: (error) => { console.log(error) }
                });
        }
    }
}