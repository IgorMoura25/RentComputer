import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";

import { ProductRoutingModule } from "./product-routing.module";

import { ProductComponent } from "./product.component";
import { NewProductComponent } from "./new/new-product.component";

import { CustomFormsModule } from "ng2-validation";

@NgModule({
    declarations: [
        ProductComponent,
        // children
        NewProductComponent
    ],
    imports: [
        CommonModule,
        RouterModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        CustomFormsModule,

        ProductRoutingModule
    ],
    providers: [
    ],
    exports: [
    ]
})
export class ProductModule { }
