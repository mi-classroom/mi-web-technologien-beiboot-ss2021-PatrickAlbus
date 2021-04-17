import { Component } from '@angular/core';
import { DirectoryService, DirectoryDTO, FileService, FileDTO, ExifDTO } from "../../api/index";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [DirectoryService, FileService]
})

export class HomeComponent {
  public directoryPaths: DirectoryDTO[] = [{
    childDirectories: null,
    directoryName: null,
    directoryPath: null,
    files: null
  }];
  public currentPath: DirectoryDTO[] = []
  public img = null;
  public exifData: ExifDTO[] = [];

  constructor(private directoryService: DirectoryService, private fileService: FileService) {
    this.getDirectoryAndFilePaths();
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
    this.img = value.filePath;
    this.exifData = [];
    this.fileService.apiFilesPathGet(value.filePath)
      .subscribe(result => {
        result.forEach(obj => {
          if (obj.name == "Exif IFD0") {
            obj.tags.forEach(tag => {
              this.exifData.push({
                exifName: tag.name,
                exifDescription: tag.description
              });
            })
          }
        })
      })
  }
}
