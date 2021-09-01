# Ausführen des Projektes
Bitte beachtet, dass zum Ausführen des Projekts `Docker` installiert sein muss.

## Komplettes Projekt Starten (Frontend und Backend)
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

## Frontend starten

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

## Backend Starten
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