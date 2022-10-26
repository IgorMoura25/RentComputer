import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AccessDeniedComponent } from './navigation/access-denied/access-denied.component';
import { CatalogComponent } from './navigation/catalog/catalog.component'
import { NotFoundComponent } from './navigation/not-found/not-found.component';
import { ProductGuard } from './product/services/product.guard';

const routes: Routes = [
  { path: '', redirectTo: '/catalog', pathMatch: 'full' },
  { path: 'access-denied', component: AccessDeniedComponent },
  { path: 'catalog', component: CatalogComponent },
  {
    path: 'account', loadChildren: () => import('./identity/identity.module').then(m => m.IdentityModule)
  },
  {
    path: 'product',
    canLoad: [ProductGuard],
    loadChildren: () => import('./product/product.module').then(m => m.ProductModule)
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
