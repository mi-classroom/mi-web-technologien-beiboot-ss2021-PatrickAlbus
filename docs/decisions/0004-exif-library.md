# Auswahl der Bibliothek zum Auslesen und Ändern der EXIF/IPTC Daten

* Status: akzeptiert
* Entscheider: [Patrick Albus](https://github.com/Narua2010)  
* Datum: 08.07.2021

Technische Geschichte: In dem Beibootprojekt soll es möglich sein, Bilddaten (Exif/IPTC) auszulesen und abzuändern.

## Kontext und Problemstellung

Es soll auf die Metadaten eines Bildes zugegriffen werden und diese sollen ebenfalls manipulierbar sein. Dafür gibt es Bibliotheken, welche den direkten Zugriff auf die entsprechenden Daten liefern.

## Betrachtete Optionen

* [Six Labors](https://docs.sixlabors.com/)
* [MetadataExtractor](https://www.nuget.org/packages/MetadataExtractor/)

## Ergebnis der Entscheidung

Es wurde sich für die Verwendung von [Six Labors](https://docs.sixlabors.com/) entschieden, da diese Bibliothek trotz ihrer Komplexität beide Anwendungsfälle abdecken kann.

## Vor- und Nachteile der Optionen 

### Six Labors

* Mithilfe von Six Labors können die Metadaten sowohl ausgelesen als auch bearbeitet werden.
* Benötigt eine eigene Abfrage für IPTC, EXIF und ICC Daten.

### MetadataExtractor

* Ermöglicht es, alle Metadaten mit einem Befehl auszulesen.
* Bietet keine Option, die Metadaten, insbesondere die IPTC Daten anzupassen.