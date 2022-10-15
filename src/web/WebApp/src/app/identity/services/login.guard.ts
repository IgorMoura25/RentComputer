import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { LocalStorageUtils } from "src/app/utils/local-storage";


@Injectable()
export class LoginGuard implements CanActivate {

    private localStorage = new LocalStorageUtils();

    constructor(private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        if (this.localStorage.getUserToken()) {
            this.router.navigate(["/catalog"]);
        }

        return true;
    }
}