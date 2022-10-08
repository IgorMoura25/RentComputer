import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";

import { CatalogComponent } from "./catalog/catalog.component";
import { FooterComponent } from "./footer/footer.component";
import { HeaderComponent } from "./header/header.component";
import { NotFoundComponent } from "./not-found/not-found.component";

@NgModule({
    declarations: [
        CatalogComponent,
        FooterComponent,
        HeaderComponent,
        NotFoundComponent
    ],
    imports: [
        CommonModule,
        RouterModule
    ],
    providers: [
    ],
    exports: [
        CatalogComponent,
        FooterComponent,
        HeaderComponent,
        NotFoundComponent
    ]
})
export class NavigationModule { }