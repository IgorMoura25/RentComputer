import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ApiResponse } from "../../models/api-response.model";

@Injectable()
export class CatalogService{
    protected urlCatalog: string = "http://localhost:5003/catalog/";

    constructor(private httpClient: HttpClient){}

    listProducts() : Observable<ApiResponse> {
       return this.httpClient.get<ApiResponse>(this.urlCatalog + "products-anonymous");
    }
}