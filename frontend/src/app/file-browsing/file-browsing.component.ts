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
    this.location.push(_directoryName);
    this.previousPath.push(this.directoryPaths.directoryPath);
    this.directoryService.apiDirectoriesGet(_directoryPath)
      .subscribe(result => {
        this.directoryPaths = result;
      })
  }

  // public filterChange(){
  //   this.filteredDirectoryPaths = this.directoryPaths;
    
  //   if(this.filter != null && this.filter != ""){
  //       this.removefilteredData(this.filteredDirectoryPaths);
  //   }
  // }

  // private removefilteredData(directories: DirectoryDTO[]){
  //   for(var i = directories.length - 1; i>=0; i--){
  //     if(directories[i].childDirectories != null) this.removefilteredData(directories[i].childDirectories);
  //     if(directories[i].files != null){
  //       for(var j = directories[i].files.length - 1; j>=0; j--){
  //         if(directories[i].files[j].fileName.toLowerCase().includes(this.filter.toLowerCase()) == false) {
  //           directories[i].files.splice(j, 1)
  //         }
  //       }
  //     }
  //     if((directories[i].childDirectories == null || directories[i].childDirectories.length == 0) && (directories[i].files == null || directories[i].files.length == 0)) directories.splice(i,1);
  //   }
  //   this.directoryService.apiDirectoriesGet()
  //     .subscribe(result => {
  //       this.directoryPaths = result;
  //     });
  // }

  public gotoPreviousPath(){
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