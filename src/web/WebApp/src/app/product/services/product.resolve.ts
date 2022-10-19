import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";

import { Product } from "../models/product.model";

import { CatalogService } from "./catalog.service";

@Injectable()
export class ProductResolve implements Resolve<Product>{
    constructor(private catalogService: CatalogService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Product | Observable<Product> | Promise<Product> {
        return this.catalogService.getProductByGuid(route.params["guid"]);
    }
}