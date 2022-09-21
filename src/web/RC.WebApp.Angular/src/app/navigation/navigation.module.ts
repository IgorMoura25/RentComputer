import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HomeComponent } from "./home/home.component";
import { NavigationRoutingModule } from "./navigation-routing.module";
import { NavigationComponent } from "./navigation.component";
import { NotFoundComponent } from "./not-found/not-found.component";


@NgModule({
    declarations: [
        NavigationComponent,
        HomeComponent,
        NotFoundComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        NavigationRoutingModule
    ],
    exports: [
        NavigationComponent,
        HomeComponent,
        NotFoundComponent,
        FormsModule
    ]
})
export class NavigationModule { }