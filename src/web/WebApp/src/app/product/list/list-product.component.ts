import { Component, OnInit } from '@angular/core';

import { CatalogService } from '../services/catalog.service';

import { Product } from '../models/product.model';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-product-list',
  templateUrl: './list-product.component.html'
})
export class ListProductComponent implements OnInit {

  public products: Product[];
  errors: any[] = [];
  imagesUrl: string = environment.productImagesUrl;

  constructor(
    private catalogService: CatalogService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.spinner.show();

    this.catalogService.listProducts()
      .subscribe({
        next: (products) => {
          this.products = products;
          this.spinner.hide();
        },
        error: (fail) => {
          if (fail.error?.errors) {
            this.errors = fail.error?.errors;
          }

          this.toastr.error("Ocorreu um erro!", "Opa :(");
          this.spinner.hide();
        }
      });
  }
}
