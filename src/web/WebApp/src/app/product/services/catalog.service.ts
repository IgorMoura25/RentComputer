import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, map, Observable } from "rxjs";

import { BaseHttpService } from "src/app/services/base-http.service";

import { AddProductApiModel } from "src/app/api-models/product/add-product.model";
import { Product } from "../models/product.model";

import { StringUtils } from "src/app/utils/string-utils";

@Injectable()
export class CatalogService extends BaseHttpService {

    constructor(private httpClient: HttpClient) { super(); }

    addProduct(product: Product): Observable<any> {
        let apiModel = <AddProductApiModel>{
            name: product.name,
            description: StringUtils.nullIfEmpty(product.description),
            value: product.value,
            quantity: product.quantity
        };

        return this.httpClient
            .post(this.CatalogUrlServiceV1 + 'catalog/product', apiModel, this.getAuthorizationHeaderJson())
            .pipe(
                map(this.getResponseData),
                catchError(this.handleError));
    }

    listProducts(): Observable<Product[]> {
        return this.httpClient
            .get(this.CatalogUrlServiceV1 + 'catalog/products', this.getAuthorizationHeaderJson())
            .pipe(
                map(this.getResponseData),
                catchError(this.handleError));
    }

    getProductByGuid(guid: string): Observable<Product> {
        return this.httpClient
            .get(this.CatalogUrlServiceV1 + 'catalog/product/' + guid, this.getAuthorizationHeaderJson())
            .pipe(
                map(this.getResponseData),
                catchError(this.handleError));
    }
}