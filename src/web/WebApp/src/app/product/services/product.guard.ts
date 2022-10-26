import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, CanLoad, Route, Router, RouterStateSnapshot, UrlSegment, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { LocalStorageUtils } from "src/app/utils/local-storage";

@Injectable()
export class ProductGuard implements CanLoad, CanActivate {

    localStorage = new LocalStorageUtils();

    constructor(private router: Router) { }

    canLoad(route: Route, segments: UrlSegment[]): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        if (!this.isLoggedIn()) {
            this.router.navigate(["/account/login"]);
        }

        return true;
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        if (!this.isLoggedIn()) {
            this.router.navigate(["/account/login"]);
        }

        if (!this.hasPermission(route)) {
            this.router.navigate(["/access-denied"]);
        }

        return true;
    }

    isLoggedIn(): boolean {
        if (this.localStorage.getUserToken()) {
            return true;
        }

        return false;
    }

    hasPermission(route: ActivatedRouteSnapshot): boolean {
        let user = this.localStorage.getUser();

        let routeClaim: any = route.data[0];

        if (routeClaim !== undefined) {
            let routeClaim = route.data[0]["claim"];

            // Se não tiver nenhuma permissão na rota, deixa passar
            // Agora, se tem permissão na rota...
            if (routeClaim) {

                // Mas o usuário não tem nenhuma permissão...
                if (!user.claims) {

                    // já dá acesso negado
                    this.router.navigate(["/access-denied"]);
                }

                // E o usuário tem alguma permissão...
                // Recupera a permissão de mesmo nome da permissão da rota
                let userClaims = user.claims.find(x => x.type === routeClaim.name);

                // Se não tem...
                if (!userClaims) {

                    // acesso negado
                    this.router.navigate(["/access-denied"]);
                }

                // Aqui o usuário tem o TIPO de permissão necessária (Produto, Fornecedor, etc)...
                // Agora veremos se ele possui os valores (Editar, Adicionar, Excluir, etc)...


                let userClaimValues = userClaims.value as String;

                // Se não tem o valor
                if (!userClaimValues.includes(routeClaim.value)) {
                    // acesso negado
                    this.router.navigate(["/access-denied"]);
                }
            }
        }

        return true;
    }
}