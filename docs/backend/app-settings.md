# App Einstellungen - Backend

## Allgemeine Einstellungen
In der Datei `appsettings.json` befinden sich die allgemeinen Einstellungen für das Backend. Diese müssen jenach Kontext angepasst werden. Die Datei ist zu finden unter `.backend\WTBeiboot_SS21_Albus/appsettings.json`.

In dieser Datei befinden sich mehrere Abschnitte. Für die relevanten Einstellungen wird jedoch nur der folgende Bereich benötigt.
```
"Settings": {
    "TargetDirectory": "../mnt",
    "FilePattern": [
      ".jpeg",
      ".jpg"
    ],
    "Configuration": [
      {
        //Only IPTC Data can be change
        "Title": "Exif",
        "Values": [
        ]
      },
      {
        "Title": "IPTC",
        "Values": [
          {
            "Name": "Name",
            "IsEditable": true,
            "Type": "Text",
            "MaxLength": 64,
            "Languages": [
              {
                "Shortcut": "de",
                "Label": "Titel"
              },
              {
                "Shortcut": "en",
                "Label": "Title"
              }
            ]
          },
          {
            "Name": "Caption",
            "IsEditable": true,
            "Type": "Textarea",
            "MaxLength": 2000,
            "Languages": [
              {
                "Shortcut": "de",
                "Label": "Dateiart / Beschreibung"
              },
              {
                "Shortcut": "en",
                "Label": "Type / Description"
              }
            ]
          },
          {
            "Name": "CaptionWriter",
            "IsEditable": true,
            "Type": "Text",
            "MaxLength": 32,
            "Languages": [
              {
                "Shortcut": "de",
                "Label": "Autor / Rechte"
              },
              {
                "Shortcut": "en",
                "Label": "Author / Copyright"
              }
            ]
          },
          {
            "Name": "Source",
            "IsEditable": true,
            "Type": "Text",
            "MaxLength": 32,
            "Languages": [
              {
                "Shortcut": "de",
                "Label": "Quelle"
              },
              {
                "Shortcut": "en",
                "Label": "Source"
              }
            ]
          }
        ]
      }
    ],
    "ImageJson": "imageData-1.1.json"
  }
```

* Die Variable `TargetDirectory` gibt den relativen Pfad zum Ordner an, in welchem sich die Artefakte befinden.
* Die Variable `FilePattern` gibt an, welche Dateiendungen bei Dokumenten und Bildern ausgelesen werden sollen.
* Unter der Variable `Configuration` werden die Metadatenfelder konfiguriert, welche auszulesen sind. Hierbei wird zwischen `EXIF` und `IPTC` unterschieden. Diese werden jeweils in der Variable `Title` angegeben. Darunter befinden sich dann die verschiedenen `Values`. Diese dürfen leer sein, wenn kein Wert aus den entsprechenden Metadaten ausgelesen werden soll. 
Sollten doch Metadaten ausgelesen werden, muss ein Objekt angelegt werden. Dieses besteht aus folgenden Variablen:
    * Die Variable `Name` definiert den Typ der jeweiligen Metadaten. Dieser ist in der Dokumentation von [SixLabors](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.Metadata.Profiles.Iptc.IptcTag.html) nachzulesen. 
    * Mit `IsEditable` wird angegeben, ob der Wert vom Nutzer bearbeitet werden darf.
    * Die Variable `Type` gibt an, um welches Anzeigeformat es sich im Frontend handelt. Hierbei wird unterschieden zwischen `Text` und `Textarea`.
    * Mit der Variable `MaxLength` wird die maximal erlaubte Länge des Feldes angegeben. Hierbei ist zu beachten, dass diese nicht Länger als die für das Feld vorgesehene maximale Länge sein darf. Diese ist ebenfalls in der Dokumentation von [SixLabors](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.Metadata.Profiles.Iptc.IptcTag.html) nachzulesen.
    * Das letzte Element in der Configuration ist die Variable `Languages`. Hier befinden sich wieder Objekte drunter, welche für die verschiedenen auszugebenden Sprachen auf der Webseite verwendet werden. 
        * Die Variable `Shortcut` gibt hierbei den Ländercode an.
        * Die Variable `Label` gibt an, welche Beschriftung das Feld auf der Webseite erhalten soll.
* Die Variable `ImageJson` nennt den Namen, welcher zu dem Dokument in den einzelnen Ordner gehört, welches die allgemeinen Informationen zu einem Artefakt enthält.

## Hinzufügen der Bilder
Damit die einzelnen Artefakte nicht mit in das Repository geladen werden, wurde das Standardverzeichnis in der `.gitignore` ausgeschlossen. Bei dem Standardverzeichnis handelt es sich um den Ordner `./data`. 
Damit die Bilddaten abgerufen werden können, müssen die beteiligten Personen diese in dem Ordner ergänzen, bevor der Docker Container gestartet wird.

Dabei soll folgende Beispielstruktur eingehalten werden:
```
data/
    ArtefaktOrdner1/
        Overall/
            Artefakt.jpg
        Reverse/
            Artefakt.jpg
        IRR/
            Artefakt.jpg
        imageData-1.1.json
    ArtefaktOrdner2/
    ArtefaktOrdner3/
```

## Hinweis
Sollten nach Änderungen an den Daten diese nicht richtig vom Docker Container übernommen werden, sollte das Docker-Image neu gebildet werden.
Dies wird mithilfe von dem Zusatz `--build` erzwungen.

```
docker-compose up --build -d backend
```