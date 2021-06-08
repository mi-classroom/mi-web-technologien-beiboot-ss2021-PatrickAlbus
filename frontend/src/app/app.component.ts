import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

  public filePath: string = null;

  public imageDataJson: string = null;

  getFilePathOnChange($event){
    this.filePath = $event;
  }

  getImageDataJsonOnChange($event){
    this.imageDataJson = $event;
  }
}
