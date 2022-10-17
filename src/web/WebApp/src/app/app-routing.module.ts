import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CatalogComponent } from './navigation/catalog/catalog.component'
import { NotFoundComponent } from './navigation/not-found/not-found.component';

const routes: Routes = [
  { path: '', redirectTo: '/catalog', pathMatch: 'full' },
  { path: 'catalog', component: CatalogComponent },
  {
    path: 'account', loadChildren: () => import('./identity/identity.module').then(m => m.IdentityModule)
  },
  {
    path: 'product', loadChildren: () => import('./product/product.module').then(m => m.ProductModule)
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
