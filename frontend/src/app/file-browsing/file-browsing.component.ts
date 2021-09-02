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
  
  public directoryLength: number = 0;
  public fileLength: number = 0;

  public downloadPath: string;

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
        this.setDirectoryFileCount();
      });
  }

  public nextDirectory(_directoryPath: string, _directoryName: string){
    this.filter = null;
    this.location.push(_directoryName);
    this.previousPath.push(this.directoryPaths.directoryPath);
    this.directoryService.apiDirectoriesGet(_directoryPath)
      .subscribe(result => {
        this.directoryPaths = result;
        this.setDirectoryFileCount();
      })
  }

  public gotoPreviousPath(){
    this.filter = null;
    this.directoryService.apiDirectoriesGet(this.previousPath.pop())
      .subscribe(result => {
        this.location.pop();
        this.directoryPaths = result;
        this.setDirectoryFileCount();
      })
  }

  public setDirectoryFileCount(){
    this.directoryLength = (this.directoryPaths.childDirectories != null) ? this.directoryPaths.childDirectories.length : 0;
    this.fileLength = (this.directoryPaths.files != null) ? this.directoryPaths.files.length : 0;
  }

  public changeFilter(){
    this.directoryLength = 0;
    if(this.directoryPaths.childDirectories != null){
      this.directoryPaths.childDirectories.forEach(element => {
        if(element.directoryName.toLowerCase().includes(this.filter.toLowerCase())) this.directoryLength++;
      });
    }

    this.fileLength = 0;
    if(this.directoryPaths.files != null){
      this.directoryPaths.files.forEach(element => {
        if(element.fileName.toLowerCase().includes(this.filter.toLowerCase())) this.fileLength++;
      });
    }
  }

  public chooseImage(filePath: string){
    this.imagePath.emit(filePath);
  }

  public chooseImageDataJson(_imageDataJson: string){
    this.imageDataJson.emit(_imageDataJson);
  }
}