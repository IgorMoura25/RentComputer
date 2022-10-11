import { Component } from "@angular/core";
import { Router } from "@angular/router";

import { LocalStorageUtils } from "src/app/utils/local-storage";

@Component({
    selector: 'app-navigation-header-login',
    templateUrl: './header-login.component.html',
    styles: []
})
export class HeaderLoginComponent {

    token: string = "";
    user: any;
    email: string = "";
    localStorageUtils = new LocalStorageUtils();

    constructor(private router: Router) { }

    isUserLoggedIn(): boolean {
        this.token = this.localStorageUtils.getUserToken();

        this.email = "pegar-email-do-token@teste.com";

        return this.token !== null;
    }

    logout() {
        this.localStorageUtils.removeUserToken();
        this.router.navigate(['/catalog']);
    }
}
