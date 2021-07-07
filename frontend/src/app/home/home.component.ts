import { Component, Input, OnChanges, SimpleChanges, ViewChild } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { DirectoryService, DirectoryDTO, FileService, FileDTO, ExifDTO } from "../../api/index";


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [FileService ]
})

export class HomeComponent implements OnChanges {
  public img = null;
  private exifData: ExifDTO[] = [];
  public imgJson = null;

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

  private saveMetadata(){
    console.log(this.filePath);
    console.log(this.exifData);
    this.fileService.apiFilesFilePathPut(this.filePath, this.exifData)
      .subscribe(result => {
        console.log(result);
      });
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
          this.exifData = result;
        })
  }
}
