import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { LoginComponent } from "./login/login.component";

export const rootRouterConfig: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'login', component: LoginComponent }
];
@NgModule({
    declarations: [
    ],
    imports: [
        CommonModule,
        RouterModule.forRoot(rootRouterConfig, { useHash: false })
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutesModule { }
