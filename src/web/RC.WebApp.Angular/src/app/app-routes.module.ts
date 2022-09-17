import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";

export const rootRouterConfig: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'login', loadChildren: () => import('./login/login.module').then(m => m.LoginModule) }
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
