import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from "@angular/core";
import { FormBuilder, FormControlName, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { fromEvent, merge, Observable } from "rxjs";

import { ToastrService } from "ngx-toastr";

import { DisplayMessage, GenericFormValidator, ValidationMessages } from "src/app/utils/generic-form-validator";

import { ApiAuthDataModel } from "src/app/api-models/identity/api-auth-data.model";

@Component({
    selector: 'app-product-new',
    templateUrl: './new-product.component.html'
})
export class NewProductComponent implements OnInit, AfterViewInit {

    productForm: FormGroup;
    errors: any[] = [];

    @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

    displayMessages: DisplayMessage = {};
    validationMessages: ValidationMessages;
    genericFormValidator: GenericFormValidator;

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private toastr: ToastrService) {

        this.validationMessages = {
            name: {
                required: 'Preencha o Nome do produto'
            },
            contractType: {
                required: 'Escolha um tipo de Contrato'
            },
            isActive: {
                required: 'Escolha se o produto está ativo'
            }
        };

        this.genericFormValidator = new GenericFormValidator(this.validationMessages);
    }

    ngOnInit(): void {
        this.productForm = this.formBuilder.group({
            name: ['', [Validators.required]],
            description: [''],
            contractType: ['', [Validators.required]],
            isActive: ['', [Validators.required]]
        });

        this.productForm.patchValue({ contractType: '1', isActive: true });
    }

    ngAfterViewInit(): void {
        let controlBlurs: Observable<any>[] = this.formInputElements
            .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

        merge(...controlBlurs).subscribe(() => {
            this.displayMessages = this.genericFormValidator.processMessages(this.productForm);
        });
    }

    add() {
        if (this.productForm.dirty && this.productForm.valid) {

            console.log(this.productForm.value);

            // adicionar no backend...
        }
    }

    processSuccess(response: ApiAuthDataModel) {
        // this.loginForm.reset();
        // this.errors = [];

        // this.identityService.LocalStorage.setUserToken(response.access_token);

        // this.toastr.success("Login realizado com sucesso!", "Bem vindo!");
        // this.router.navigate(['/catalog']);
    }

    processError(fail: any) {
        this.errors = fail.error.errors;
        this.toastr.error("Ocorreu um erro!", "Opa :(");
    }
}