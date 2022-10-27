import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from "@angular/core";
import { AbstractControl, FormBuilder, FormControlName, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { fromEvent, merge, Observable } from "rxjs";

import { DisplayMessage, GenericFormValidator, ValidationMessages } from "src/app/utils/generic-form-validator";

import { CatalogService } from "../services/catalog.service";

import { Product } from "../models/product.model";

import { ToastrService } from "ngx-toastr";
import { MASKS } from 'ng-brazil';
import { NgxSpinnerService } from "ngx-spinner";
import { Dimensions, ImageCroppedEvent, ImageTransform, LoadedImage } from "ngx-image-cropper";

@Component({
    selector: 'app-product-new',
    templateUrl: './new-product.component.html'
})
export class NewProductComponent implements OnInit, AfterViewInit {

    productForm: FormGroup;
    product: Product;
    errors: any[] = [];
    hasUnsavedChanges: boolean;
    valueLabel = "Valor";

    imageChangedEvent: any = '';
    croppedImage: any = '';
    canvasRotation = 0;
    rotation = 0;
    scale = 1;
    showCropper = false;
    containWithinAspectRatio = false;
    transform: ImageTransform = {};
    imageUrl: string;
    imageName: string;

    @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

    displayMessages: DisplayMessage = {};
    validationMessages: ValidationMessages;
    genericFormValidator: GenericFormValidator;
    MASKS = MASKS;

    constructor(
        private formBuilder: FormBuilder,
        private catalogService: CatalogService,
        private router: Router,
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
            },
            image: {
                required: 'Escolha uma imagem para o produto'
            }
        };

        this.genericFormValidator = new GenericFormValidator(this.validationMessages);
    }

    ngOnInit(): void {
        this.spinner.show();

        this.productForm = this.formBuilder.group({
            name: ['', [Validators.required]],
            description: [''],
            quantity: ['', [Validators.required]],
            value: ['', [Validators.required]],
            contractType: ['', [Validators.required]],
            isActive: ['', [Validators.required]],
            image: ['', [Validators.required]]
        });

        this.productForm.patchValue({ contractType: '1', isActive: true });
    }

    ngAfterViewInit(): void {
        this.getContractTypeFormControl().valueChanges.subscribe(() => {
            this.changeValueField();
            this.configureControlValidations();
        });

        this.configureControlValidations();

        this.spinner.hide();
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
            this.spinner.show();

            console.log(this.productForm.value);

            this.product = Object.assign({}, this.product, this.productForm.value);

            this.product.imageName = this.imageName;
            this.product.imageBase64 = this.croppedImage.split(',')[1];

            this.catalogService.addProduct(this.product)
                .subscribe({
                    next: (result) => { this.processSuccess(); },
                    error: (fail) => { this.processError(fail); }
                });
        }
    }

    processSuccess() {
        this.productForm.reset();
        this.errors = [];
        this.hasUnsavedChanges = false;
        this.spinner.hide();

        this.toastr.success("Produto cadastrado com sucesso!", "Cadastro");
        this.router.navigate(['/product']);
    }

    processError(fail: any) {
        if (fail.error?.errors) {
            this.errors = fail.error?.errors;
        }

        this.toastr.error("Ocorreu um erro!", "Opa :(");
        this.spinner.hide();
    }

    // Image cropper

    fileChangeEvent(event: any): void {
        this.imageChangedEvent = event;
        this.imageName = event.currentTarget.files[0].name;
    }

    imageCropped(event: ImageCroppedEvent) {
        this.croppedImage = event.base64;
    }

    imageLoaded(image: LoadedImage) {
        this.showCropper = true;
    }

    cropperReady(sourceImageDimensions: Dimensions) {
        console.log("Cropper ready", sourceImageDimensions);
    }

    loadImageFailed() {
        this.errors.push("O formato do arquivo " + this.imageName + " não é aceito");
    }
}