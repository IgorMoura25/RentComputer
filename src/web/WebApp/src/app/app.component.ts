import { AfterViewInit, Component, OnInit } from '@angular/core';

import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, AfterViewInit {
  title = 'Rent Computer';

  constructor(private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.spinner.show();
  }

  ngAfterViewInit(): void {
    this.spinner.hide();
  }
}
