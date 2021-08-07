import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';

  public filePath: string = null;

  public imageDataJson: string = null;

  getFilePathOnChange($event){
    this.filePath = $event;
    this.imageDataJson = null;
  }

  getImageDataJsonOnChange($event){
    this.imageDataJson = $event;
    this.filePath = null;
  }
}
