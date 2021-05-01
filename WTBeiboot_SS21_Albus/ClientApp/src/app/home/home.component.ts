import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { DirectoryService, DirectoryDTO, FileService, FileDTO, ExifDTO, StartupService, ConfigurationDTO } from "../../api/index";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [StartupService, DirectoryService, FileService ]
})

export class HomeComponent {
  public directoryPaths: DirectoryDTO[] = [{
    childDirectories: null,
    directoryName: null,
    directoryPath: null,
    files: null
  }];
  public configurationDTO: ConfigurationDTO[] = [{
    title: null,
    values: []
  }];
  public currentPath: DirectoryDTO[] = []
  public img = null;
  public exifData: ExifDTO[] = [];

  constructor(private startupService: StartupService, private directoryService: DirectoryService, private fileService: FileService, private domSanitizer: DomSanitizer) {
    this.startupService.apiStartupGet()
      .subscribe(result => {
        this.configurationDTO = result;
        console.log(this.configurationDTO);
        this.getDirectoryAndFilePaths();
      });
  }

  public getDirectoryAndFilePaths(_directoryDTO?: DirectoryDTO) {
    var path = (_directoryDTO != null) ? _directoryDTO.directoryPath : null;

    this.directoryService.apiDirectoriesGet(path)
      .subscribe(result => {
        this.directoryPaths = result;
        this.currentPath.push(this.directoryPaths[0]);
      })
  }

  public goToPreviousDirectory(index: any) {
    var _directoryDTO: DirectoryDTO = this.currentPath[index];
    this.currentPath = this.currentPath.slice(0, index);
    this.getDirectoryAndFilePaths(_directoryDTO);
  }

  public showImage(value: FileDTO) {
    this.fileService.apiFilesPathGet(value.filePath)
      .subscribe(result => {
        this.img = this.domSanitizer.bypassSecurityTrustUrl('data:image/jpg;base64, ' + result.fileContents);;
      });
    
    this.exifData = [];
    this.fileService.apiFilesExifPathGet(value.filePath)
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
