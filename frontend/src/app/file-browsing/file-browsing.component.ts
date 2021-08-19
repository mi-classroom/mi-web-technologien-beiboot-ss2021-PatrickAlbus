import { Component, Output, EventEmitter, Injectable, Inject } from '@angular/core';
import { DirectoryService, DirectoryDTO, FileService, FileDTO, ExifDTO } from "../../api/index";
import { environment } from "../../environments/environment"

interface Service {
  readonly directoryName: string;
}

@Component({
  selector: 'app-file-browsing',
  templateUrl: './file-browsing.component.html',
  styleUrls: ['./file-browsing.component.scss'],
  providers: [DirectoryService, FileService ]
})

export class FileBrowsingComponent{
  @Output() 
  imagePath = new EventEmitter<string>();

  @Output()
  imageDataJson = new EventEmitter<string>();

  public filteredDirectoryPaths: DirectoryDTO[] = [];
  public directoryPaths: DirectoryDTO;
  public filter: string;
  
  private downloadPath: string;

  private location: string[] = [];
  private previousPath: string[] = [];
  
  constructor(private directoryService: DirectoryService) {
    this.downloadPath = environment.API_BASE_PATH + "/api/directories/download?path=";
    this.getDirectoryAndFilePaths();
  }
  
  private getDirectoryAndFilePaths(_directoryDTO?: DirectoryDTO) {
    this.directoryService.apiDirectoriesGet()
      .subscribe(result => {
        this.directoryPaths = result;
      });
  }

  public nextDirectory(_directoryPath: string, _directoryName: string){
    this.filter = null;
    this.location.push(_directoryName);
    this.previousPath.push(this.directoryPaths.directoryPath);
    this.directoryService.apiDirectoriesGet(_directoryPath)
      .subscribe(result => {
        this.directoryPaths = result;
      })
  }

  public gotoPreviousPath(){
    this.filter = null;
    this.directoryService.apiDirectoriesGet(this.previousPath.pop())
      .subscribe(result => {
        this.location.pop();
        this.directoryPaths = result;
      })
  }

  public chooseImage(filePath: string){
    this.imagePath.emit(filePath);
  }

  public chooseImageDataJson(_imageDataJson: string){
    this.imageDataJson.emit(_imageDataJson);
  }
}