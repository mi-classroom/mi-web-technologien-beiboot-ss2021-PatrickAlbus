import { Component, Input, OnChanges } from '@angular/core';
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

  @Input()
  filePath: string = null;

  constructor(private startupService: StartupService, private fileService: FileService, private domSanitizer: DomSanitizer) {
    this.startupService.apiStartupGet()
      .subscribe(result => {
        this.configurationDTO = result;
      });
  }

  ngOnChanges(){
    if(this.filePath != null){
      this.showImage();
    }
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
