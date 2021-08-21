import { Component, Input, OnChanges, SimpleChanges, ViewChild } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { FileService, ExifDTO, ConfigurationDTO } from "../../api/index";


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  providers: [FileService ]
})

export class HomeComponent implements OnChanges {
  public img = null;
  public exifData: ExifDTO = null;
  public imgJson = null;

  @Input()
  startupConfiguration: ConfigurationDTO[] = null;

  @Input()
  filePath: string = null;

  @Input()
  imageDataJson: string = null;

  constructor(private fileService: FileService, private domSanitizer: DomSanitizer) {
  }

  ngOnChanges(changes: SimpleChanges){
    if(this.filePath && changes.filePath){
      if(changes.filePath.currentValue != changes.filePath.previousValue){
        if(this.filePath != null){
          this.imgJson = null;
          this.showImage();
        }
      }
    }

    if(this.imageDataJson && changes.imageDataJson){
      if(changes.imageDataJson.currentValue != changes.imageDataJson.previousValue){
        if(this.imageDataJson != null){
          this.img = null;
          this.showJson();
        }
      }
    }
  }

  private showJson() {
    this.exifData = null;
    this.imgJson = JSON.parse(this.imageDataJson);
  }

  private showImage() {
    this.fileService.apiFilesPathGet(this.filePath)
        .subscribe(result => {
          this.img = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64, ' + result.fileContents);;
        });
      
      this.exifData = null;
      this.fileService.apiFilesExifPathGet(this.filePath)
        .subscribe(result => {
          this.exifData = result;
        });
  }
}
