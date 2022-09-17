import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CustomFormsModule } from "ng2-validation";
import { LoginRoutesModule } from "./login-routes.module";
import { LoginComponent } from "./login.component";

@NgModule({
    declarations: [
        LoginComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        CustomFormsModule,
        ReactiveFormsModule,
        LoginRoutesModule
    ],
    exports: [
        LoginComponent,
        FormsModule,
        CustomFormsModule,
        ReactiveFormsModule
    ]
})
export class LoginModule { }