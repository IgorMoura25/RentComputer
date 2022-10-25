import { Component } from "@angular/core";
import { DomSanitizer } from "@angular/platform-browser";
import { ActivatedRoute } from "@angular/router";


import { Product } from "../models/product.model";


@Component({
    selector: 'app-product-detail',
    templateUrl: './detail-product.component.html'
})
export class DetailProductComponent {

    product: Product;
    mapAddress;

    constructor(
        private route: ActivatedRoute,
        private sanitizer: DomSanitizer) {

        this.product = this.route.snapshot.data["product"];
        this.mapAddress = this.sanitizer.bypassSecurityTrustResourceUrl("https://www.google.com/maps/embed/v1/place?q=" + this.getStockAddress() + "&key=AIzaSyC8eBkTPgaHjkXRsrFGrdJfGNSN57_-wNg")
    }

    getStockAddress(): string {
        return "Rua Francisco Bonicio, 80 - Santa Terezinha, São Bernardo do Campo - São Paulo";
    }
}