import { Component, Input, OnChanges, SimpleChanges, } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ExifDTO, ConfigurationDTO } from "../../../api/index";

interface ExifConfiguration {
    Language: string;
    Values: ExifConfigurationValues[];
}

interface ExifConfigurationValues {
    Name: string;
    Label: string;
    Type: string;
    MaxLenght: string;
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

    constructor() {
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
                                        MaxLenght: value.maxLenght,
                                        Value: this.getConfigurationValue(language.label, value.name)
                                    });
                                } 
                            });

                            if(!_checkLanguage){
                                this._exifConfiguration.push({
                                    Values: [{
                                        Name: value.name,
                                        Label: language.label,
                                        Type: value.type,
                                        MaxLenght: value.maxLenght,
                                        Value: this.getConfigurationValue(language.label, value.name)
                                    }],
                                    Language: language.shortcut
                                });
                            }
                        });
                    });
                }
            });
            console.log(this._exifConfiguration);
        }
    }

    private getConfigurationValue(language: string, name: string){
        let response = null;
        this._exifData.exifData.forEach(data => {
            if(data.exifName == name){
                try{
                    console.log(JSON.parse(data.exifDescription));
                }
                catch(e){
                    response = data.exifDescription;
                }
            }
        });
        return response;
    }

    public textChange(id: string, maxLenght: string, text: string){
        let percent = Math.round((100*text.length)/Number(maxLenght));
        let color = "--accent";
        var element = document.querySelector('#'+id) as HTMLElement;

        if(percent >= 80) color = "--error";

        element.style.backgroundImage = "linear-gradient(to left, var(--lighten-strong) "+ (Number(100) - percent) +"%,var("+color+") 0%)";
    }

    public showExif(){
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
}
