import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { ProductComponent } from "./product.component";
import { NewProductComponent } from "./new/new-product.component";

export const routerConfig: Routes = [
    {
        path: '', component: ProductComponent,
        children: [
            { path: 'new', component: NewProductComponent }
        ]
    }
];
@NgModule({
    declarations: [
    ],
    imports: [
        CommonModule,
        RouterModule.forChild(routerConfig)
    ],
    exports: [
        RouterModule
    ]
})
export class ProductRoutingModule { }