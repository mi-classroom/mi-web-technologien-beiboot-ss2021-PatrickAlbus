import { Component, Input, OnChanges, SimpleChanges} from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { FileService, ExifDTO, ConfigurationDTO } from "../../../api/index";

interface ExifConfiguration {
    Language: string;
    Values: ExifConfigurationValues[];
}

interface ExifConfigurationValues {
    Name: string;
    Label: string;
    Type: string;
    MaxLength: string;
    Value: string;
    IsEditable: boolean;
}

interface ExifJsonObject {
    Language: string;
    Value: string;
}

@Component({
  selector: 'app-exif-footer',
  templateUrl: './exif-footer.component.html',
  styleUrls: ['./exif-footer.component.scss'],
  providers: []
})

export class ExifFooterComponent implements OnChanges{
    public _exifData: ExifDTO = null;
    public _exifConfiguration: ExifConfiguration[] = []; 

    @Input()
    startupConfiguration: ConfigurationDTO[] = null;

    @Input()
    exifData: ExifDTO = null;

    @Input()
    filePath: string = null;

    constructor(private fileService: FileService) {
    }

    ngOnChanges(changes: SimpleChanges){
        if(this.exifData != null){
            this._exifData = this.exifData;
            if(this.startupConfiguration != null){
                this.updateConfiguration();
            }
        } else {
            this._exifData = null;
        }
    }

    private updateConfiguration(){
        this._exifConfiguration = [];
        if(this.startupConfiguration != null && this.startupConfiguration.length > 0){
            this.startupConfiguration.forEach(element => {
                if(element.values.length > 0){
                    element.values.forEach(value => {
                        value.languages.forEach(language => {
                            let _checkLanguage = false;
                            this._exifConfiguration.forEach(final => {
                                if(final.Language == language.shortcut){
                                    _checkLanguage = true;
                                    final.Values.push({
                                        Name: value.name,
                                        Label: language.label,
                                        Type: value.type,
                                        MaxLength: value.maxLength,
                                        Value: this.getConfigurationValue(language.label, value.name),
                                        IsEditable: value.isEditable
                                    });
                                } 
                            });

                            if(!_checkLanguage){
                                this._exifConfiguration.push({
                                    Values: [{
                                        Name: value.name,
                                        Label: language.label,
                                        Type: value.type,
                                        MaxLength: value.maxLength,
                                        Value: this.getConfigurationValue(language.label, value.name),
                                        IsEditable: value.isEditable
                                    }],
                                    Language: language.shortcut
                                });
                            }
                        });
                    });
                }
            });
        }
    }

    private getConfigurationValue(language: string, name: string){
        let response = null;
        this._exifData.exifData.forEach(data => {
            if(data.exifName == name){
                try{
                    console.log(JSON.parse(data.exifDescription));
                    response = data.exifDescription;
                }
                catch(e){
                    response = data.exifDescription;
                }
            }
        });
        return response;
    }

    public textChange(id: string, maxLength: string, text: string){
        try{
            let percent = Math.round((100*text.length)/Number(maxLength));
            let color = "--accent";
            var element = document.querySelector('#'+id) as HTMLElement;

            if(percent >= 80) color = "--error";

            element.style.backgroundImage = "linear-gradient(to left, var(--lighten-strong) "+ (Number(100) - percent) +"%,var("+color+") 0%)";
        }
        catch{}
    }

    public showExif(){
        this._exifConfiguration.forEach(parent => {
            parent.Values.forEach(child => {
                this.textChange(parent.Language + '' + child.Name, child.MaxLength, child.Value);
            });
        });
        
        let container = document.querySelector('.exifContainer') as HTMLElement;
        let exifDataContainer = document.querySelector('.exifDataContainer') as HTMLElement;

        if(container.style.height == "50vh"){
            container.style.height = "4vh";
            exifDataContainer.style.display = "none";
        }else{
            container.style.height = "50vh";
            exifDataContainer.style.display = "block";
        }
    }

    public saveExif(){
        if(this._exifConfiguration.length > 0){
            let setExif: ExifDTO = {
                size: this._exifData.size,
                exifData: []
            };

            this._exifConfiguration[0].Values.forEach(item => {
                let exifJson: ExifJsonObject[] = [];
                exifJson.push({
                    Language: this._exifConfiguration[0].Language,
                    Value: item.Value
                });

                for(let i = 1;i<this._exifConfiguration.length;i++){
                    this._exifConfiguration[i].Values.forEach(innerItem => {
                        if(innerItem.Name == item.Name){
                            exifJson.push({
                                Language: this._exifConfiguration[i].Language,
                                Value: innerItem.Value
                            });
                        }
                    })
                }

                setExif.exifData.push({
                    exifName: item.Name,
                    exifIsEditable: item.IsEditable,
                    exifDescription: JSON.stringify(exifJson)
                });
            });
            
            this.fileService.apiFilesPathPut(this.filePath, setExif)
                .subscribe(response => {
                    console.log(response);
                })
        }
    }
}
