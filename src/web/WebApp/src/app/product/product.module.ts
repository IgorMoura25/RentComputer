import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";

import { ProductRoutingModule } from "./product-routing.module";

import { ProductResolve } from "./services/product.resolve";
import { CatalogService } from "./services/catalog.service";

import { ProductComponent } from "./product.component";
import { NewProductComponent } from "./new/new-product.component";
import { ListProductComponent } from "./list/list-product.component";
import { EditProductComponent } from "./edit/edit-product.component";
import { DetailProductComponent } from "./detail/detail-product.component";

import { CustomFormsModule } from "ng2-validation";
import { NgBrazil } from "ng-brazil";
import { TextMaskModule } from "angular2-text-mask";
import { CurrencyMaskConfig, CurrencyMaskModule, CURRENCY_MASK_CONFIG } from 'ng2-currency-mask';

export const CustomCurrencyMaskConfig: CurrencyMaskConfig = {
    align: "left",
    allowNegative: true,
    decimal: ",",
    precision: 2,
    prefix: "R$ ",
    suffix: "",
    thousands: "."
};

@NgModule({
    declarations: [
        ProductComponent,
        // children
        NewProductComponent,
        ListProductComponent,
        EditProductComponent,
        DetailProductComponent
    ],
    imports: [
        CommonModule,
        RouterModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        CustomFormsModule,
        TextMaskModule,
        CurrencyMaskModule,

        ProductRoutingModule
    ],
    providers: [
        CatalogService,
        ProductResolve,
        { provide: CURRENCY_MASK_CONFIG, useValue: CustomCurrencyMaskConfig }
    ],
    exports: [
    ]
})
export class ProductModule { }
