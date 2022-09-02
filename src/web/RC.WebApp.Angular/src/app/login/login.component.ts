import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: 'login.component.html'
})
export class LoginComponent {

  public numberToDisplay: number = 0;
  public minhaUrl: string = "../../favicon.ico";
  public name: string = "";

  incrementNumbertToDisplay(){
    this.numberToDisplay++;
  }
}
