import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, CanDeactivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { LocalStorageUtils } from "src/app/utils/local-storage";

import { AccountComponent } from "../account/account.component";

@Injectable()
export class AccountGuard implements CanActivate, CanDeactivate<AccountComponent> {

    private localStorage = new LocalStorageUtils();

    constructor(private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        if (this.localStorage.getUserToken()) {
            this.router.navigate(["/catalog"]);
        }

        return true;
    }

    canDeactivate(component: AccountComponent) {
        if (component.hasUnsavedChanges) {
            return window.confirm("As informações preenchidas não serão salvas. Tem certeza que deseja sair desta página?");
        }

        return true;
    }
}