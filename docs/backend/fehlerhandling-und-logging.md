# Fehlerhandling und Logging - Backend

## Auslesen der Logfiles
Sollte es zu Problemen im Backend kommen, werden die entscheidenden Informationen, welche zur Laufzeit geschehen in einem Logfile gespeichert.
Dieses wird unter Docker in dem folgenden Verzeichnis gespeichert: `./logs/{Datum}_logfile.txt`
* Wenn die CLI von Docker geöffnet ist, kann zwischen den Verzeichnissen mit `cd` das Verzeichnis gewechselt werden.
* Mit dem Befehl `ls` lässt sich die aktuelle Struktur einsehen. Dadurch kann der Name der Logdatei ausgelesen werden, da sich diese durch die Datumskategorisierung regelmäßig ändert.
* Zum Schluss lässt sich die Logdatei mit dem Befehl `tail {Dateiname}` auslesen.

Innerhalb der Logdatei wird zwischen vier Szenarien unterschieden.
1. `LogInfo`: Hier werden Informationen angezeigt um über den Ablauf bescheid zu wissen.
2. `LogWarn`: Hier werden Warnungen aufgelistet.
3. `LogDebug`: Diese Ausgabe wird nur bei der Entwicklung benutzt, um zu schauen ob beim Umzug auf ein Produktivsystem alles funktioniert hat.
4. `LogError`: In Falle eines Fehlers, wird dieser in der entsprechen Rubrik abgebildet.
