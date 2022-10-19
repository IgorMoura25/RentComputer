import { Component, OnInit } from '@angular/core';

import { CatalogService } from '../services/catalog.service';

import { Product } from '../models/product.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './list-product.component.html'
})
export class ListProductComponent implements OnInit {

  public products: Product[];
  errorMessage: string;

  constructor(private catalogService: CatalogService) { }

  ngOnInit(): void {
    this.catalogService.listProducts()
      .subscribe({
        next: (products) => { this.products = products; },
        error: (fail) => { this.errorMessage; }
      });
  }
}
