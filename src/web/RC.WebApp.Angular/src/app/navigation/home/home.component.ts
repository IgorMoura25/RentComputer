import { Component, OnInit } from '@angular/core';
import { CatalogService } from '../../services/catalog/catalog.service';
import { Product } from '../../models/product.model';
import { AppNavigation } from 'src/app/models/navigation.interface';

@Component({
  selector: 'app-navigation-home',
  templateUrl: 'home.component.html'
})
export class HomeComponent implements OnInit {

  public products: Product[];
  public totalProducts: number;
  public numberToDisplay: number = 0;
  public minhaUrl: string = "../../favicon.ico";
  public name: string = "";

  public nav: AppNavigation[] = [
    { link: '/login', name: 'Login', isExact: true }
  ];

  constructor(private catalogService: CatalogService) { }

  ngOnInit(): void {
    this.catalogService.listProducts()
      .subscribe({
        next: (products) => {
          this.products = products.data;

          if (this.products != null) {
            this.totalProducts = this.products.length;
          }
        },
        error: (error) => console.error(error)
      });
  }

  incrementNumbertToDisplay() {
    this.numberToDisplay++;
  }
}
