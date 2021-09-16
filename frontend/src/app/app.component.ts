import { Component } from '@angular/core';
import { StartupService, ConfigurationDTO } from "../api/index";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [StartupService]
})
export class AppComponent {
  title = 'cda_';

  public startupConfiguration: ConfigurationDTO[] = null;

  public filePath: string = null;

  public imageDataJson: string = null;

  constructor(private startupService: StartupService) {
    this.startupService.apiStartupGet()
      .subscribe(result => {
        this.startupConfiguration = result;
      })
  }

  getFilePathOnChange($event){
    this.filePath = $event;
    this.imageDataJson = null;
  }

  getImageDataJsonOnChange($event){
    this.imageDataJson = $event;
    this.filePath = null;
  }
}
