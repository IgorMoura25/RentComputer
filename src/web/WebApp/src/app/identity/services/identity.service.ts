import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, map, Observable } from "rxjs";

import { BaseHttpService } from "src/app/services/base-http.service";

import { User } from "../models/user.model";

@Injectable()
export class IdentityService extends BaseHttpService {

    constructor(private httpClient: HttpClient) { super(); }

    registerUser(user: User): Observable<User> {
        return this.httpClient
            .post(this.UrlServiceV1 + 'auth/user', user)
            .pipe(
                map(this.getResponseData),
                catchError(this.handleError));
    }

    login(user: User) {

    }
}