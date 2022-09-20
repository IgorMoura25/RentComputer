import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";


export const loginRouterConfig: Routes = [
    { path: '', component: HomeComponent }
];
@NgModule({
    declarations: [
    ],
    imports: [
        CommonModule,
        RouterModule.forChild(loginRouterConfig)
    ],
    exports: [
        RouterModule
    ]
})
export class NavigationRoutingModule { }