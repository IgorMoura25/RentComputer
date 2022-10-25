import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";

import { IdentityRoutingModule } from "./identity-routing.module";

import { AccountGuard } from "./services/account.guard";
import { LoginGuard } from "./services/login.guard";

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
        CustomFormsModule,

        IdentityRoutingModule
    ],
    providers: [
        IdentityService,
        AccountGuard,
        LoginGuard
    ],
    exports: [
    ]
})
export class IdentityModule { }
