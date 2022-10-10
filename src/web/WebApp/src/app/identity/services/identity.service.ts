import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { User } from "../models/user.model";

@Injectable()
export class IdentityService {

    constructor(private httpClient: HttpClient) { }

    registerUser(user: User) {

    }

    login(user: User) {

    }
}