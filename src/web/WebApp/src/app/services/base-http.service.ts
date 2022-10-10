import { HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { throwError } from "rxjs";

export abstract class BaseHttpService {

    protected UrlServiceV1: string = "https://localhost:7241/";

    protected getJsonHeader() {
        return {
            headers: new HttpHeaders({
                "Content-Type": "application/json"
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