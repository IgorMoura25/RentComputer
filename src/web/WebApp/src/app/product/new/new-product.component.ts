import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from "@angular/core";
import { AbstractControl, FormBuilder, FormControlName, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { fromEvent, merge, Observable } from "rxjs";

import { ToastrService } from "ngx-toastr";
import { MASKS, NgBrazilValidators } from 'ng-brazil';

import { DisplayMessage, GenericFormValidator, ValidationMessages } from "src/app/utils/generic-form-validator";

import { ApiAuthDataModel } from "src/app/api-models/identity/api-auth-data.model";

@Component({
    selector: 'app-product-new',
    templateUrl: './new-product.component.html'
})
export class NewProductComponent implements OnInit, AfterViewInit {

    productForm: FormGroup;
    errors: any[] = [];
    hasUnsavedChanges: boolean;
    valueLabel = "Valor";

    @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

    displayMessages: DisplayMessage = {};
    validationMessages: ValidationMessages;
    genericFormValidator: GenericFormValidator;
    MASKS = MASKS;

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private toastr: ToastrService) {

        this.validationMessages = {
            name: {
                required: 'Preencha o Nome do produto'
            },
            value: {
                required: 'Prencha um valor'
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
            value: ['', [Validators.required]],
            contractType: ['', [Validators.required]],
            isActive: ['', [Validators.required]]
        });

        this.productForm.patchValue({ contractType: '1', isActive: true });
    }

    ngAfterViewInit(): void {
        this.getContractTypeFormControl().valueChanges.subscribe(() => {
            this.changeValueField();
            this.configureControlValidations();
        });

        this.configureControlValidations();
    }

    changeValueField() {
        let valueControl = this.getValueFormControl();

        if (this.getContractTypeFormControl().value === "1") {
            this.valueLabel = "Valor";
            valueControl.clearValidators();
            valueControl.setValidators([Validators.required]);
        }
        else {
            this.valueLabel = "Valor Venda";
            valueControl.clearValidators();
            valueControl.setValidators([Validators.required]);
        }
    }

    getContractTypeFormControl(): AbstractControl {
        return this.productForm.get('contractType');
    }

    getValueFormControl(): AbstractControl {
        return this.productForm.get('value');
    }

    configureControlValidations() {
        let controlBlurs: Observable<any>[] = this.formInputElements
            .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

        merge(...controlBlurs).subscribe(() => {
            this.validateForm();
        });
    }

    validateForm() {
        this.displayMessages = this.genericFormValidator.processMessages(this.productForm);
        this.hasUnsavedChanges = true;
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
        this.hasUnsavedChanges = false;

        // this.identityService.LocalStorage.setUserToken(response.access_token);

        // this.toastr.success("Login realizado com sucesso!", "Bem vindo!");
        // this.router.navigate(['/catalog']);
    }

    processError(fail: any) {
        this.errors = fail.error.errors;
        this.toastr.error("Ocorreu um erro!", "Opa :(");
    }
}