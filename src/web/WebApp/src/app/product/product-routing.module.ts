import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { ProductResolve } from "./services/product.resolve";

import { ProductComponent } from "./product.component";
import { ListProductComponent } from "./list/list-product.component";
import { NewProductComponent } from "./new/new-product.component";
import { EditProductComponent } from "./edit/edit-product.component";

export const routerConfig: Routes = [
    {
        path: '', component: ProductComponent,
        children: [
            { path: '', component: ListProductComponent },
            { path: 'new', component: NewProductComponent },
            {
                path: 'edit/:guid', component: EditProductComponent,
                resolve: { product: ProductResolve }
            }
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