import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { APP_BASE_HREF } from '@angular/common';

import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
registerLocaleData(localePt);

import { AppComponent } from './app.component';
import { AppRoutesModule } from './app-routes.module';
import { HomeComponent } from './home/home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { CatalogService } from './services/catalog/catalog.service';
import { HttpClientModule } from '@angular/common/http';
import { CustomFormsModule } from 'ng2-validation';
import { LayoutModule } from './layout/layout.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    CustomFormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    LayoutModule,
    AppRoutesModule
  ],
  providers: [
    CatalogService,
    { provide: APP_BASE_HREF, useValue: '/' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
