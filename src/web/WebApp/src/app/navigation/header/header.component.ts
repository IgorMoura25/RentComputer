import { Component } from "@angular/core";

@Component({
    selector: 'app-navigation-header',
    templateUrl: './header.component.html',
    styles: []
})
export class HeaderComponent {
    public isCollapsed: boolean;

    constructor() {
        this.isCollapsed = true;
    }
}