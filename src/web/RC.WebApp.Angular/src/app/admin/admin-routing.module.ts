import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdminComponent } from "./admin.component";


export const adminRouterConfig: Routes = [
    { path: '', component: AdminComponent }
];
@NgModule({
    declarations: [
    ],
    imports: [
        CommonModule,
        RouterModule.forChild(adminRouterConfig)
    ],
    exports: [
        RouterModule
    ]
})
export class AdminRoutingModule { }