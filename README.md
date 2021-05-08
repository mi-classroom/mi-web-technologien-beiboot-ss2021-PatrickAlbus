# Web Technologien // begleitendes Projekt Sommersemester 2021

Zum Modul Web Technologien gibt es ein begleitendes Projekt. Im Rahmen dieses Projekts werden wir von Veranstaltung zu Veranstaltung ein Projekt sukzessive weiter entwickeln und uns im Rahmen der Veranstaltung den Fortschritt anschauen, Code Reviews machen und Entwicklungsschritte vorstellen und diskutieren.

Als organisatorischen Rahmen für das Projekt nutzen wir GitHub Classroom. Inhaltlich befassen wir uns mit der Entwicklung einer kleinen Web-Anwendung für die Bearbeitung von Bildern. Hierbei steht weniger ein professioneller Konzeptions-, Entwurfs- und Entwicklungsprozess im Vordergrund, sondern vielmehr die sukzessive Weiterentwicklung einer Anwendung, das Ausprobieren, Vergleichen, Refactoren und die Freude an lauffähigem Code.

## Ausführen des Projektes
Bitte beachtet, dass zum Ausführen des Projekts `Docker` installiert sein muss.

### Komplettes Projekt Starten (Frontend und Backend)
Das Project wird per Docker (docker-compose) im Hauptverzeichnis ausgeliefert.

Mit dem Befehl 

```
docker-compose up -d
```

wird der Dienst gestartet. Existiert das `image` nicht auf dem Rechner, wird es herruntergeladen und automatisch beim ersten Lauf gebaut. Sollten danach Änderungen an der Dockerfile für das Frontend vorgenommen werden, muss dieses per

```
docker-compose up --build -d
```

neu gebaut werden. Die Option `-d` lässt bei Erfolg den Container im Hintergrund laufen. Ein erfolgreicher Start kann einmal per 

```
docker-compose ps
```

überprüft bzw. der Aufruf im Browser über die URL [localhost:80](http://localhost:80/) (für das Frontend) und [localhost:8080](http://localhost:8080/) (für das Backend) überprüft werden.

Mit 

```
docker-compose down
```

lässt sich der Dienst runterfahren.

### Frontend starten

Das Frontend wird per Docker (docker-compose) im Hauptverzeichnis ausgeliefert.

Mit dem Befehl 

```
docker-compose up -d frontend
```

wird der Dienst `frontend` gestartet. Existiert das `image` nicht auf dem Rechner, wird es herruntergeladen und automatisch beim ersten Lauf gebaut. Sollten danach Änderungen an der Dockerfile für das Frontend vorgenommen werden, muss dieses per

```
docker-compose up --build -d frontend
```

neu gebaut werden. Die Option `-d` lässt bei Erfolg den Container im Hintergrund laufen. Ein erfolgreicher Start kann einmal per 

```
docker-compose ps
```

überprüft bzw. der Aufruf im Browser über die URL [localhost:80](http://localhost:80/) überprüft werden.

Mit 

```
docker-compose down
```

lässt sich der Dienst runterfahren.

### Backend Starten
Das Backend wird per Docker (docker-compose) im Hauptverzeichnis ausgeliefert.
Mit dem Befehl 
```
docker-compose up -d backend
```
wird der Dienst `backend` gestartet. Existiert das `image` nicht auf dem Rechner, wird es heruntergeladen und automatisch beim ersten Lauf gebaut. Sollten danach Änderungen an dem Dockerfile für das Backend vorgenommen werden, muss dieser per
```
docker-compose up --build -d backend
```
neu gebaut werden. Die Option `-d` lässt bei Erfolg den Container im Hintergrund laufen. Ein erfolgreicher Start kann einmal per
```
docker-compose ps
```
überprüft bzw. der Aufruf im Browser über die URL [localhost:8080](http://localhost:8080/) überprüft werden.

Mit 
```
docker-compose down
```
lässt sich der Dienst herunterfahren.

## APIs
Die API-Dokumentation ist unter [http://localhost:8080/swagger](http://localhost:8080/swagger) zu finden.
Dort können diese ebenfalls getestet werden und die URL der entsprechenden Abfrage werden ebenfalls angezeigt.