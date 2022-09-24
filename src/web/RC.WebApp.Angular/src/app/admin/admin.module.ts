import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { AdminComponent } from "./admin.component";
import { AdminRoutingModule } from "./admin-routing.module";
import { FileSizePipe } from "../pipes/filesize.pipe";

@NgModule({
    declarations: [
        AdminComponent,
        FileSizePipe
    ],
    imports: [
        CommonModule,
        AdminRoutingModule
    ],
    exports: [
    ],
    providers: [
        FileSizePipe
    ]
})
export class AdminModule { }