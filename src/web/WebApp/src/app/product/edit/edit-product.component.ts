import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from "@angular/core";
import { AbstractControl, FormBuilder, FormControlName, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { fromEvent, merge, Observable } from "rxjs";

import { DisplayMessage, GenericFormValidator, ValidationMessages } from "src/app/utils/generic-form-validator";

import { CatalogService } from "../services/catalog.service";
import { Product } from "../models/product.model";

import { ToastrService } from "ngx-toastr";
import { MASKS } from 'ng-brazil';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
    selector: 'app-product-edit',
    templateUrl: './edit-product.component.html'
})
export class EditProductComponent implements OnInit, AfterViewInit {

    productForm: FormGroup;
    product: Product;
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
        private catalogService: CatalogService,
        private router: Router,
        private route: ActivatedRoute,
        private toastr: ToastrService,
        private spinner: NgxSpinnerService) {

        this.validationMessages = {
            name: {
                required: 'Preencha o Nome do produto'
            },
            quantity: {
                required: 'Preencha uma quantidade'
            },
            value: {
                required: 'Preencha um valor'
            },
            contractType: {
                required: 'Escolha um tipo de Contrato'
            },
            isActive: {
                required: 'Escolha se o produto está ativo'
            }
        };

        this.genericFormValidator = new GenericFormValidator(this.validationMessages);
        this.product = this.route.snapshot.data["product"];
    }

    ngOnInit(): void {
        this.spinner.show();

        this.productForm = this.formBuilder.group({
            name: ['', [Validators.required]],
            description: [''],
            quantity: ['', [Validators.required]],
            value: ['', [Validators.required]],
            contractType: ['', [Validators.required]],
            isActive: ['', [Validators.required]]
        });

        this.fillForm();
    }

    ngAfterViewInit(): void {
        this.getContractTypeFormControl().valueChanges.subscribe(() => {
            this.changeValueField();
            this.configureControlValidations();
        });

        this.configureControlValidations();
        
        this.spinner.hide();
    }

    fillForm() {
        this.productForm.patchValue({
            name: this.product.name,
            description: this.product.description,
            quantity: this.product.quantity,
            value: this.product.value,
            contractType: "1",
            isActive: this.product.isActive
        });
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

    edit() {
        this.spinner.show();

        if (!this.productForm.valid) {
            this.spinner.hide();
            return;
        }

        if (this.productForm.dirty) {
            console.log(this.productForm.value);

            this.product = Object.assign({}, this.product, this.productForm.value);

            // Editar no backend...
        }
        else {
            this.toastr.info("Não há alterações para salvar", "Editar");
            this.spinner.hide();
        }
    }

    processSuccess() {
        this.productForm.reset();
        this.errors = [];
        this.hasUnsavedChanges = false;

        this.toastr.success("Produto editado com sucesso!", "Editar");
        this.router.navigate(['/product']);

        this.spinner.hide();
    }

    processError(fail: any) {
        if (fail.error?.errors) {
            this.errors = fail.error?.errors;
        }

        this.toastr.error("Ocorreu um erro!", "Opa :(");

        this.spinner.hide();
    }
}