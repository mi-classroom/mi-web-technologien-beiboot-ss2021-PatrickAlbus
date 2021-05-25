import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

  public filePath: string = null;

  getFilePathOnChange($event){
    console.log($event);
    this.filePath = $event;
  }
}
