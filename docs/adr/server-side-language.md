# Auswahl der/des serverseitigen Programmiersprache/Framework

* Status: akzeptiert
* Entscheider: [Patrick Albus](https://github.com/Narua2010)  
* Datum: 17.04.2021

Technische Geschichte: Für das Beibootprojekt in dem Modul Webtechnologien soll eine Client- und eine Serverkomponente entwickelt werden. Im Laufe des Seminars werden die Anforderungen erweitert.

## Kontext und Problemstellung

Welche Programmiersprache bzw. welches Framework soll für die Umsetzung der Serverkomponente genutzt werden?
Es ist zu beachten, dass durch erweiterbare Funktionalitäten die Anforderungen sich anpassen können.

## Entscheidungsgeber
* Das Backend sollte bei jeder beteiligen Person unkompliziert eingerichtet und gestartet werden.
* Das Backend soll eine gradlinige API zu den bereitgestellten Daten erlauben. Hierzu zählen:
  * Auslesen der Verzeichnisse mit den Bildern anhand eines konfigurierbaren Musters, z.B. *-representative.jpg
  * Rückgabe der Verzeichnisstruktur
  * Rückgabe der Bilder
  * Rückgabe der Bilddaten (Exif, IPTC)

## Betrachtete Optionen

* Python
  *  Django
* PHP
  * Laravel
* NodeJS
  * ExpressJS 
* C#
  * ASP.NET 
* Java
  * Spring Boot 

## Ergebnis der Entscheidung

Für die Umsetzung der Serverkomponente wurde sich für __ASP.NET__ entschieden, da es sich bei den Entscheidungsgebern um Standardoperationen handelt, welche letztlich von jeder Sprache gelöst werden.
Dementsprechend wurde bei der Entscheidung darauf geachtet, welche Umgebung am umkompliziertesten aufzusezten ist. Darüber hinaus wurde das eigene Interesse und der akuelle Kenntnisstand mit einbezogen.
Auch wenn ASP.NET recht umfangreich im Grundaufbau ist, hilft es, durch seine mitgelieferten Namespaces skalierbar zu sein für die kommenden Anforderungen.

## Vor- und Nachteile der Optionen 

### Django

* Funktionsreich - viele Funktionen mit denen allgemeine Anforderungen erfüllt werden können.
* Sicher - schützt unter anderem vor Cross-Site-Scripting oder SQL Injection
* Gute Skalierbarkeit
* Enthält ein Framework für Python-Unit-Tests

### Laravel
* bietet sofortige Unterstützung von Cache-Backends
* enthält eine Template Engine (beeinflusst ggf. die Entscheidung von #25)

### ExpressJS
* Mit NodeJS können sowohl Frontend- als auch Backendanwendungen entwickelt werden (beeinflusst ggf. die Entscheidung von #25)

### ASP.NET
* Plattformübergreifende Unterstützung
* Minimale Codierung - Entwickler benötigen weniger Zeit, um eine Anwendung zu erstellen
* Einfache Wartung des Codes
* Bessere Leistung - durch den integrierten Compiler von ASP.NET kann der Code verbessert werden, wenn das Framework mit dem Code neu kompiliert wird
* Ermöglicht asynchrone Abläufe
* Ermöglicht Multithreading

### Spring Boot

## Quellen

* [Back4App](https://blog.back4app.com/backend-frameworks/)