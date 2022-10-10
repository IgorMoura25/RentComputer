import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { HttpClientModule } from "@angular/common/http";

import { IdentityRoutingModule } from "./identity-routing.module";

import { IdentityComponent } from "./identity.component";
import { AccountComponent } from "./account/account.component";
import { LoginComponent } from "./login/login.component";

import { IdentityService } from "./services/identity.service";
import { CustomFormsModule } from "ng2-validation";

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
        CustomFormsModule,

        IdentityRoutingModule
    ],
    providers: [
        IdentityService
    ],
    exports: [
    ]
})
export class IdentityModule { }