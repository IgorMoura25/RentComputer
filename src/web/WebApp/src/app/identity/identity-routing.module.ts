import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { AccountGuard } from "./services/account.guard";

import { IdentityComponent } from "./identity.component";
import { AccountComponent } from "./account/account.component";
import { LoginComponent } from "./login/login.component";

export const routerConfig: Routes = [
    {
        path: '', component: IdentityComponent,
        children: [
            { path: 'register', component: AccountComponent, canActivate: [AccountGuard], canDeactivate: [AccountGuard] },
            { path: 'login', component: LoginComponent }
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
export class IdentityRoutingModule { }