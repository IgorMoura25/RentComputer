import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { catchError, Observable, throwError } from "rxjs";

import { LocalStorageUtils } from "../utils/local-storage";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private router: Router) { }

    localStorageUtil = new LocalStorageUtils();

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return next.handle(req).pipe(catchError(error => {

            if (error instanceof HttpErrorResponse) {

                if (error.status === 401) {
                    this.localStorageUtil.removeUserToken();
                    this.router.navigate(['/account/login'], { queryParams: { returnUrl: this.router.url } });
                }
                if (error.status === 403) {
                    this.router.navigate(['/access-denied']);
                }
            }

            return throwError(error);
        }));
    }
}