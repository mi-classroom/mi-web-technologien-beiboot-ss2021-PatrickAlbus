# Nutzung von Docker Compose

* Status: vorgeschlagen
* Entscheider: [Patrick Albus](https://github.com/Narua2010)  
* Datum: 15.05.2021

Technische Geschichte: Das Beibootprojekt in dem Modul Webtechnologien soll mithilfe von Docker gestartet werden. 

## Kontext und Problemstellung

Das Beibootprojekt soll mithilfe von Docker gestartet werden. Dabei soll weiterhin beachtet werden, dass das Frontend und das Backend getrennt voneinander laufen sollen.

## Betrachtete Optionen

* Docker Compose

## Ergebnis der Entscheidung

Sowohl für das Backend, als auch für das Frontend wird ein Dockerfile benötigt. Darüber hinaus wird eine Docker-Compose Datei erstellt.
Mithilfe von Docker Compose können über eine Datei mehrere Dienste mit einem einzigen Befehl gestartet bzw. beendet werden.
Der große Vorteil der Verwendung von Docker Compose besteht darin, dass diese Datei im Stammverzeichnis des Repositorys abgelegt werden kann, wohingegen die Dockerfiles weiterhin in den Quellcodeverzeichnissen liegen. Dadurch kann eine andere Person einfach an dem Projekt mitwirken. Dafür muss er lediglich das Repository klonen und die Compose-App starten.