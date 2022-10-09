import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { HttpClientModule } from "@angular/common/http";

import { AccountComponent } from "./account/account.component";
import { IdentityComponent } from "./identity.component";
import { LoginComponent } from "./login/login.component";

import { IdentityRoutingModule } from "./identity-routing.module";

@NgModule({
    declarations: [
        IdentityComponent,
        // children
        AccountComponent,
        LoginComponent
    ],
    imports: [
        CommonModule,
        RouterModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,

        IdentityRoutingModule
    ],
    providers: [
    ],
    exports: [
    ]
})
export class IdentityModule { }