# Nutzung von Docker Compose

* Status: akzeptiert
* Entscheider: [Patrick Albus](https://github.com/Narua2010)  
* Datum: 15.05.2021

Technische Geschichte: Das Beibootprojekt in dem Modul Webtechnologien soll mithilfe von Docker gestartet werden. 

## Kontext und Problemstellung

Damit zum einen die beteiligen Personen möglichst zügig und unkompliziert das Projekt lauffähig auf ihren individuellen Systemen (MacOS, Windows und Linux) für die Entwicklung vorfinden können und zum anderen das Projekt automatisiert in eine Produktionsumgebung veröffentlicht werden kann, wird eine Virtualisierungs- bzw. Containerlösung benötigt. Da verschiedene Komponenten (Backend und Frontend) zur Realisierung des Projekts benötigt werden, wird auch eine sinnvolle Orchestrierung der min. zwei, ggf. in Zukunft +n Systeme benötigt. Das Ziel ist es den Overheat für Installation, Konfiguration und Aktualisierung durch die Automatisierung gering zu halten.

## Betrachtete Optionen

* Docker + docker-compose
* Nomad
* Rancher

## Ergebnis der Entscheidung

Durch die vorhandene Erfahrung wurde sich für Docker mithilfe von docker-compose entschieden. Dadurch bleibt die Einarbeitungszeit relativ gering. Ebenso gilt Docker als Quasistandard.

### Positive Konsequenzen

* Die Installation von Entwicklungskomponenten ist nicht notwendig
* Konfiguration der Container ist an einem zentralen Ort möglich
* Erweiterung neuer Komponenten ist leicht möglich

### Negative Konsequenzen

* Docker wird als Abhängigkeit benötigt und muss entsprechend auf allen Systemen installiert werden
* Benötigt mehr Systemressourcen

## Links

* [docker-compose Alternativen](https://www.slant.co/options/11648/alternatives/~docker-compose-alternatives)