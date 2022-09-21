import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { NavigationComponent } from "./navigation.component";


export const navigationRouterConfig: Routes = [
    {
        path: '', component: NavigationComponent,
        children: [
            { path: 'home', component: HomeComponent }
        ]
    }
];
@NgModule({
    declarations: [
    ],
    imports: [
        CommonModule,
        RouterModule.forChild(navigationRouterConfig)
    ],
    exports: [
        RouterModule
    ]
})
export class NavigationRoutingModule { }