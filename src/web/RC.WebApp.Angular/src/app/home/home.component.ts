import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CatalogService } from '../services/catalog/catalog.service';
import { Product } from '../services/catalog/product.model';

@Component({
  selector: 'app-home',
  templateUrl: 'home.component.html'
})
export class HomeComponent implements OnInit {

  public products: Product[];
  public totalProducts: number;

  constructor(private catalogService: CatalogService){}

  ngOnInit(): void {
    this.catalogService.listProducts()
          .subscribe({
            next: (products) => {
              this.products = products.data;
              
              if(this.products != null){
                this.totalProducts = this.products.length;
              }
            },
            error: (error) => console.error(error)
          });
  }
}
