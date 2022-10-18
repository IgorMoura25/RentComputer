import { HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { throwError } from "rxjs";
import { environment } from "src/environments/environment";

import { LocalStorageUtils } from "../utils/local-storage";

export abstract class BaseHttpService {

    public LocalStorage = new LocalStorageUtils();

    protected AuthUrlServiceV1: string = environment.authorizationServerUrl;
    protected CatalogUrlServiceV1: string = environment.catalogServerUrl;

    protected getJsonHeader() {
        return {
            headers: new HttpHeaders({
                "Content-Type": "application/json"
            })
        };
    }

    protected getAuthorizationHeaderJson() {
        return {
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Authorization": `Bearer ${this.LocalStorage.getUserToken()}`
            })
        };
    }

    protected getResponseData(response: any) {
        return response.data || {};
    }

    protected handleError(response: Response | any) {
        if (response.statusText === "Unknown Error") {
            if (response instanceof HttpErrorResponse) {
                response.error.errors = ["Ocorreu um erro desconhecido"];
            }
        }

        return throwError(response);
    }
}