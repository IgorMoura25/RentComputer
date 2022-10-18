import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, map, Observable } from "rxjs";

import { BaseHttpService } from "src/app/services/base-http.service";

import { User } from "src/app/models/user.model";
import { CreateUserApiModel } from "src/app/api-models/identity/create-user.model";
import { LoginUserApiModel } from "src/app/api-models/identity/login-user.model";
import { ApiAuthDataModel } from "src/app/api-models/identity/api-auth-data.model";

@Injectable()
export class IdentityService extends BaseHttpService {

    constructor(private httpClient: HttpClient) { super(); }

    registerUser(user: User): Observable<ApiAuthDataModel> {
        let createUserApiModel = <CreateUserApiModel>{
            email: user.email,
            password: user.password,
            passwordConfirmation: user.passwordConfirmation
        };

        return this.httpClient
            .post(this.AuthUrlServiceV1 + 'auth/user', createUserApiModel)
            .pipe(
                map(this.getResponseData),
                catchError(this.handleError));
    }

    login(user: User): Observable<ApiAuthDataModel> {
        let loginUserApiModel = <LoginUserApiModel>{
            email: user.email,
            password: user.password
        };

        return this.httpClient
            .post(this.AuthUrlServiceV1 + 'auth/login', loginUserApiModel)
            .pipe(
                map(this.getResponseData),
                catchError(this.handleError));
    }
}