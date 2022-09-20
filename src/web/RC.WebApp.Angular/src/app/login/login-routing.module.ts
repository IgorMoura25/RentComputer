import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LoginComponent } from "./login.component";


export const loginRouterConfig: Routes = [
    // Se alguém chamar alguma coisa relativa a login, cai no LoginComponent
    // por ser o único componente ele irá atender pela rota em branco porque existe apenas um componente
    { path: '', component: LoginComponent }
];
@NgModule({
    declarations: [
    ],
    imports: [
        CommonModule,
        // forChild porque é um submódulo (filho)
        // ele não é o principal
        RouterModule.forChild(loginRouterConfig)
    ],
    exports: [
        RouterModule
    ]
})
export class LoginRoutingModule { }