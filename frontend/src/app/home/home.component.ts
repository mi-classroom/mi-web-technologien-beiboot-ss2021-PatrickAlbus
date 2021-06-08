import { Component, Input, OnChanges, SimpleChanges, ViewChild } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { DirectoryService, DirectoryDTO, FileService, FileDTO, ExifDTO, StartupService, ConfigurationDTO } from "../../api/index";



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [StartupService, FileService ]
})

export class HomeComponent implements OnChanges {
  private configurationDTO: ConfigurationDTO[] = [{
    title: null,
    values: []
  }];
  public img = null;
  private exifData: ExifDTO[] = [];
  public imgJson = null;

  @Input()
  filePath: string = null;

  @Input()
  imageDataJson: string = null;

  constructor(private startupService: StartupService, private fileService: FileService, private domSanitizer: DomSanitizer) {
    this.startupService.apiStartupGet()
      .subscribe(result => {
        this.configurationDTO = result;
      });
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
    this.imgJson = JSON.parse(this.imageDataJson);
  }

  private showImage() {
    this.fileService.apiFilesPathGet(this.filePath)
        .subscribe(result => {
          this.img = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64, ' + result.fileContents);;
        });
      
      this.exifData = [];
      this.fileService.apiFilesExifPathGet(this.filePath)
        .subscribe(result => {
          result.forEach(obj => {
            this.configurationDTO.forEach(config => {
              if (obj.name == config.title) {
                obj.tags.forEach(tag => {
                  config.values.forEach(configValue => {
                    if (tag.name == configValue) {
                      this.exifData.push({
                        exifName: tag.name,
                        exifDescription: tag.description
                      });
                    }
                  })
                })
              }
            })
          })
        })
  }
}
