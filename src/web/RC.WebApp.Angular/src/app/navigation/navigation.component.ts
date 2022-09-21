import { Component } from '@angular/core';

@Component({
  selector: 'app-navigation',
  template: '<app-layout-header></app-layout-header><router-outlet></router-outlet><app-layout-header></app-layout-header>'
})
export class NavigationComponent { }
