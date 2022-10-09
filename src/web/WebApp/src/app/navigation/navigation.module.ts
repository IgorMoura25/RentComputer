import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";

import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

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
        RouterModule,
        NgbModule
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