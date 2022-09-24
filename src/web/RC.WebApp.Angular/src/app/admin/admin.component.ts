import { Component } from '@angular/core';
import { FileSizePipe } from '../pipes/filesize.pipe';

@Component({
  selector: 'app-admin',
  templateUrl: 'admin.component.html'
})
export class AdminComponent {

  formattedSize: string = this.filesizePipe.transform(2345678);

  constructor(private filesizePipe: FileSizePipe) { }

}
